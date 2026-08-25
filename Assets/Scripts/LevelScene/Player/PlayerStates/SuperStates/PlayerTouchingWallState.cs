using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingWallBack;
    protected int xInput;
    protected int yInput;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool grappleInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }

    public override void AnimationFinishTrigger() {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger() {
        base.AnimationTrigger();
    }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        if (!player.inLobby) {
            isTouchingWall = player.CheckIfTouchingWall();
            isTouchingWallBack = player.CheckIfTouchingWallBack();
        }
        
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;
        jumpInput = player.InputHandler.JumpInput;
        grappleInput = player.InputHandler.GrappleInput;

        // touching wall state to idle state transition
        if (isGrounded && !grabInput) { // from touching wall to idle state
            stateMachine.ChangeState(player.IdleState);
        } // touch wall state to in air state transition
        else if (!isTouchingWall || (xInput != player.FacingDirection && !grabInput)) { // if we are no longer moving towards the wall && not on ground, transition to inAirState
            stateMachine.ChangeState(player.InAirState);
        } // touching wall state to wall jump state
        else if (jumpInput && (isTouchingWall || isTouchingWallBack)) {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        } // touching wall state to grapple state
        else if (grappleInput) {
            stateMachine.ChangeState(player.GrappleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
