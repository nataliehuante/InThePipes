using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // scene references
    protected Transform playerTransform;
    protected Player player;
    [SerializeField]
    private PlayerData playerData;

    // movement variables
    Vector3 velocity = Vector3.zero;
    protected float smoothTime = 0; // this is what we will pass into our movement
    protected float fallSmoothTime; 
    protected float defaultSmoothTime; 
    protected float swingSmoothTime;
    // public Vector2 xLimit_Level1Part1; 
    // public Vector2 xLimit_Level1Part2; 
    private Vector2 xLimit_toSet;
    private Vector2 yLimit_toSet;
    // public Vector2 yLimit;

    public Vector3 positionOffset = new Vector3(0, 0, -12);
    

    private void Awake() {
        // find references
        playerTransform = FindObjectOfType<Player>().transform;
        player = FindObjectOfType<Player>();

        // set variables from playerData
        defaultSmoothTime = playerData.defaultSmoothTime;
        fallSmoothTime = playerData.fallSmoothTime;
        swingSmoothTime = playerData.swingSmoothTime;
    }

    private void LateUpdate() {
        // update position offset to favor side player is facing
        updateFavorSide();

        // get the player's position 
        Vector3 targetPosition = playerTransform.position + positionOffset;
        

        // limit camera movement 
        if (player.levelController.inLobby) {
            xLimit_toSet = new Vector2(-3f, 19f);
            yLimit_toSet = new Vector2(0.8f, 9.2f);
        }
        else if (player.levelController.currentLevel == 1) {
            xLimit_toSet = new Vector2(1790f, 2065f);
            yLimit_toSet = new Vector2(-110f, -47.5f);
        }
        else if (player.levelController.currentLevel == 2) {
            xLimit_toSet = new Vector2(2290f, 2571.5f);
            yLimit_toSet = new Vector2(-110f, -47.5f);
        }
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit_toSet.x, xLimit_toSet.y), Mathf.Clamp(targetPosition.y, yLimit_toSet.x, yLimit_toSet.y), targetPosition.z);

        // update smoothTime based on player's movement
        updateSmoothTime();

        // move camera towards player's position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void updateSmoothTime() {
        if (player.isSwinging) { // if player is swinging
            smoothTime = swingSmoothTime;
        }
        else if (player.StateMachine.CurrentState == player.InAirState) { // if player is in air 
            smoothTime = swingSmoothTime;
        }
        else if (player.CurrentVelocity.y < 0) { // if player is falling 
            smoothTime = fallSmoothTime;
        }
        else { // default 
            smoothTime = defaultSmoothTime;
        }
    }

    private void updateFavorSide() {
        Vector3 newPositionOffset = new Vector3(positionOffset.x, positionOffset.y, positionOffset.z);
        if (player.FacingDirection == -1) { // facing left
            newPositionOffset.x = -0.5f;
        } else { // facing right
            newPositionOffset.x = 0.5f;
        }
        positionOffset = newPositionOffset;
    }
}
