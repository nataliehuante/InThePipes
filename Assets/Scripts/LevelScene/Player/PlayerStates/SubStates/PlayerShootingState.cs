using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingState : PlayerAbilityState
{
    private float shotCooldownLength = 1f;
    private bool shotOnCooldown = false;

    private float timeLastShotFired;
    
    public PlayerShootingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) 
    {
    }
    
    public override void Enter() {
        base.Enter();
        timeLastShotFired = this.startTime;
    }

    public override void Exit() {
        base.Exit();
        shotOnCooldown = false;
    }

    private IEnumerator ResetShootInputAfterDelay(float delay) {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        shotOnCooldown = false;
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();
        
        if (shotOnCooldown == false)
        {
            timeLastShotFired = Time.time;
            //TODO implement shooting from player file
            player.InstantiateShot();
            player.sounds.PlayShootSound();
            
            shotOnCooldown = true;
        } else if (shotOnCooldown == true && Time.time - timeLastShotFired >= shotCooldownLength)
        {
            shotOnCooldown = false;
        }
        if (this.player.InputHandler.ShootInput == false)
        {
            isAbilityDone = true;
            return;
        }
    }
}
