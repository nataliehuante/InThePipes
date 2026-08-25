using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected Data_ChargeState stateData;
    protected bool isPlayerInMinAttackRange;
    protected bool isPlayerInBiteDistance;
    protected bool isDetectingWall;
    protected bool isDetectingWallVertical;
    protected bool isDetectingLedge;
    protected bool isChargeTimeOver;
    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void Enter() {
        base.Enter();

        entity.SetVelocity(stateData.chargeSpeed);
        isChargeTimeOver = false;
    }

    public override void Exit() {
        base.Exit();

        
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime) {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public override void DoChecks() {
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRange();
        isPlayerInBiteDistance = entity.CheckPlayerInBiteRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isDetectingWallVertical = entity.CheckWallVertical();
    }

    
}
