using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_MoveState : MoveState
{
    private Bat bat;

    public Bat_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Bat bat) : base(entity, stateMachine, animBoolName, stateData) {
        this.bat = bat;
    }

    public override void Enter() {
        // base.Enter();
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
        entity.SetVelocityY(stateData.movementSpeed);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        // base.LogicUpdate();
        DoChecks();
        
        // if we detect the player in front of us, change to player detected state
        if (isPlayerInMinAttackRange) {
            stateMachine.ChangeState(bat.playerDetectedState);
        }
        // if detects wall or no longer detects ground in front of it, then idle 
        else if (isDetectingWallVertical) {
            bat.idleState.SetFlipAfterIdleVertical(true);
            stateMachine.ChangeState(bat.idleState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        DoChecks();
        
    }

    public override void DoChecks() {
        base.DoChecks();
        // isDetectingLedge = entity.CheckLedge();
        isDetectingWallVertical = entity.CheckWallVertical();
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRangeVertical();
    }
}
