using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour {
    public ChannelManager channelManager;
    public int maxTimeUntilAppear;
    public int minTimeUntilAppear;

    private int NumberOfChannels;
    private int channelResidence; // Which channel the monster is hiding in
    private float timer; // Count up starting when on channel with monster
    private int timeUntilAppear; // Time after channel change to show itself
    private bool channelActive; // True if monster is on current active channel
    private GameObject currentPosition; // The GameObject in the location on screen where monster will appear

    public void GameBegin() {
        // Initialize Monster
        NumberOfChannels = channelManager.NumberOfChannels;
        channelResidence = Random.Range(1, NumberOfChannels);
        currentPosition = channelManager.GetCurrentChannelObject().GetComponent<Channel>().GetRandomMonsterPosition();
        Debug.Log(channelResidence); // Debug Helper
        Debug.Log(currentPosition);
    }

    // Update is called once per frame
    void Update() {
        // If monster is on active channel, countdown until appearance
        if (channelActive) {
            print(channelActive); // Debug Helper

            timer += Time.deltaTime;

            // If countdown runs out, appear
            if (timer > timeUntilAppear) {
                currentPosition.SetActive(true);
                Sprite[] monsterSprites = MonsterTypeManager.currentMonster.Sprites;
                currentPosition.GetComponent<SpriteRenderer>().sprite = monsterSprites[Random.Range(0, monsterSprites.Length)];
                // Choose next channel to appear on
                channelResidence = Random.Range(0, NumberOfChannels);
                // Ensures monster doesn't want to return to same channel
                if (channelResidence == channelManager.GetCurrentChannel()) {
                    if (channelResidence == 0) {
                        channelResidence = NumberOfChannels - 1;
                    }
                    else {
                        channelResidence = 0;
                    }
                }
                print(channelResidence); // Debug Helper
                channelActive = false;
                timer = 0;
            }
        }
    }

    // Called upon a channel change
    public void ChannelChange() {
        currentPosition.SetActive(false);
        // If monster on current active channel
        if (channelManager.GetCurrentChannel() == channelResidence) {
            // Get a position on screen to show up in
            Transform currentChannelObject = channelManager.GetCurrentChannelObject();
            currentPosition = currentChannelObject.GetComponent<Channel>().GetRandomMonsterPosition();
            channelActive = true;
            timeUntilAppear = Random.Range(minTimeUntilAppear, maxTimeUntilAppear);
        }
        else {
            channelActive = false;
            timer = 0;
            currentPosition.SetActive(false);

            // Possible minion jumpscare if new channel isn't channel with monster

        }
    }

    public bool GetChannelActive() {
        return channelActive;
    }
}
