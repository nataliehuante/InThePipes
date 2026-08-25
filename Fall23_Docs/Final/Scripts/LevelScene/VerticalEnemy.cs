/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages an enemy with a vertical movement range.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : MonoBehaviour
{
    // public variables
    [Header("Movement")]
    public float distanceToCover; // how much to move in either direction **the enemy should start in the center of its range**
    public float speed; // flying enemies will always move at a constant speed

    // private variables
    private bool invertStartDirection; 
    private Vector3 startingPosition;
    private LevelsSounds levelsSounds;
    private PlayerController player;
    private bool holdingEnemySoundLock = false;
    
    
    void Start()
    {
        // references assignments
        startingPosition = transform.position;
        levelsSounds = FindObjectOfType<LevelsSounds>();
        player = FindObjectOfType<PlayerController>();

        // randomize if the enemy will move up or down first
        if(Random.value<0.5f)
            invertStartDirection = true;
        else
            invertStartDirection = false;
    }
    
    void Update()
    {
        // vertically-moving enemies always follow the same movement pattern
        move();

        // only if the player is within range, play the enemy sound
        if (playerWithinRange()) {
            playEnemySound();
        }
        else {
            pauseEnemySound();
        }


    }

    private void move() {
        Vector3 y = startingPosition;

        // if inverted, enemy will move down first
        if(invertStartDirection){
            y.y += distanceToCover * Mathf.Sin(Time.time * speed);
        }
        else { 
            y.y += distanceToCover * Mathf.Cos(Time.time * speed);
        }
        
        transform.position = y;
    }


    // returns true if the player is within the enemy's attack range
    private bool playerWithinRange() {
        // if within the X range in which the enemy is moving and is within a reasonable Y range (will be useful for stacked floors)
        if (((float)Mathf.Abs(player.transform.position.y - startingPosition.y) <= (float)distanceToCover) && 
            ((float)Mathf.Abs(player.transform.position.x - transform.position.x) <= (float)3)) 
        {
            return true;
        }
        else {
            return false;
        }
    }

    // plays the enemy sound, if the audio source is free
    private void playEnemySound() {
        // play the corresponding enemy animal sound
        if (!player.enemyNearbySoundLock) { // if no one is holding the lock 
            player.enemyNearbySoundLock = true; // hold the lock
            holdingEnemySoundLock = true; // take note that we are holding the lock
            levelsSounds.PlayNearbyEnemySound(gameObject.tag); // play our enemy sound
        }
    }

    // pauses the enemy sound, if we are currently playing 
    private void pauseEnemySound() {
        // if we are currently playing our enemy sound, stop playing and release the lock
        if (holdingEnemySoundLock) {
            levelsSounds.PauseNearbyEnemySound();
            player.enemyNearbySoundLock = false; 
            holdingEnemySoundLock = false; 
        }
    }
    
}
