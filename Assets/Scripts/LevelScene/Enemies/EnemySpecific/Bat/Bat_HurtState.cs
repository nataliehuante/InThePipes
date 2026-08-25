using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_HurtState : State
{
    private Bat bat;
    private float startTime;

    public Bat_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Bat bat) : base(entity, stateMachine, animBoolName) {
        this.bat = bat;
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
            stateMachine.ChangeState(bat.playerDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
