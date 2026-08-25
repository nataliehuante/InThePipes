using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Data_AttackState stateData;
    protected bool doneAttacking;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_AttackState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void Enter() {
        base.Enter();

        entity.SetVelocity(0.1f);
        doneAttacking = false;

    }

    public override void Exit() {
        base.Exit();

        doneAttacking = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the attack animation has played once, change back to playerDetected state 
        if (Time.time >= startTime + stateData.timeToAttackFor) {
            doneAttacking = true;
        }

        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public override void DoChecks() {
        
    }
}
