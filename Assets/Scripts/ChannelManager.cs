using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelManager : MonoBehaviour
{

    public bool RandomizeOrder;
    public int NumberOfChannels;
    public GameObject ActiveChannels;
    public GameObject InactiveChannels;
    private int currentChannel;
    private Transform[] channels;
    private int axisReset;
    public BookMovement bookMovement;
    public MonsterAI monsterAI;
    public WildlifeAI wildlifeAI;
    public SpriteRenderer UpArrow;
    public SpriteRenderer DownArrow;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Set channels active/inactive
        foreach(Transform child in InactiveChannels.transform) {
            child.gameObject.SetActive(false);
        }
        InactiveChannels.SetActive(false);
        ActiveChannels.SetActive(true);

        // Store all the channel transforms in an array
        Transform allChannels = InactiveChannels.transform;
        channels = new Transform[allChannels.childCount];
        for(int i = 0; i < allChannels.childCount; i++) {
            channels[i] = allChannels.GetChild(i);
        }

        // Option to randomize the order of the channels
        if(RandomizeOrder) {
            // Knuth Shuffle
            int n = channels.Length;
            while (n > 1) 
            {
                int k = Random.Range(0, n--); //double check this line
                Transform temp = channels[n];
                channels[n] = channels[k];
                channels[k] = temp;
            }
        }

        // Set active channels
        // (Active channels are set to children of the ActiveChannels GameObject)
        for(int i = 0; i < NumberOfChannels; i++) {
            channels[i].SetParent(ActiveChannels.transform);

            // Ensure the channel has the channel component
            if (channels[i].GetComponent<Channel>() == null) Debug.Log("An active channel is missing the channel component");

            channels[i].GetComponent<Channel>().GameBegin();
        }

        // Set the current channel
        channels[0].gameObject.SetActive(true);
        currentChannel = 0;

        // Enable AI
        monsterAI.GameBegin();
        //wildlifeAI.GameBegin();

        //Fetch remote sound
        audioSource = GetComponent<AudioSource>();
    }

    public void Update(){
        // axisReset is used to prevent spam changing the channels
        // by holding down a direction

        //if player press down the up key
        float verticalValue = Input.GetAxisRaw("Vertical"); 
        if(axisReset != 1 && verticalValue > 0.02) {
            axisReset = 1;
            ChannelUp();
        }
        // if player press down the down key
        else if(axisReset != -1 && verticalValue < -0.02){
            axisReset = -1;
            ChannelDown();
        }
        else if(verticalValue <= 0.02 && verticalValue >= -0.02){
            axisReset = 0;
            UpArrow.color = new Color(0.9f, 0.9f, 0.9f, 0.9f);
            DownArrow.color = new Color(0.9f, 0.9f, 0.9f, 0.9f);
        }
    }

    public void ChannelUp() {
        // Hides the old channel
        channels[currentChannel].gameObject.SetActive(false);

        // Increments the channel tracker and loops back to 0
        // if it's too big
        currentChannel++;
        if(currentChannel >= NumberOfChannels) currentChannel = 0;

        // Shows the new channel
        channels[currentChannel].gameObject.SetActive(true);

        // Tell MonsterAI that channel has changed
        monsterAI.ChannelChange();
        //wildlifeAI.ChannelChange();

        // Move the book
        bookMovement.StopAllCoroutines();
        bookMovement.StartCoroutine(bookMovement.MoveDown());

        // Set the arrow sprite color
        UpArrow.color = new Color(0.6f, 0.6f, 0.6f, 1f);

        //play sound
        audioSource.Play();
    }

    public void ChannelDown() {
        // Hides the old channel
        channels[currentChannel].gameObject.SetActive(false);

        // Increments the channel tracker and loops back to the
        // last channel if it's negative
        currentChannel--;
        if(currentChannel < 0) currentChannel = NumberOfChannels-1;

        // Shows the new channel
        channels[currentChannel].gameObject.SetActive(true);

        // Tell MonsterAI that channel has changed
        monsterAI.ChannelChange();
        //wildlifeAI.ChannelChange();

        // Move the book
        bookMovement.StopAllCoroutines();
        bookMovement.StartCoroutine(bookMovement.MoveDown());

        // Set the arrow sprite color
        DownArrow.color = new Color(0.6f, 0.6f, 0.6f, 1f);

        //play sound
        audioSource.Play();
    }

    public int GetCurrentChannel()
    {
        return currentChannel;
    }

    public Transform GetCurrentChannelObject()
    {
        return channels[currentChannel];
    }
}
