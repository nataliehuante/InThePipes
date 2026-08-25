using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected string animBoolName;

    protected float startTime; 


    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName) {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        DoChecks();
    }

    public virtual void Enter() {
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
        // Debug.Log("Rat " + animBoolName);
    }

    public virtual void Exit() {
        entity.animator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate() {
        DoChecks();
    }

    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() {
        
    }

    public void printStateName() {
        //Debug.Log("Enemy " + animBoolName);
    }
}
