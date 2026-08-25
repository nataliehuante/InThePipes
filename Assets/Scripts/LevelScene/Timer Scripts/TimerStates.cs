using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStates
{
    // Enum of all possible states
    public enum TimerState
    {
        Counting,
        Paused,
        Reset
    }

    protected Timer timer;
    protected float timeTimerStarted;
    protected float currentTime;
    protected TimerStates(TimerStateController sc)
    {
        this.timer = sc.GetComponent<Timer>();
    }

    // Virtual methods to be overloaded in each state
    public virtual void OnStateEntered() { }
    public virtual void OnStateExit() { }
    public virtual void OnStateUpdate() { }
}
