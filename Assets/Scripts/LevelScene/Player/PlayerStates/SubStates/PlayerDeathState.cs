using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public string methodOfDeath;
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {
        
    }

    public override void Enter() {
        base.Enter();
        
    }

    public override void Exit() {
        base.Exit();

    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        
        if (player.isDeathAnimationFinished) {
            player.levelController.OnPlayerLoseLevel(methodOfDeath);
        }
    }
} 
