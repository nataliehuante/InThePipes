/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file handles the camera's movement. It will follow the player through the level unless 
it is at the start or end of the level, in which case it will return to a fixed point. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{ 
    // public variables 
    public PlayerController playerController; // player references
    public GameObject player; // player references
    public new GameObject camera; // camera reference
    public float speed = 2000f; // camera speed

    // private variables
    private Vector3 levelStartCameraPosition; // left-most position allowed
    private Vector3 levelEndCameraPosition; // right-most position allowed
    public GameObject levelEndCameraSpot; // used to calculate the right-most position

    void Start() {
        // assign values to the start and end camera positions
        levelStartCameraPosition = camera.transform.position;
        levelEndCameraPosition = levelEndCameraSpot.transform.position;
    }

    void Update() {
        
        // if the camera should follow the player
        if ((playerController.isInScrollableArea) && (playerController.isAlive)) { 
            // follow the player
            camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }
        else { // if camera should not follow the player
            if (playerController.leftOfScrollableArea) { // and we're at the beginning of the level
                // lerp back to start position
                camera.transform.position = Vector3.Lerp(camera.transform.position, levelStartCameraPosition, speed);
            }
            else if (playerController.rightOfScrollableArea) { // and we're at the end of the level
                // lerp back to end position
                camera.transform.position = Vector3.Lerp(camera.transform.position, levelEndCameraPosition, speed);
            }   
        }
        
    }

}
