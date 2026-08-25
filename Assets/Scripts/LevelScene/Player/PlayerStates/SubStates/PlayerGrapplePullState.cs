using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapplePullState : PlayerAbilityState
{
    public PlayerGrapplePullState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
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
        if (!player.InputHandler.GrapplePullInput) {
            isAbilityDone = true;
            return;
        }

        // handle grappling through web system
        player.webSystem.HandleInput();
    }
}
