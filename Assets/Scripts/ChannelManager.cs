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

    // Start is called before the first frame update
    void Start()
    {
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
            if(channels[i].GetComponent<Channel>() == null) Debug.Log("An active channel is missing the channel component");
        }

        // Set the current channel
        channels[0].gameObject.SetActive(true);
        currentChannel = 0;
    }

    public void Update(){
        //if player press down the up key
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           ChannelUp();

        }
        //if player press down the down key
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChannelDown();
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
    }
}
