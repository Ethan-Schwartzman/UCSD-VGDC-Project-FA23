using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour {
    //timer is set to 5 minutes in seconds
    public float timeValue = 300;
    //disply text
    public TextMeshPro timeText;
    public MonsterTimerChange monsterTimerChange;
    public bool monsterTakeover;

    private float monsterTimeValue;
    private float shownTime;

    // Update is called once per frame
    void Update() {
        //if there are time left, start the timer
        if (timeValue > 0) {
            timeValue -= Time.deltaTime;
        }
        else
        //this prevents the timeValue to have the negative integers
        {
            timeValue = 0;
        }

        if (monsterTakeover) {
            monsterTimeValue = monsterTimerChange.getTimeValue();
            shownTime = monsterTimeValue;
            monsterTimerChange.setTimeValue(monsterTimeValue - Time.deltaTime);
        }
        else {
            //have the timeValue pass through the method to display time
            shownTime = timeValue;
        }
        DisplayTime(shownTime);
    }
    //this method calculates the time and then display them
    public void DisplayTime(float timeToDisplay) {
        //prevents the timeDisplay to have a negative value
        if (timeToDisplay < 0) {
            timeToDisplay = 0;
        }
        if (timeToDisplay < 30) {
            timeText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else {
            timeText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        //calculate how many minutes left
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        //calculate how many seconds left  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //{0:00}:{1:00} are parameters. the first parameter assigns what value, for example
        //the 0 before the colon is assigned to minutes while the 1 is assinged to seconds
        //the second parameter is basically what values you want to show
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimer(float time) {
        timeValue = time;
    }
}
