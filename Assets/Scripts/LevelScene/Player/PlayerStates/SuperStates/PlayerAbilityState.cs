using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;
    protected bool grabInput;
    private bool isGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }

    public override void DoChecks() {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter() {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (isAbilityDone) {
            // ability to idle state transition
            if (isGrounded && player.CurrentVelocity.y < 0.01f) {
                stateMachine.ChangeState(player.IdleState);
            } // ability to in air state transition
            else {
                stateMachine.ChangeState(player.InAirState);
            }
        }        
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
