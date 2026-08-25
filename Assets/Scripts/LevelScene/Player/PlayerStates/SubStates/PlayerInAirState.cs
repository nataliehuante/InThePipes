using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime; // gives the player some wiggle room to press jump after falling off from ground
    private bool isJumping;
    private bool grabInput; 
    private bool grappleInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
    }

    public override void Enter() {
        base.Enter();
        player.LandState.ResetLandHard();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        CheckCoyoteTime();
        if (player.CurrentVelocity.y < -6f) {
            player.LandState.SetLandHard();
        }

        // read in inputs
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        grappleInput = player.InputHandler.GrappleInput;

        CheckJumpMultiplier();

        // in air to land state transition
        if (isGrounded && player.CurrentVelocity.y < 0.01f) { // if we are on the ground and velocity is small, switch to land state
            stateMachine.ChangeState(player.LandState);
        } // in air to wall jump transition (this takes priority over regular jump)
        else if (jumpInput && (isTouchingWall || isTouchingWallBack)) {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        } // in air to jump state transition
        else if (jumpInput && player.JumpState.CanJump()) { // if we hit jump and still have jumps left, switch to jump state
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        } // in air to wall grab transition 
        else if (isTouchingWall && grabInput) { // we are hitting a wall && it's in the direction we are moving && we are not actively jumpint
            stateMachine.ChangeState(player.WallGrabState);
        } // in air to grapple state
        else if (grappleInput) {
            stateMachine.ChangeState(player.GrappleState);
        }
        else { // if no state transitions, fall accordingly
            // face correct direction && move left/right
            player.CheckIfShouldFlip(xInput);
            if (xInput != 0){
                if (player.inLobby) {
                    player.SetVelocityX(playerData.movementVelocityLobby * xInput);
                } else {
                    player.SetVelocityX(playerData.movementVelocity * xInput);
                }
            }
        

            // update values in animator
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier() {
        if (isJumping) {
            if (jumpInputStop) {
                // once we stop pressing down jump input, we will stop accelerating to the full jump force
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f) {
                // if we are now falling after jumping to peak, then set isJumping to false
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime() {
        // once we are past our coyote time interval, then we will take away the jump ability
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime) {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() {
        coyoteTime = true;
    }

    public void SetIsJumping() {
        isJumping = true;
    }
}
