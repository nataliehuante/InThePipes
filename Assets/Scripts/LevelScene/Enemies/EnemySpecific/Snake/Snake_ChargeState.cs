using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_ChargeState : ChargeState
{
    private Snake snake;

    public Snake_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData, Snake snake) : base(entity, stateMachine, animBoolName, stateData) {
        this.snake = snake;
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();

        // in case we have hit the player during this state, reset our variables before we exit 
        // rat.hitPlayer = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if we meet a wall or a ledge, go to idle state
        if (!isDetectingLedge || isDetectingWall) {
            snake.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(snake.idleState);
        }
        // if player is within bite range, attack 
        else if (isPlayerInBiteDistance) {
            stateMachine.ChangeState(snake.attackState);
        }
        // once we have charged for a certain amount of time 
        else if (isChargeTimeOver) {
            // Debug.Log("charge time over");
            // if player is still within attack range, go to player detected state
            // if (isPlayerInMinAttackRange) {
            stateMachine.ChangeState(snake.playerDetectedState);
                // Debug.Log("player in range");
            // }
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
