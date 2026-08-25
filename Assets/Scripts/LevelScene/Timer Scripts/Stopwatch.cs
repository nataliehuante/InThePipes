/*  1. Max Starreveld 
 *  2. ID 2380029
 *  3. starreveld@chapman.edu
 *  4. CPSC 245 
 *  5. Project 1 - Stopwatch
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Tracking time and updating text
public class Stopwatch : MonoBehaviour
{
    //Member vars
    private Timer timer;
    private float timeTimerStarted;
    private float currentTime;

    // Assign timer component
    private void Start()
    {
        timer = GetComponent<Timer>();
    }

    // Update time if counting
    private void Update()
    {
        if (timer.stateController.timerState == timer.stateController.CountingState)
        {
            currentTime = Time.time - timeTimerStarted;
        }
    }

    // public void StopS

    // Keep track of time counted, NOT time passed
    public void StartStopwatch()
    {
        if (currentTime != 0)
        {
            timeTimerStarted = Time.time - currentTime;
        }
        else
        {
            timeTimerStarted = Time.time;
        }
    }

    // Reset timer
    public void ResetStopwatch()
    {
        timeTimerStarted = 0;
        currentTime = 0;
    }

    // Return current time count
    public float GetCurrentTime()
    {
        return currentTime;
    }
}
