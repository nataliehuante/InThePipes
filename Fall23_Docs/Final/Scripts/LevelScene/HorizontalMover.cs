/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages an enemy who is moving horizontally. It manages enemy mechanics such as attacking the player, moving back 
and forth within a set range, and playing its corresponding enemy sfx. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    // public variables
    [Header("Movement")]
    public float distanceToCover; // how much to move in either direction **the enemy should start in the center of its range**
    public float normalSpeed; // speed to move at when not attacking
    public bool movesRightFirst; 
    public bool originFaceLeft; // the sprite used faces left originally
   
    // private movement variables
    private float speed;
    private Vector3 startingPosition; 
    private Vector3 lastPosition;
    private Vector3 leftMostPosition;
    private Vector3 rightMostPosition; 
    private bool reachedFirstLimit = false;

    // attack variables
    private float attackSpeed;
    private bool holdingEnemySoundLock = false;

    // references
    private LevelController levelController;
    private SpriteRenderer spriteRenderer;
    private PlayerController player;
    private LevelsSounds levelsSounds;
    
    
    void Start()
    {
        // references assignments
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerController>();
        levelController = FindObjectOfType<LevelController>();
        levelsSounds = FindObjectOfType<LevelsSounds>();

        // assign variables starting values
        startingPosition = transform.position; // the starting position of the enemy
        attackSpeed = normalSpeed * 2f; // default attack speed is 2 times the normal speed
        
        // calculates both extremes of the enemy's movement range **this is why they must start in the center of the range**
        leftMostPosition = startingPosition; 
        leftMostPosition.x -= distanceToCover;

        rightMostPosition = startingPosition;
        rightMostPosition.x += distanceToCover;

        // randomize which direction to move first (avoids all enemies being synchronized in their movements)
        if(Random.value<0.5f)
            movesRightFirst = true;
        else
            movesRightFirst = false;

        // sets up the player to move in the right direction according to its randomized direction above
        if (!movesRightFirst) 
            reachedFirstLimit = true;

    }
    
    void Update()
    {
        // update the enemy's movement
        lastPosition = transform.position; // save the last position before updating

        // if the game is not paused, allow enemy movement
        if (!levelController.isPaused) {

            if (playerWithinSoundRange()) {
                // play the corresponding enemy animal sound
                if (!player.enemyNearbySoundLock) { // if no one is holding the lock 
                    player.enemyNearbySoundLock = true; // hold the lock
                    holdingEnemySoundLock = true; // take note that we are holding the lock
                    levelsSounds.PlayNearbyEnemySound(gameObject.tag); // play our enemy sound
                }
            }

            // check if the player is in range and attack
            if (playerWithinRange()) {
                // attack 
                attackPlayer();
            } 
            else { // player is not within attack range
                // if we are currently playing our enemy sound, stop playing and release the lock
                if (holdingEnemySoundLock) {
                    levelsSounds.PauseNearbyEnemySound();
                    player.enemyNearbySoundLock = false; 
                    holdingEnemySoundLock = false; 
                }
                // move normally
                move();
            }

            // having moved, face the correct direction
            Vector3 deltaPosition = transform.position - lastPosition;
            // face correct direction
            faceCorrectDirection(deltaPosition);
        }
    }

    // give the recent change in movement, face the sprite the correct direction
    private void faceCorrectDirection(Vector3 deltaPosition) {
        if (deltaPosition.x > 0) { // moving right
            if (originFaceLeft) // if sprite originally faces left, then flip
                spriteRenderer.flipX = true; 
            else // otherwise, leave it alone
                spriteRenderer.flipX = false;
        }
        else { // moving left
            if (originFaceLeft) // if sprite originally faces left, leave it
                spriteRenderer.flipX = false;
            else // otherwise, flip it
                spriteRenderer.flipX = true;
        }
    }

    // manages the movement of the enemy when not attacking
    private void move() {
        // reset the enemy's speed 
        speed = normalSpeed;
        
        if (transform.position.x > rightMostPosition.x) { // if we have managed to knock the enemy out of it's range to the right
            transform.position = rightMostPosition;
        }
        else if (transform.position.x < leftMostPosition.x) { // if we have managed to knock the enemy out of it's range to the left
            transform.position = leftMostPosition;
        }
        else if ((transform.position.x < rightMostPosition.x) && (!reachedFirstLimit)) { // if we are somewhere in the middle, move right
            transform.Translate(.005f * (float)speed, 0, 0);
        }
        else { // if we are somewhere in the middle and heading left OR we've reached the right limit, move left
            reachedFirstLimit = true; 
            transform.Translate(-.005f * (float)speed, 0, 0);
            if (transform.position.x <= leftMostPosition.x) {
                reachedFirstLimit = false;
            }
        }
    }

    // returns true if the player is within the enemy's attack range
    private bool playerWithinRange() {
        // if within the X range in which the enemy is moving and is within a reasonable Y range (will be useful for stacked floors)
        if (((float)Mathf.Abs(player.transform.position.x - startingPosition.x) <= (float)distanceToCover) && 
            ((float)Mathf.Abs(player.transform.position.y - transform.position.y) <= (float)1)) 
        {
            return true;
        }
        else {
            return false;
        }
    }

    // returns true if the player is within the enemy's attack range
    private bool playerWithinSoundRange() {
        // if within the X range in which the enemy is moving and is within a reasonable Y range (will be useful for stacked floors)
        if (((float)Mathf.Abs(player.transform.position.x - startingPosition.x) <= ((float)distanceToCover + 3f)) && 
            ((float)Mathf.Abs(player.transform.position.y - transform.position.y) <= (float)2)) 
        {
            return true;
        }
        else {
            return false;
        }
    }


    // manages the enemy's movement when attacking
    private void attackPlayer() {
        // increase the speed to the attack speed
        speed = attackSpeed;

        if (transform.position.x > rightMostPosition.x) { // if we have managed to knock the enemy out of it's range to the right
            transform.position = rightMostPosition;
        }
        else if (transform.position.x < leftMostPosition.x) { // if we have managed to knock the enemy out of it's range to the left
            transform.position = leftMostPosition;
        }
        else if (player.transform.position.x <= transform.position.x) { // if player on the left
            // move to the left
            transform.Translate(-.005f * (float)attackSpeed, 0, 0);
        }
        else { // if player on the right
            // move to the right
            transform.Translate(.005f * (float)attackSpeed, 0, 0);
        }
    }

    // flips the sprite in the opposite direction from which it is currently facing
    private void flipSprite() {
        if (spriteRenderer.flipX == true) {
            spriteRenderer.flipX = false;
        }
        else {
            spriteRenderer.flipX = true;
        }
    }

}
