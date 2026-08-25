using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    private bool landHard = false;

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {
        
    }

    public override void Enter() {
        base.Enter();
        player.SetVelocityX(0f);
        if (landHard) {
            if (player.inLobby)
                Instantiate(player.dirtPoof1_Lobby, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y - 0.15f, player.gameObject.transform.position.z), Quaternion.identity);
            else
                Instantiate(player.dirtPoof1, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y - 0.15f, player.gameObject.transform.position.z), Quaternion.identity);
        }
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        
        // land to move state
        if (!isExitingState) {
            if (xInput != 0){
                stateMachine.ChangeState(player.MoveState);
            } // land to idle state
            else if (isAnimationFinished) {
                stateMachine.ChangeState(player.IdleState);
            }
        }
        
    }

    public void SetLandHard() {
        landHard = true;
    }

    public void ResetLandHard() {
        landHard = false;
    }

}
