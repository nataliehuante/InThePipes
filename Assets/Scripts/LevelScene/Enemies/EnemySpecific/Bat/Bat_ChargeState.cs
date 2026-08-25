using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_ChargeState : ChargeState
{
    private Bat bat;

    public Bat_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData, Bat bat) : base(entity, stateMachine, animBoolName, stateData) {
        this.bat = bat;
    }

    public override void Enter() {
        base.Enter();
        
        entity.SetVelocity(0f);
        entity.SetVelocityY(stateData.chargeSpeed);
    }

    public override void Exit() {
        base.Exit();

    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if we meet a wall or a ledge, go to idle state
        if (isDetectingWall) {
            bat.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(bat.idleState);
        }
        // if player is within bite range, attack 
        else if (isPlayerInBiteDistance) {
            stateMachine.ChangeState(bat.attackState);
        }
        // once we have charged for a certain amount of time 
        else if (isChargeTimeOver) {
            stateMachine.ChangeState(bat.playerDetectedState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }

    public override void DoChecks() {
        base.DoChecks();
        // isDetectingLedge = entity.CheckLedge();
        // isDetectingWall = entity.CheckWallVertical();
        isDetectingWallVertical = entity.CheckWallVertical();
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRangeVertical();
        isPlayerInBiteDistance = entity.CheckPlayerInBiteRangeVertical();
    }
}
