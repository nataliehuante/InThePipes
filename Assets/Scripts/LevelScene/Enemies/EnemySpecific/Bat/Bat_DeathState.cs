using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_DeathState : State
{
    private Bat bat;
    private float startTime;
    private bool startedFadeOut;

    public Bat_DeathState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Bat bat) : base(entity, stateMachine, animBoolName) {
        this.bat = bat;
    }
    
    public override void Enter()  {
        base.Enter();
        entity.SetVelocityY(0f);
        startTime = Time.time;
        bat.gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        // if (entity.animator.GetCurrentAnimatorStateInfo(0).length > 1.667f) {
        //     rat.destroy();
        // }

        if (Time.time > startTime + 5f) {
            bat.destroy();
        } else if (!startedFadeOut) {
            bat.startFadeOut(3f);   
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
