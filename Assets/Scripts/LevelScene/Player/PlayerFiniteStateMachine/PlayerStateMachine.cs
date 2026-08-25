using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{  
    // has a public getter and a private setter
    public PlayerState CurrentState{ get; private set; }

    // used to initialize what state the player starts in
    public void Initialize(PlayerState startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // will be called every time we change states to handle the exits and enters properly
    public void ChangeState(PlayerState newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
