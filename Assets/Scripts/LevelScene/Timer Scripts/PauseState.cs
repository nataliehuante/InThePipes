//Reset State code
public class PauseState : TimerStates
{
    public PauseState(TimerStateController sc) : base(sc) { }

    // Perform necessary operations on entering state
    public override void OnStateEntered()
    {
        timer.StopwatchPauseCounting();
    }

    // Nothing needs to happen here
    public override void OnStateExit()   { }

    // Nothing needs to happen here
    public override void OnStateUpdate() { }
}