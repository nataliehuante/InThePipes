using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector3 holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
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
    }

    public override void Enter() {
        base.Enter();

        holdPosition = player.transform.position;
        HoldPosition();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        HoldPosition();

        if (!isExitingState) {
            // wall grab to wall climb state
            if (yInput > 0) {
                stateMachine.ChangeState(player.WallClimbState);
            } else if (yInput < 0) { // wall grab to wall slide state
                stateMachine.ChangeState(player.WallSlideState);
            } else if (!grabInput) { // wall grab to in air state
                stateMachine.ChangeState(player.InAirState);
            }
        }
        
    }

    private void HoldPosition() {
        player.transform.position = holdPosition;

        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }
    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
