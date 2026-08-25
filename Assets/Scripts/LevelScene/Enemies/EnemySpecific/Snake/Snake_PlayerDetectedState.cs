using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_PlayerDetectedState : PlayerDetectedState
{
    private Snake snake;
    public Snake_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetectedState stateData, Snake snake) : base(entity, stateMachine, animBoolName, stateData) {
        this.snake = snake;
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();

        // snake.hitPlayer = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if out confusion time is done
        if (performLongRangeAction) {
            // if player is no longer within attack range, keep moving
            if (!isPlayerInMinAttackRange) {
                stateMachine.ChangeState(snake.moveState);
            } 
            // otherwise, charge in the player's direction
            else {
                stateMachine.ChangeState(snake.chargeState);
            }
           
        }
        else if (performShortRangeAction || (!previousStateAttack)) {
            if (isPlayerInBiteDistance) {
                stateMachine.ChangeState(snake.attackState);
            }
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
