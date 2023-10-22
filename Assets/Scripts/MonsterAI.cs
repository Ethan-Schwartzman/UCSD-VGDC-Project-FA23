using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour {
    public ChannelManager channelManager;
    public int maxTimeUntilAppear;
    public int minTimeUntilAppear;
    public GameObject TVStaticGameobject;
    public GameObject Handprint;
    public GameObject Pentagram;
    public PageTurner PageTurnerScript;
    public float MonsterEventMinTime;
    public float MonsterEventMaxTime;


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

        StopAllCoroutines();
        StartCoroutine(MonsterEventCountdown());
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

    public void MonsterEvent() {
        MonsterType monster = MonsterTypeManager.currentMonster;
        int eventType = Random.Range(0, 3);

        if(eventType == 0 && monster.CausesTVStatic) {
            CauseTVStatic();
        }

        if(eventType == 1 && monster.ChangesTimer) {
            //
        }

        if(eventType == 2 && monster.LeavesBlood) {
            LeaveBlood();
        }
    }

    public void CauseTVStatic() {
        int loops = Random.Range(1, 5);
        SpriteRenderer staticSprite = TVStaticGameobject.GetComponent<SpriteRenderer>();
        float time = Random.Range(0.5f, 3f);
        StartCoroutine(WaitForTVStatic(time, staticSprite, loops));

    }

    public void LeaveBlood() {
        int bookOrWindow = Random.Range(0, 2);
        if(true) { // window //TODO
            Handprint.SetActive(true);
        } else { // book
            Pentagram.SetActive(true);
            int pageIndex = Random.Range(0, PageTurnerScript.Pages.Length-2);
            GameObject targetPage = PageTurnerScript.Pages[pageIndex];
            Pentagram.transform.SetParent(targetPage.transform.GetChild(0));
            //Pentagram.transform.SetParent(targetPage.transform);
            if(pageIndex % 2 == 0) {
                Pentagram.transform.position = new Vector3(2f, 0, 0);
            } else {
                Pentagram.transform.position = new Vector3(2f, 0, 0);
            }
            Pentagram.transform.localScale = new Vector3(5, 5, 1);
            Pentagram.GetComponent<SpriteRenderer>().sortingOrder = 12+pageIndex;
        }
    }

    private IEnumerator WaitForTVStatic(float time, SpriteRenderer staticSprite, int loops) {
        yield return new WaitForSeconds(time);
        staticSprite.color = new Color(0.6f, 0.6f, 0.6f, Random.Range(0.05f, 0.8f));
        time = Random.Range(0.2f, 1.5f);
        loops--;
        if(loops >= 0) {
            StartCoroutine(WaitForTVStatic(time, staticSprite, loops));
        }
        else {
            staticSprite.color = new Color(0.6f, 0.6f, 0.6f, 0.02f);
        }
    }

    private IEnumerator MonsterEventCountdown() {
        float time = Random.Range(MonsterEventMinTime, MonsterEventMaxTime);
        yield return new WaitForSeconds(time);
        MonsterEvent();
        StartCoroutine(MonsterEventCountdown());
    }
}
