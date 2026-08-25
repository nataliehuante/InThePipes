using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected Data_PlayerDetectedState stateData;
    protected bool isPlayerInMinAttackRange;
    // protected bool isPlayerInMaxAttackRange;
    protected bool performLongRangeAction;
    protected bool performShortRangeAction;
    protected bool isPlayerInBiteDistance;
    protected bool flipBefore;
    protected bool previousStateAttack = false;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void Enter() {
        base.Enter();

        performLongRangeAction = false;
        performShortRangeAction = false;
        entity.SetVelocity(0f);

        // if (flipBefore) {
        //     entity.Flip();
        // }
        
    }

    public override void Exit() {
        base.Exit();

        flipBefore = false;
        previousStateAttack = false;
        
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime) {
            performLongRangeAction = true;
        }

        if (Time.time >= startTime + stateData.shortRangeActionTime) {
            performShortRangeAction = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

    }

    public override void DoChecks() {
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRange();
        // isPlayerInMaxAttackRange = entity.CheckPlayerInMaxAttackRange();
        isPlayerInBiteDistance = entity.CheckPlayerInBiteRange();
    }

    // public void SetFlipBefore(bool shouldFlip) {
    //     flipBefore = shouldFlip;
    // }

    public void SetPrevStateAttack() {
        previousStateAttack = true;
    }
}
