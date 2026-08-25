/*  1. Max Starreveld 
 *  2. ID 2380029
 *  3. starreveld@chapman.edu
 *  4. CPSC 245 
 *  5. Project 1 - Stopwatch
 */

// Counting state code
public class CountingState : TimerStates
{
    public CountingState(TimerStateController sc) : base(sc) { }

    // Perform necessary operations on entering state
    public override void OnStateEntered()
    {
        
        timer.StopwatchStartCounting();
    }

    // Perform necessary operations on exiting state
    public override void OnStateExit()
    {
        //timer.StopwatchStopCounting();
    }

    // When state is updated, update timer text
    public override void OnStateUpdate()
    {
        timer.UpdateStopwatchText();
    }
}
