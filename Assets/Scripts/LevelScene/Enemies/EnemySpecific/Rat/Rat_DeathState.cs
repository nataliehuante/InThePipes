using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_DeathState : State
{
    private Rat rat;
    private float startTime;
    private bool startedFadeOut;

    public Rat_DeathState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Rat rat) : base(entity, stateMachine, animBoolName) {
        this.rat = rat;
    }
    
    public override void Enter()  {
        base.Enter();
        entity.SetVelocity(0f);
        startTime = Time.time;
        rat.gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        // if (entity.animator.GetCurrentAnimatorStateInfo(0).length > 1.667f) {
        //     rat.destroy();
        // }

        if (Time.time > startTime + 5f) {
            rat.destroy();
        } else if (!startedFadeOut) {
            rat.startFadeOut(3f);   
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
