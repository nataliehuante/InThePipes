using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_AttackState : AttackState
{
    private Snake snake;

    public Snake_AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_AttackState stateData, Snake snake) : base(entity, stateMachine, animBoolName, stateData) {
        this.snake = snake;
    }

    public override void Enter() {
        base.Enter();
        
        // cause damage to the player 
        snake.bitePlayer("snake");
    }   

    public override void Exit() {
        base.Exit();

        
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        if (doneAttacking) {
            // Debug.Log("attack animation done playing");
            snake.playerDetectedState.SetPrevStateAttack();
            stateMachine.ChangeState(snake.playerDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
