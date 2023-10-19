using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    //timer is set to 5 minutes in seconds
    public float timeValue= 300;
    //disply text
    public TextMeshProUGUI timeText;
    // Update is called once per frame
    void Update()
    {
        //if there are time left, start the timer
        if(timeValue>0)
        {
          timeValue -= Time.deltaTime;
        }
        else
        //this prevents the timeValue to have the negative integers
        {
            timeValue = 0;
        }
        //have the timeValue pass through the method to display time
        DisplayTime(timeValue);
    }
    //this method calculates the time and then display them
    void DisplayTime(float timeToDisplay)
    {
        //prevents the timeDisplay to have a negative value
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
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
}