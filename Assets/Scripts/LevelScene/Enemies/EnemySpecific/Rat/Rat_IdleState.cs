using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_IdleState : IdleState
{
    private Rat rat;

    public Rat_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Rat rat) : base(entity, stateMachine, animBoolName, stateData) {
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
        // wait until idle time is over, then start moving 
        else if (isIdleTimeOver) {
            stateMachine.ChangeState(rat.moveState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
