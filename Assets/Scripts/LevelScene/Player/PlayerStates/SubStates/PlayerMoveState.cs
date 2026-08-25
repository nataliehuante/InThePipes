using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {
        
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        if (player.inLobby) {
            player.SetVelocityX(playerData.movementVelocityLobby * xInput);
        }
        else {
            player.SetVelocityX(playerData.movementVelocity * xInput);
        }

        if (Time.time >= startTime + 0.3f) {
            if (player.inLobby) 
                Instantiate(player.dirtPoof2_Lobby, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y - 0.115f, player.gameObject.transform.position.z), Quaternion.identity);
            else
                Instantiate(player.dirtPoof2, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y - 0.115f, player.gameObject.transform.position.z), Quaternion.identity);
            startTime = Time.time;
        }


        // move to idle state
        if (xInput == 0 && (!isExitingState)) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
