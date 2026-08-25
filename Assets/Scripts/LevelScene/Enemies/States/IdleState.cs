using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected Data_IdleState stateData;
    protected bool flipAfterIdle;
    protected bool flipAfterIdleVertical;
    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAttackRange;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData) : base(entity, stateMachine, animBoolName) {
        this.stateData = stateData;
    }

    public override void Enter() {
        base.Enter();
        
        entity.SetVelocity(0f);
        isIdleTimeOver = false;

        SetRandomIdleTime();
    }

    public override void Exit() {
        base.Exit();

        if (flipAfterIdle) {
            entity.Flip();
        }
        else if (flipAfterIdleVertical) {
            entity.FlipVertical();
        }

        flipAfterIdle = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime) {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
        

    public override void DoChecks() {
        base.DoChecks();
        isPlayerInMinAttackRange = entity.CheckPlayerInMinAttackRange();
    }

    public void SetFlipAfterIdle(bool shouldFlip) {
        flipAfterIdle = shouldFlip;
    }

    public void SetFlipAfterIdleVertical(bool shouldFlip) {
        flipAfterIdleVertical = shouldFlip;
    }

    private void SetRandomIdleTime() {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }


}
