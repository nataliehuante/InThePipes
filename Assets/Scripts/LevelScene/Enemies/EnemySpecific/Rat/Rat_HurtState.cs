using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_HurtState : State
{
    private Rat rat;
    private float startTime;

    public Rat_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Rat rat) : base(entity, stateMachine, animBoolName) {
        this.rat = rat;
    }
    
    public override void Enter() {
        // Debug.Log("Entered hurt state");
        base.Enter();

        startTime = Time.time;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        if (Time.time >= startTime + 0.5f) {
            // Debug.Log("Hurt animation done playing");
            stateMachine.ChangeState(rat.playerDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
