using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {

    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        if (player.inLobby) {
            player.SetVelocityY(-playerData.wallSlideVelocityLobby);
        } else {
            player.SetVelocityY(-playerData.wallSlideVelocity);
        }
        
        
        // wall slide to wall climb state
        if (!isExitingState) {
            if (yInput > 0) {
                stateMachine.ChangeState(player.WallClimbState);
            } // wall slide to wall grab state
            else if (yInput == 0) {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
        
    }
}
