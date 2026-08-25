using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_IdleState : IdleState
{
    private Snake snake;

    public Snake_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Snake snake) : base(entity, stateMachine, animBoolName, stateData) {
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
        // wait until idle time is over, then start moving 
        else if (isIdleTimeOver) {
            stateMachine.ChangeState(snake.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
