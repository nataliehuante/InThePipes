using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_HurtState : State
{
    private Snake snake;
    private float startTime;

    public Snake_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Snake snake) : base(entity, stateMachine, animBoolName) {
        this.snake = snake;
    }
    
    public override void Enter() {
        Debug.Log("Entered hurt state");
        base.Enter();

        startTime = Time.time;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        if (Time.time >= startTime + 0.5f) {
            // Debug.Log("Hurt animation done playing");
            stateMachine.ChangeState(snake.playerDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
