using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_MoveState : MoveState
{
    private Snake snake;

    public Snake_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Snake snake) : base(entity, stateMachine, animBoolName, stateData) {
        this.snake = snake;
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        
        // if we detect the player in front of us, change to player detected state
        if (isPlayerInMinAttackRange) {
            stateMachine.ChangeState(snake.playerDetectedState);
        }
        // if detects wall or no longer detects ground in front of it, then idle 
        else if (isDetectingWall || !isDetectingLedge) {
            snake.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(snake.idleState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
