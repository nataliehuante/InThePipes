using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Entity
{
    #region States
    public Bat_IdleState idleState { get; private set; }
    public Bat_MoveState moveState { get; private set; }
    public Bat_PlayerDetectedState playerDetectedState { get; private set; }
    public Bat_ChargeState chargeState { get; private set; }
    public Bat_AttackState attackState { get; private set; }
    public Bat_HurtState hurtState { get; private set; }
    public Bat_DeathState deathState { get; private set; }
    #endregion

    #region Data Objects
    [SerializeField] 
    private Data_IdleState idleStateData; 
    [SerializeField] 
    private Data_MoveState moveStateData; 
    [SerializeField]
    private Data_PlayerDetectedState playerDetectedData;
    [SerializeField]
    private Data_ChargeState chargeStateData;
    [SerializeField]
    private Data_AttackState attackStateData;
    private SpriteRenderer spriteRenderer;
    #endregion



    public override void Start() {
        base.Start();

        moveState = new Bat_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Bat_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Bat_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Bat_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        attackState = new Bat_AttackState(this, stateMachine, "attack", attackStateData, this);
        hurtState = new Bat_HurtState(this, stateMachine, "hurt", this);
        deathState = new Bat_DeathState(this, stateMachine, "death", this);

        stateMachine.Initialize(moveState);

        healthPoints = 3;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        if (col.collider.tag == "Bullet")
        {
            if (healthPoints <= 0)
            {
                stateMachine.ChangeState(deathState);
            }
            else
            {
                stateMachine.ChangeState(hurtState);
            }
        }
    }

    public IEnumerator fadeOut(float duration) {
        float counter = 0f;
        Color spriteColor = spriteRenderer.material.color;
        while (counter < duration) {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter/duration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }
    }

    public void startFadeOut(float duration) {
        StartCoroutine(fadeOut(duration));
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
