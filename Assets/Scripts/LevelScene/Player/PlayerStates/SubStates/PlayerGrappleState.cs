using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleState : PlayerAbilityState
{
    protected int yInput;
    private int xMovement;

    public PlayerGrappleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();

        player.webSystem.ResetRope();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // if no grapple input, end ability
        if (!player.InputHandler.GrappleInput) {
            isAbilityDone = true;
            return;
        }

        yInput = player.InputHandler.NormInputY;

        // handle grappling through web system
        player.webSystem.HandleInput();
        player.webSystem.HandleRopeLength(yInput);
        player.CheckIfShouldFlip((int)player.transform.position.x - xMovement);
        xMovement = (int)player.transform.position.x;
    }


}
