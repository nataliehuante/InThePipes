/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages all functions that the Player might carry out. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // // public variables
    // public bool isAlive;
    // // public bool isInScrollableArea;
    // // public bool leftOfScrollableArea;
    // // public bool rightOfScrollableArea;
    // // public bool freeze = false;
    // // public bool activeSwing = false;
    // // public bool successfulSwing = false;
    // public SceneLoader sceneLoader;
    // // public Animator playerWalkAnim;
    // // public TidalWave tidalWave;
    // public LevelsSounds sounds;
    // public bool enemyNearbySoundLock = false;
    // // public bool hasMoved = false;

    // // private variables
    // // public float speed;
    // // public float maxSpeed;
    // // public float jumpForce;
    // private new Rigidbody2D rigidbody;
    // public int playerScore;
    // private LevelController levelController;
    // private InPlayGameView inPlayGameView;
    // private SpriteRenderer spriteRenderer;
    // // public bool onFloor = false;
    // // public bool hittingSideOfAWall;
    // // public GameObject player;


    // // public int jumpCount;
    // public int lives;
    // // private bool inWave;
    // // private bool playedWaveHurtSound = false;

    // // grappling variables
    // // public bool isSwinging = false;
    // // public Vector2 ropeHook;
    // // public Transform ropeHook;
    // // public float swingForce = 4f;
    // public WebSystem webSystem;



    // // Start is called before the first frame update
    // void Start()
    // {
    //     // find references
    //     inPlayGameView = FindObjectOfType<InPlayGameView>();
    //     rigidbody = GetComponent<Rigidbody2D>();
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     sceneLoader = FindObjectOfType<SceneLoader>();
    //     levelController = FindObjectOfType<LevelController>();
    //     sounds = FindObjectOfType<LevelsSounds>();
    //     webSystem = FindObjectOfType<WebSystem>();

    //     // assign variables
    //     isAlive = true;
    //     speed = 2;
    //     jumpForce = 30;
    //     lives = 3;
    //     jumpCount = 0;
    //     // inWave = false;
    //     maxSpeed = 3;

    //     isInScrollableArea = false;
    //     leftOfScrollableArea = true;
    //     rightOfScrollableArea = false;
    // }

	// void Update()
	// {
    //     playerWalkAnim.speed = 0;

    //     // if player is alive, move and jump as normal
    //     if (isAlive) {
    //         listenForMovementInput();
    //         listenForJump();	
    //     }	
	// }

    // // called if the player is caught in the wave, spins player in circles
    // private void SweepAndDrag() {
    //     // if we haven't played the hurt sound yet, play it
    //     if (!playedWaveHurtSound) {
    //         sounds.PlayHurtSound(0.5f);
    //         playedWaveHurtSound = true;
    //     }

    //     // spin the player
    //     player.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z + 15);
        
    // }

    // // listen for player movement input
    // private void listenForMovementInput() {
    //     // if in active swing, don't move or jump
    //     if (isSwinging && (!onFloor)) {
    //         jumpCount = 2;

    //         var playerToHookDirection = (ropeHook.position - transform.position).normalized;

    //         Vector3 perpendicularDirection;
    //         float horizontalInput = Input.GetAxis("Horizontal");
    //         if (horizontalInput < 0)
    //         {
    //             perpendicularDirection = new Vector3(-playerToHookDirection.y, playerToHookDirection.x, playerToHookDirection.z);
    //             var leftPerpPos = (Vector3)transform.position - perpendicularDirection * -2f;
    //             Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
    //         }
    //         else
    //         {
    //             perpendicularDirection = new Vector3(playerToHookDirection.y, -playerToHookDirection.x, playerToHookDirection.z);
    //             var rightPerpPos = (Vector3)transform.position + perpendicularDirection * 2f;
    //             Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
    //         }

    //         var force = perpendicularDirection * swingForce;
    //         rigidbody.AddForce(force, ForceMode2D.Force);

    //     }
    //     else if (jumpCount == 2){ // in the air but not swinging
    //     }
    //     else if (!isSwinging){  // if not swinging and not in the air after swinging, listen for movement input
    //         // left and right movement
    //         float h = Input.GetAxis("Horizontal") * speed;

    //         if (h < 0) { // walk left 
    //             if (hasMoved == false)
    //                 hasMoved = true;
    //             FacePlayerLeft(true);
    //             if (jumpCount == 0) // if not in the air bc of jumping
    //                 playerWalkAnim.speed = 1;
    //             if (rigidbody.velocity.x > (-maxSpeed))
    //                 rigidbody.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
    //         }
    //         else if (h > 0) { // walk right
    //             if (hasMoved == false)
    //                 hasMoved = true;
    //             FacePlayerLeft(false);
    //             if (jumpCount == 0) // if not in the air bc of jumping
    //                 playerWalkAnim.speed = 1;
    //             if (rigidbody.velocity.x < maxSpeed) 
    //                 rigidbody.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
    //         }
    //         else { // idle
    //             playerWalkAnim.speed = 0;
    //         }

    //     }


    // }

    // // listens for player to jump
    // private void listenForJump() {
    //     // listen for jump
    //     if (Input.GetKeyDown(KeyCode.Space) && ((jumpCount == 0) || onFloor) && (!activeSwing))
    //     {   // jump
    //         rigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    //         sounds.PlayJumpSound();
    //         jumpCount++;
    //     }
    // }

    // // faces the player left
    // public void FacePlayerLeft(bool faceLeft) {
    //     if (faceLeft)
    //         spriteRenderer.flipX = true;
    //     else 
    //         spriteRenderer.flipX = false;
    // }


    // // Handles player collisions with other objects
    // void OnCollisionEnter2D(Collision2D collision) {
    //     switch (collision.gameObject.tag) {
    //         case "Floor":
    //             resetJumpCount();
    //             webSystem.isColliding = true;
    //             onFloor = true;
    //             break;
    //         case "Ceiling":
    //             webSystem.isColliding = true;
    //             break;
    //         case "Prop":
    //             resetJumpCount();
    //             break;
    //         case "Rat":
    //             loseALife(false, 1, true);
    //             break;
    //         case "Bat":
    //             loseALife(false, 8, true);
    //             break;
    //         case "Snake":
    //             loseALife(false, 2, true);
    //             break;
    //         case "Croc":
    //             loseALife(false, 3, true);
    //             resetJumpCount();
    //             break;
    //     }
    // }

    // // handles player no longer colliding with another object 
    // void OnCollisionExit2D(Collision2D collision) {
    //     switch (collision.gameObject.tag) {
    //         case "Floor":
    //             onFloor = false;
    //             webSystem.isColliding = false;
    //             break;
    //         case "Ceiling":
    //             webSystem.isColliding = false;
    //             break;
    //     }
    // }

    // // handles player entering a trigger box
    // void OnTriggerEnter2D(Collider2D collision) {
    //     switch(collision.gameObject.tag) {
    //         case "CameraScrollTrigger":
    //             isInScrollableArea = false;
    //             leftOfScrollableArea = true;
    //             break;
    //         case "CameraStopScrollingTrigger":
    //             isInScrollableArea = false;
    //             rightOfScrollableArea = true;
    //             break;
    //         case "CompleteLevelTrigger":
    //             levelController.GoToNextLevel();
    //             break;
    //         case "PlayerFell":
    //             loseALife(true, 6, false);
    //             break;
    //         case "Water":
    //             loseALife(true, 7, false);
    //             break;
    //         case "ShortWater":
    //             loseALife(false, 7, true);
    //             break;
    //         case "Coin":
    //             collectCoin();
    //             break;
    //         case "TidalWave":
    //             StartCoroutine(ScrollTidalWave());
    //             break;
            
    //     }
    // }

    // // called when the player enters the tidal wave. will spin the player and wait a few seconds before sending the system the alert that the player has lost
    // public IEnumerator ScrollTidalWave()
    // {
    //     print("hit");
    //     rigidbody.isKinematic = true; // don't allow any player movement, only spinning
    //     isAlive = false; // we are dead
    //     // inWave = true; // remember we are in the wave
    //     tidalWave.speed = 6; // speed up the wave so we can see it fill the screen
    //     yield return new WaitForSeconds(2.5f);
    //     loseALife(true, 7, false); // send the level controller the signal the player has lost
    // }

    // // handles the player exiting a trigger box
    // void OnTriggerExit2D(Collider2D collision) {
    //     switch(collision.gameObject.tag) {
    //         case "CameraScrollTrigger":
    //             isInScrollableArea = true;
    //             leftOfScrollableArea = false;
    //             break;
    //         case "CameraStopScrollingTrigger":
    //             isInScrollableArea = true;
    //             rightOfScrollableArea = false;
    //             break;
    //     }
    // }

    // // resets jump count
    // private void resetJumpCount() {
    //     if (jumpCount > 0){
    //             jumpCount = 0;
    //         }
    // }

    // // handles the player losing a life based on how they received damage
    // private void loseALife(bool instantDeath, int methodOfLoss, bool showHurtFlash) {
    //     sounds.PlayHurtSound(0.5f);

    //     // if player falls or in wave, player loses level
    //     if (instantDeath)
    //         lives = 0;

    //     // decrement the life count or lose level depending on method of loss
    //     lives--;

    //     // show red damage flash if necessary
    //     if (showHurtFlash) {
    //         inPlayGameView.ShowPlayerHurtCanvas();
    //     }

    //     if (lives <= 0) {// if no lives left, player loses level
    //         levelController.OnPlayerLoseLevel(methodOfLoss);
    //     }
    //     else { // if still alive, update lives sprite
    //         inPlayGameView.updateLives(lives);
    //     }

    // }

    // // player collects a coin
    // private void collectCoin() {
    //     playerScore = playerScore + 10;
    //     inPlayGameView.setScoreText(playerScore);
    //     sounds.PlayPickupCoinSound();
    // }
}
