using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildlifeAI : MonoBehaviour {
    public ChannelManager channelManager;
    public MonsterAI monsterAI;
    public int maxTimeUntilAppear;
    public int minTimeUntilAppear;
    public int appearChance;

    private float timer; // Count up starting when on channel with monster
    private int timeUntilAppear; // Time after channel change to show itself
    private GameObject currentPosition;
    private bool active; // True if wildlife will appear on channel

    // Start is called before the first frame update
    public void GameBegin() {
        active = false;
        currentPosition = channelManager.GetCurrentChannelObject().GetComponent<Channel>().GetRandomMonsterPosition();
    }

    // Update is called once per frame
    void Update() {
        if (active) {
            timer += Time.deltaTime;
            if (timer > timeUntilAppear) {
                currentPosition.SetActive(true);
            }
        }
    }

    public void ChannelChange() {
        int yes = Random.Range(0, 100);
        Debug.Log(yes);
        if (yes < appearChance) {
            active = true;
            timer = 0;
        }
        else {
            active = false;
            currentPosition.SetActive(false);
        }
        if (active) {
            Transform currentChannelObject = channelManager.GetCurrentChannelObject();
            currentPosition = currentChannelObject.GetComponent<Channel>().GetRandomWildlifePosition();
            timeUntilAppear = Random.Range(minTimeUntilAppear, maxTimeUntilAppear);
        }

    }
}
