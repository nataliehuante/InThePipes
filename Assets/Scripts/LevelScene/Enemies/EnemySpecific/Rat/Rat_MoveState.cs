using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_MoveState : MoveState
{
    private Rat rat;

    public Rat_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Rat rat) : base(entity, stateMachine, animBoolName, stateData) {
        this.rat = rat;
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        
        // if we detect the player in front of us, change to player detected state
        if (isPlayerInMinAttackRange) {
            stateMachine.ChangeState(rat.playerDetectedState);
        }
        // if detects wall or no longer detects ground in front of it, then idle 
        else if (isDetectingWall || (!isDetectingLedge)) {
            rat.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(rat.idleState);
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }


}
