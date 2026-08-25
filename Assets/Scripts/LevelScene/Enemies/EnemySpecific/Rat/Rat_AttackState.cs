using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_AttackState : AttackState
{
    private Rat rat;

    public Rat_AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_AttackState stateData, Rat rat) : base(entity, stateMachine, animBoolName, stateData) {
        this.rat = rat;
    }

    public override void Enter() {
        base.Enter();
        
        // cause damage to the player 
        rat.bitePlayer("rat");
    }   

    public override void Exit() {
        base.Exit();

        
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // // once the animation is done, change back to player detected state
        // if (entity.animator.GetCurrentAnimatorStateInfo(0).length > entity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime) {
        //     // Debug.Log("attack animation done playing");
        //     stateMachine.ChangeState(rat.playerDetectedState);
        // }
        if (doneAttacking) {
            rat.playerDetectedState.SetPrevStateAttack();
            stateMachine.ChangeState(rat.playerDetectedState);
        }
            
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
