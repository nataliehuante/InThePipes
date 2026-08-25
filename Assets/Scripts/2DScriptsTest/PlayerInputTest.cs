using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTest : MonoBehaviour
{
    Vector2 _Movement;
    Rigidbody2D _Rigidbody;
    public float speed;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Animator playerWalkAnim;
    public bool isAlive;
    public int jumpCount;
    public float jumpForce;

    private void Awake() {
        _Rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        jumpCount = 0;
        isAlive = true;
        // jumpForce = 5;
        spriteRenderer.flipX = false;
    }

    private void Update() {
        if (isAlive) {
            listenForMovementInput();
            listenForJump();	
        }	
    }

    // faces the player left
    public void FacePlayerLeft(bool faceLeft) {
        if (faceLeft)
            spriteRenderer.flipX = true;
        else 
            spriteRenderer.flipX = false;
    }

    private void listenForMovementInput() {
        float h = Input.GetAxis("Horizontal") * speed;

            if (h < 0) { // walk left 
                FacePlayerLeft(true);
                if (jumpCount == 0) // if not in the air bc of jumping
                    playerWalkAnim.speed = 1;
                if (_Rigidbody.velocity.x > (-maxSpeed))
                    _Rigidbody.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
            else if (h > 0) { // walk right
                FacePlayerLeft(false);
                if (jumpCount == 0) // if not in the air bc of jumping
                    playerWalkAnim.speed = 1;
                if (_Rigidbody.velocity.x < maxSpeed)
                    _Rigidbody.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            }
            else { // idle
                playerWalkAnim.speed = 0;
                spriteRenderer.sprite = idleSprite;
            }
    }

    private void listenForJump() {
        // listen for jump
        if (Input.GetKeyDown(KeyCode.Space) && ((jumpCount == 0)))
        {   // jump
            _Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // sounds.PlayJumpSound();
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case "Floor":
                resetJumpCount();
                break;
        }
    }
    
    // resets jump count
    private void resetJumpCount() {
        if (jumpCount > 0){
                jumpCount = 0;
            }
    }
}
