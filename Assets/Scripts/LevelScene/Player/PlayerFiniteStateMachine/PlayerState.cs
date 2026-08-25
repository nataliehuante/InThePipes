using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // references to other scripts
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected LevelsSounds sounds;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    // used to track how long we have been in a state
    protected float startTime;

    // will represent which animation should be playing in the state
    private string animBoolName;



    // constructor 
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    // called when we enter the state
    public virtual void Enter() {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;

        // Debug.Log(animBoolName);
        isAnimationFinished = false; 
        isExitingState = false;
    }

    // called when we exit the state
    public virtual void Exit() {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    // called when update is called every frame
    public virtual void LogicUpdate() {
        DoChecks();
    }

    // called when fixedUpdate is called every fixed update 
    public virtual void PhysicsUpdate() {
        // DoChecks();
    }

    // will be called in PhysicsUpdate() and from Enter()
    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
