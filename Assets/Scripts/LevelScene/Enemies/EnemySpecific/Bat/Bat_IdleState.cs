using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_IdleState : IdleState
{
    private Bat bat;

    public Bat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Bat bat) : base(entity, stateMachine, animBoolName, stateData) {
        this.bat = bat;
    }

    public override void Enter() {
        base.Enter();
        
        entity.SetVelocityY(0f);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if we detect the player in front of us, change to player detected state
        if (isPlayerInMinAttackRange) {
            stateMachine.ChangeState(bat.playerDetectedState);
        }
        // wait until idle time is over, then start moving 
        else if (isIdleTimeOver) {
            stateMachine.ChangeState(bat.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        DoChecks();

        
    }

    public override void DoChecks() {
        // base.DoChecks();
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRangeVertical();
    }
}
