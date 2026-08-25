using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public Player player;

    public Data_Entity entityData;
    
    public int facingDirection { get; private set; }     
    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    [SerializeField]
    private Transform wallCheck; 
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform biteCheck;
    private Vector2 velocityWorkspace;


    protected int healthPoints;

    public virtual void Start() {
        facingDirection = 1;
        
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        stateMachine = new FiniteStateMachine();


    }

    public virtual void Update() {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate() {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity) {
        velocityWorkspace.Set(facingDirection * velocity, rigidbody.velocity.y);
        rigidbody.velocity = velocityWorkspace;
    }

    public virtual void SetVelocityY(float velocity) {
        velocityWorkspace.Set(rigidbody.velocity.x, facingDirection * velocity);
        rigidbody.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall() {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckWallVertical() {
        return Physics2D.Raycast(wallCheck.position, transform.up, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge() {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAttackRange() {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAttackDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMinAttackRangeVertical() {
        return Physics2D.Raycast(playerCheck.position, transform.up, entityData.minAttackDistance, entityData.whatIsPlayer);
    }

    // public virtual bool CheckPlayerInMaxAttackRange() {
    //     return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAttackDistance, entityData.whatIsPlayer);
    // }

    public virtual bool CheckPlayerInBiteRange() {
        return Physics2D.Raycast(biteCheck.position, transform.right, entityData.biteDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInBiteRangeVertical() {
        return Physics2D.Raycast(biteCheck.position, transform.up, entityData.biteDistance, entityData.whatIsPlayer);
    }

    public virtual void Flip() {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void FlipVertical() {
        facingDirection *= -1;
        transform.Rotate(180f, 0f, 0f);
        toggleSpriteYDirection();
    }

    public virtual void toggleSpriteYDirection() {
        if (spriteRenderer.flipY == true) {
            spriteRenderer.flipY = false;
        } else {
            spriteRenderer.flipY = true;
        }
    }

    public virtual void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.up * entityData.wallCheckDistance));

    }

    public virtual void bitePlayer(string enemyType) {
        player.getBitten(enemyType);
        // player.applyKnockback(gameObject.transform.position);
    }
    
    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Bullet")
        {
            healthPoints--;
        }
    } 



}
