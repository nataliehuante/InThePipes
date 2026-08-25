using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_PlayerDetectedState : PlayerDetectedState
{
    private Bat bat;
    public Bat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetectedState stateData, Bat bat) : base(entity, stateMachine, animBoolName, stateData) {
        this.bat = bat;
    }

    public override void Enter() {
        base.Enter();
        
        entity.SetVelocityY(0f);
    }

    public override void Exit() {
        base.Exit();

        // bat.hitPlayer = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if out confusion time is done
        if (performLongRangeAction) {
            // if player is no longer within attack range, keep moving
            if (!isPlayerInMinAttackRange) {
                stateMachine.ChangeState(bat.moveState);
            } 
            // otherwise, charge in the player's direction
            else {
                stateMachine.ChangeState(bat.chargeState);
            }
        }
        else if (performShortRangeAction || (!previousStateAttack)) {
            if (isPlayerInBiteDistance) {
                stateMachine.ChangeState(bat.attackState);
            }
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        DoChecks();

        
    }

    public override void DoChecks() {
        // base.DoChecks();
        // isDetectingLedge = entity.CheckLedge();
        // isDetectingWall = entity.CheckWallVertical();
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRangeVertical();
        isPlayerInBiteDistance = entity.CheckPlayerInBiteRangeVertical();
    }
}
