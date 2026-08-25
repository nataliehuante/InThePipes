using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class WebSystem : MonoBehaviour
{
    [Header("Grapple Variables")]
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;

    [Header("Crosshair Variables")]
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;

    [Header("Components")]
    public Player playerMovement;
    private Rigidbody2D playerRigidbody;
    private Rigidbody2D ropeHingeAnchorRb;
    public SpriteRenderer ropeHingeAnchorSprite;
    public LineRenderer ropeRenderer;
    public WebAnimator webAnimator;

    [Header("Other Variables")]
    private bool ropeAttached;
    private Vector2 playerPosition;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;
    public bool isColliding;
    public Vector2 aimDirection; 
    private string grappleType;
    private bool playerChangedDistance;
    private float prevFrameAngle = 90.0f; //for determining when to add more force artificially to increase speed
    private LevelsSounds sounds;
    // public bool disableAllAbility = false;

    [Header("Need to be moved to playerData")]
    public LayerMask webSwingLayerMask; // what we can grapple swing from 
    public LayerMask webPullLayerMask; // what we can grapple pull from 
    public float ropeMaxCastDistance; // max distance we can grapple from 
    public float climbSpeed; // the speed at which we will climb from WS input
    public float distanceToPullTo; // the distance we will pull to when grapple pulling
    private float distanceToPullToForSwing; // the distance we will pull to when grapple swinging

    public float climbSpeedForPull; // the climb speed when pulling to distanceToPullTo
    public float climbSpeedForSwing; // the climb speed when pulling to distanceToPullToForSwing
    public float distanceDifferencePullForSwing;

    public float climbSpeedLobby;
    public float climbSpeedForPullLobby; // the climb speed when pulling to distanceToPullTo
    public float climbSpeedForSwingLobby; // the climb speed when pulling to distanceToPullToForSwing
    public float distanceDifferencePullForSwingLobby;

    [Header("Web Colors Variables")]
    public List<Material> webColors = new List<Material>(); 
    public List<Sprite> webAnchors = new List<Sprite>();


    float maxSwingVelocity = 4;
    // void FixedUpdate() {
    //     playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
    // }

    void Awake()
    {
        // set variables
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        playerChangedDistance = false;
        sounds = FindObjectOfType<LevelsSounds>();
        playerRigidbody = playerMovement.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerMovement.disableAllMovement)
            return;
        // calculations needed 

        // get mouse position 
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        if (playerMovement.inLobby) {
            mousePosition.z = 15f;
        } else {
            mousePosition.z = 10f;
        }
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // get facing direction
        var facingDirection = mousePosition - transform.position;
        // get our aim angle
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }
        // get our aim direction
        aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        // get the player's position
        playerPosition = transform.position;

        // if we are not currently grappling, set the crosshair's position
        if (!ropeAttached)
        {
            playerMovement.isSwinging = false;
            if ((GlobalVariables.showUI) && (Time.timeScale != 0)) {
                if (!crosshairSprite.enabled)
                {
                    crosshairSprite.enabled = true;
                }

                crosshair.transform.position = mousePosition;
            }
            
        }
        else
        { // if we are currently grappling, hide the crosshair
            playerMovement.isSwinging = true;
            crosshairSprite.enabled = false;
        }
        
        // update rope positions
        UpdateRopePositions();
    }

    public void updateWebColor(int webIndex) {
        ropeRenderer.material = webColors[webIndex];
        ropeHingeAnchorSprite.sprite =webAnchors[webIndex];
    }
    private void SetCrosshairPosition(float aimAngle)
    {
        // enable the crosshair sprite
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        // calculate the x and y positions
        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        // set the cross hair position
        var crossHairPosition = new Vector3(x, y, -10);
        crosshair.transform.position = crossHairPosition;
    }

    // handle grappling input
    public void HandleInput()
    {

        // stores which to do first if both raycats return hits
        bool doGrapplePull = false;
        bool doGrappleSwing = false;

        // enable the line renderer of the web 
        ropeRenderer.enabled = true;

        // raycasts
        var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, webSwingLayerMask);
        var hitPull = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, webPullLayerMask);
        
        // if we are already swinging, rappel to the correct distance and then return
        if (playerMovement.isSwinging)
        {
            if (grappleType == "Pull") {
                if ((ropeJoint.distance > distanceToPullTo) && (!playerChangedDistance)) {
                    // Debug.Log("Rappeling Up");
                    if (playerMovement.inLobby && !isColliding) {
                        ropeJoint.distance -= Time.deltaTime * climbSpeedForPullLobby;
                    } else if (!isColliding) {
                        ropeJoint.distance -= Time.deltaTime * climbSpeedForPull;
                    }
                }
            } else if (grappleType == "Swing") {
                // Do not do anything inside lobby, since scaling is different.
                if (playerMovement.inLobby)
                {
                    return; 
                }
                
                //Debug.Log("Player position: " + playerPosition);
                //Debug.Log("Hinge Anchor: " + grappleCenter);
                //Debug.Log("Angle: " + Mathf.Atan2(normAngle.y, normAngle.x) * Mathf.Rad2Deg);
                Vector3 playerPosition = playerMovement.GetComponent<Transform>().position;
                Vector3 grappleCenter = ropeHingeAnchor.GetComponent<Transform>().position;
                Vector3 normAngle = (grappleCenter - playerPosition).normalized;
                float angleInDeg = Mathf.Atan2(normAngle.y, normAngle.x) * Mathf.Rad2Deg;

                Vector2 perpendicularDirection = Vector2.zero;
                //Get Perpendicular angle to apply force for maximum effect
                if (angleInDeg > 90.0f && prevFrameAngle > angleInDeg) //Left side of center and moving right
                {
                    perpendicularDirection = new Vector2(-normAngle.y, normAngle.x);
                }
                else if (angleInDeg < 90.0f && angleInDeg > prevFrameAngle)
                {
                    perpendicularDirection = new Vector2(normAngle.y, -normAngle.x);
                }

                if (playerRigidbody.velocity.magnitude < maxSwingVelocity)
                    playerRigidbody.AddForce(perpendicularDirection * 4f, ForceMode2D.Force);   
                prevFrameAngle = angleInDeg;
                
            }
            return;
        }
        

        // IF WE HIT BOTH RAYCASTS, PRIORITIZE THE CLOSESTS HITPOINT
        if ( (hitPull.collider != null) && (hit.collider != null)) {
            float grappleDistance = Vector2.Distance(playerPosition, hitPull.point);
            float swingDistance = Vector2.Distance(playerPosition, hit.point);

            if (grappleDistance <= swingDistance) {
                doGrapplePull = true;
            } else {
                doGrappleSwing = true;
            }
        }


        // GRAPPLE PULL - if we hit something we can grapple pull from, set up joint accordingly
        if ( (hitPull.collider != null) && (doGrapplePull || (hit.collider == null)) ) {
            // debugging logs
            // Debug.Log(hitPull.point);
            // Debug.Log("Grapple Pulling");

            // indicate we have the web attached
            ropeAttached = true;

            // attach our grapple point 
            if (!ropePositions.Contains(hitPull.point))
            {
                // Jump slightly to distance the player a little from the ground after grappling to something.
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);

                // add grapple point 
                ropePositions.Add(hitPull.point);
                ropeJoint.distance = Vector2.Distance(playerPosition, hitPull.point);
                ropeJoint.enabled = true;
                ropeHingeAnchorSprite.enabled = true;
            }

            // set grapple type
            grappleType = "Pull";
            sounds.PlaySwingingSound();

            // update player animator 
            playerMovement.setAnimatorForSwing();
            
        } 
        // GRAPPLE SWING - if we hit something we can swing from, set up joint accordingly
        else if ( (hit.collider != null) && (doGrappleSwing || (hitPull.collider == null)) ) {
            // debugging logs
            // Debug.Log(hit.point);
            // Debug.Log("Grapple Swinging");

            // indicate we have the web attached
            ropeAttached = true;

            // attach our grapple point
            if (!ropePositions.Contains(hit.point))
            {
                // Jump slightly to distance the player a little from the ground after grappling to something.
                // transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                ropePositions.Add(hit.point);
                // Debug.Log(hit.point);
                ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                ropeJoint.enabled = true;
                ropeHingeAnchorSprite.enabled = true;
                distanceToPullToForSwing = ropeJoint.distance - distanceDifferencePullForSwing;
            }

            // set grapple type 
            grappleType = "Swing";
            sounds.PlaySwingingSound();

            // update player animator 
            playerMovement.setAnimatorForSwing();
        } 
        else // if we hit NEITHER
        {
            // reset our variables
            ropeRenderer.enabled = false;
            ropeAttached = false;
            ropeJoint.enabled = false;
            grappleType = "";
        }
    }

    // stop swinging and reset our web components
    public void ResetRope()
    {
        // Debug.Log("Resetting Web System...");
        ropeJoint.enabled = false;

        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
        playerChangedDistance = false;
        playerMovement.resetAnimatorSpeed();
    }

    private void UpdateRopePositions()
    {
        // if we are not grappling, return
        if (!ropeAttached)
        {
            return;
        }

        // set the rope's line renderer vertex count to whatever number of positions are stored plus 1 (for the player's position)
        ropeRenderer.positionCount = ropePositions.Count + 1;

        // loop backwards through the ropePositions list and set the line renderer's vertexes to each point
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, new Vector3(ropePositions[i][0], ropePositions[i][1], -10.1f));
                    
                // set the rope anchor to the second to last position in the list
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    // change our raycasted position to a 3d vector with the correct z-axis value 
                    var ropePosition = new Vector3(ropePositions[ropePositions.Count - 1][0], 
                                                    ropePositions[ropePositions.Count - 1][1], -10.2f);
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // this case handles when the rope position being looped over is the second to last one
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    // var ropePosition = ropePositions.Last();
                    var ropePosition = new Vector3(ropePositions.Last()[0], 
                                                    ropePositions.Last()[1], -10.1f);
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // handles setting the rope's last vertex position to the player's current position
                ropeRenderer.SetPosition(i, new Vector3(transform.position.x, transform.position.y, -9.995f));
            }
        }
    }

    public void HandleRopeLength(int yInput)
    {
        
        /* to move up or down rope we must have the following
            - player must be inputting to go up / down
            - we must currenly have a rope/web attached to us (aka we must be currently grappling)
            - we must not be colliding with any walls in the map
            - at the top: we will always be at least 1 unit away from the ceiling
        */

        // rappel up
        if ( (yInput >= 1) && ropeAttached && !isColliding && (ropeJoint.distance >= 1) ) {
            // var tempDistance = ropeJoint.distance - Time.deltaTime * climbSpeed;
            // distanceToPullToForSwing = tempDistance;
            // ropeJoint.distance = tempDistance;
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
            playerChangedDistance = true;
        } // rappel down
        else if ( (yInput < 0) && ropeAttached && !isColliding ) {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
            playerChangedDistance = true;
        }
    } 



}
