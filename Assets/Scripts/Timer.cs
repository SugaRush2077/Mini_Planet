using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    private float timeRemaining = 0;
    private bool timeIsRunning = false;
    public TMP_Text timeText;
    public TMP_Text FinalTimeText;
    // Start is called before the first frame update
    void Start()
    {
        UltimatePlayer.whenPlayerDead += OnCCompleted;
    }
    private void OnDestroy()
    {
        UltimatePlayer.whenPlayerDead -= OnCCompleted;
    }
    private void OnCCompleted()
    {
        DisplayTimeInCenti(timeRemaining);
        timeIsRunning = false;
        timeRemaining = 0;
    }

    private void OnEnable()
    {
        startTimer();
    }

    private void OnDisable()
    {
        DisplayTimeInCenti(timeRemaining);
        timeIsRunning = false;
        timeRemaining = 0;
        Debug.Log("Timer Disable");
    }
    public void stopTimer()
    {
        timeIsRunning = false;
        //DisplayTimeInCenti(timeRemaining);
    }

    public void startTimer()
    {
        timeIsRunning = true;
        timeRemaining = 0;
        DisplayTimeInCenti(timeRemaining);
    }
    void FixedUpdate()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                
                DisplayTimeInCenti(timeRemaining);
            }
        }
    }

    void DisplayTimeInCenti(float timeToDisplay)
    {
        //timeToDisplay += 1;
        /*
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);*/


        // Transform second to centseconds
        int totalHundredths = Mathf.FloorToInt(timeToDisplay * 100);

        // calculate
        float minutes = totalHundredths / (60 * 100);
        float seconds = (totalHundredths / 100) % 60;
        float centiseconds = totalHundredths % 100;

        // Format
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
        FinalTimeText.text = string.Format("Time: {0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);


    }
}
