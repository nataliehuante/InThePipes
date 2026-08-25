/*  1. Max Starreveld 
 *  2. ID 2380029
 *  3. starreveld@chapman.edu
 *  4. CPSC 245 
 *  5. Project 1 - Stopwatch
 */

//Reset State code
public class ResetState : TimerStates
{
    public ResetState(TimerStateController sc) : base(sc) { }

    // Perform necessary operations on entering state
    public override void OnStateEntered()
    {
        timer.StopwatchReset();
    }

    // Nothing needs to happen here
    public override void OnStateExit()   { }

    // Nothing needs to happen here
    public override void OnStateUpdate() { 
        timer.stateController.NewState(timer.stateController.CountingState);
    }
}
