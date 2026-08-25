using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{   
    [Header("Move State")]
    public float movementVelocity = 10f;
    public float movementVelocityLobby = 5f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;
    public float jumpVelocityLobby = 5f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;
    public float wallSlideVelocityLobby = 1f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;
    public float wallClimbVelocityLobby = 1f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 15f;
    // will make you have to wait a slight moment before being able to move back to the wall in the in air state
    public float wallJumpVelocityLobby = 1f;
    public float wallJumpTime = 0.4f; 
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsGroundLobby;

    [Header("Player Stats")]
    public int lives = 3;
    public int score = 0;

    [Header("Camera Variables")]
    [Range(0,1)] // default
    public float defaultSmoothTime; 
    [Range(0,1)] // a bit faster when falling
    public float fallSmoothTime; 
    [Range(0,1)] // a bit faster when swinging
    public float swingSmoothTime; 

    [Header("Level Select Variables")]
    public bool startAtDifferentLevel = false;
    public int levelToStartAt = 1;

    [Header("Grappling Variables")]
    public float maxRopeCastDistance = 20f;
    public float shortenedRopeCastDistance = 5f;
}
