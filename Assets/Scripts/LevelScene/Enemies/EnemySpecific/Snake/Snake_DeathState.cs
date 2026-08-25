using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_DeathState : State
{
    private Snake snake;
    private float startTime;
    private bool startedFadeOut;

    public Snake_DeathState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Snake snake) : base(entity, stateMachine, animBoolName) {
        this.snake = snake;
    }
    
    public override void Enter()  {
        base.Enter();
        entity.SetVelocity(0f);
        startTime = Time.time;
        snake.gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        // if (entity.animator.GetCurrentAnimatorStateInfo(0).length > 1.667f) {
        //     rat.destroy();
        // }

        if (Time.time > startTime + 5f) {
            snake.destroy();
        } else if (!startedFadeOut) {
            snake.startFadeOut(3f);   
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
