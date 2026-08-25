using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_PlayerDetectedState : PlayerDetectedState
{
    private Rat rat;
    public Rat_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetectedState stateData, Rat rat) : base(entity, stateMachine, animBoolName, stateData) {
        this.rat = rat;
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();

        // rat.hitPlayer = false;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if out confusion time is done
        if (performLongRangeAction) {
            // if player is no longer within attack range, keep moving
            if (!isPlayerInMinAttackRange) {
                stateMachine.ChangeState(rat.moveState);
            } 
            // otherwise, charge in the player's direction
            else {
                stateMachine.ChangeState(rat.chargeState);
            }
           
        } 
        else if (performShortRangeAction || (!previousStateAttack)) {
            if (isPlayerInBiteDistance) {
                stateMachine.ChangeState(rat.attackState);
            }
        }

    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
