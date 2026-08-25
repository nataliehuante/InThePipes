using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_AttackState : AttackState
{
    private Bat bat;

    public Bat_AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_AttackState stateData, Bat bat) : base(entity, stateMachine, animBoolName, stateData) {
        this.bat = bat;
    }

    public override void Enter() {
        base.Enter();
        
        // cause damage to the player 
        entity.SetVelocity(0f);
        entity.SetVelocityY(0.1f);
        bat.bitePlayer("bat");
    }   

    public override void Exit() {
        base.Exit();

        
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // once the animation is done, change back to player detected state
        if (doneAttacking) {
            // Debug.Log("attack animation done playing");
            bat.playerDetectedState.SetPrevStateAttack();
            stateMachine.ChangeState(bat.playerDetectedState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();

        
    }
}
