using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTimerChange : MonoBehaviour {
    public Timer timer;
    public bool monsterTakeOver = false;
    public int takeOverLimit;
    public int maxTimeUntilTakeover;
    public int minTimeUntilTakeover;
    public float monsterTakeOverTimeStart;
    public float monsterTakeOverTimeStop;


    private float timeUntilNextTakeOver;
    private float timeValue;
    public int takeOverCount = 0;


    // Start is called before the first frame update
    void Start() {
        timeUntilNextTakeOver = Random.Range(minTimeUntilTakeover, maxTimeUntilTakeover);
        timeValue = monsterTakeOverTimeStart;

        Debug.Log("timeuntil" + timeUntilNextTakeOver);
    }

    private void Update() {
        if (takeOverCount < takeOverLimit) {
            timeUntilNextTakeOver -= Time.deltaTime;
            if (!monsterTakeOver && timeUntilNextTakeOver < 0) {
                timer.monsterTakeover = true;
                monsterTakeOver = true;
                takeOverCount++;
                timeValue = monsterTakeOverTimeStart;
            }
            if (timeValue < monsterTakeOverTimeStop) {
                timer.monsterTakeover = false;
                monsterTakeOver = false;
                timeValue = monsterTakeOverTimeStart;
                timeUntilNextTakeOver = Random.Range(minTimeUntilTakeover, maxTimeUntilTakeover);
            }
        }
        else {
            timer.monsterTakeover = false;
            monsterTakeOver = false;
            timeValue = monsterTakeOverTimeStart;
        }
    }

    // Update is called once per frame
    public float getTimeValue() {
        return timeValue;
    }

    public void setTimeValue(float newTimeValue) {
        timeValue = newTimeValue;
    }
}
