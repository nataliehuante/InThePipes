using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{

    private int wallJumpDirection; 

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }
    
    public override void Enter() {
        base.Enter();
        // reset jump count
        player.JumpState.ResetAmountOfJumpsLeft();
        if (player.inLobby) {
            player.SetVelocity(playerData.wallJumpVelocityLobby, playerData.wallJumpAngle, wallJumpDirection);
        } else {
            // jump at an angle
            player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        }
        // make sure we are facing the correct direction
        player.CheckIfShouldFlip(wallJumpDirection);
        // decrease jump count (ex: for in case we want to double jump off a wall jump)
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // pass through velocities to our animator
        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
     
        // check if our wall jump ability is done yet or not
        if (Time.time >= startTime + playerData.wallJumpTime){
            if (!isAbilityDone) {
                // Debug.Log("wall jump ability done");
                isAbilityDone = true;
            }
            
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall) {
        if (isTouchingWall) {
            wallJumpDirection = -player.FacingDirection;
        }
        else {
            wallJumpDirection =   player.FacingDirection;
        }
    }

}
