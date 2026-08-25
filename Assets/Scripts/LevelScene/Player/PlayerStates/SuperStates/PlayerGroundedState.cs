using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput; 
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool grabInput;
    protected bool grappleInput;
    private bool shootInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }

    public override void DoChecks() {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        if (!player.inLobby) {
            isTouchingWall = player.CheckIfTouchingWall();
        } else {
            // isTouchingWall =  false;
        }
    }

    public override void Enter() {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        grappleInput = player.InputHandler.GrappleInput;
        shootInput = player.InputHandler.ShootInput;

        // grounded to jump state
        if (JumpInput && player.JumpState.CanJump()) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } // grounded to in air state
        else if (!isGrounded) {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        } // grounded to wall grab state
        else if (isTouchingWall && grabInput) {
            stateMachine.ChangeState(player.WallGrabState);
        } // grounded to grappling state
        else if (grappleInput) {
            stateMachine.ChangeState(player.GrappleState);
        } 
        else if (shootInput && isGrounded)
        {
            stateMachine.ChangeState(player.ShootingState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
