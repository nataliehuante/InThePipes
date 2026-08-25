/*  1. Max Starreveld 
 *  2. ID 2380029
 *  3. starreveld@chapman.edu
 *  4. CPSC 245 
 *  5. Project 1 - Stopwatch
 */

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Timer setup and maintenance
public class Timer : MonoBehaviour
{
    // Member variables
    public TimerStateController stateController;
    public Stopwatch stopwatch;
    public TextMeshProUGUI stopwatchText;

    //Include the states, but remove them from inspector. Behind the scenes declaration
    [HideInInspector] public CountingState CountingState;
    [HideInInspector] public ResetState ResetState;
    [HideInInspector] public PauseState PauseState;

    // Call relevant update method
    private void Update()
    {
        // if (stateController.timerState != null)
        //     stateController.timerState.OnStateUpdate();
    }

    // Start stopwatch and enable counting outline
    public void StopwatchStartCounting()
    {
        stopwatch.StartStopwatch();
    }


    public void ResetStopwatch() {
        stopwatch.ResetStopwatch();
    }
    // Pause stopwatch without resetting the clock, disable outline.
    public void StopwatchPauseCounting()
    {
        //stopwatch.PauseStopwatch();
    }
    
    // Reset stopwatch and remove outline
    public void StopwatchReset()
    {
        stopwatch.ResetStopwatch();
        this.UpdateStopwatchText();
    }
    
    // Stopwatch text formatting and update
    public void UpdateStopwatchText()
    {
        TimeSpan ts = TimeSpan.FromSeconds(stopwatch.GetCurrentTime());
        String result = ts.ToString("mm\\:ss");
        stopwatchText.text = result;
    }

    public void OnResetLevel_ButtonClick() {
        stateController.NewState(stateController.ResetState);
    }



}
