using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStateController : MonoBehaviour
{
    public TimerStates timerState;

    //Include the states, but remove them from inspector. Behind the scenes declaration

    [HideInInspector] public CountingState CountingState;
    [HideInInspector] public ResetState ResetState;
    [HideInInspector] public PauseState PauseState;

    public GameObject timer;
    
    public void Awake()
    {
        CountingState = new CountingState(this);
        ResetState = new ResetState(this);
        PauseState = new PauseState(this);
    }

    public void Start()
    {
        timerState = CountingState;
    }

    public void Update()
    {
        if (timerState != null)
        {
            timerState.OnStateUpdate();
        }
    }

    // Switch states
    public void NewState(TimerStates newState)
    {
        if (timerState != null)
            timerState.OnStateExit();

        timerState = newState;
        timerState.OnStateEntered();
    }
}
