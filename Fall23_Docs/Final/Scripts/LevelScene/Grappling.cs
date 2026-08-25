/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the grappling (or swinging) mechanic of the player.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerController playerController;
    private LevelController levelController;

    public Transform cameraTransform; 
    public new Camera camera;
    public Transform raycastStartPoint; 
    public LayerMask whatIsGrappable;
    public GameObject player;
    public LineRenderer lineRenderer;
    public float overshootYAxis;

    [Header("Swinging")]
    private float maxSwingDistance = 10f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    
    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse0;

    

    private void Start() {
        playerController = GetComponent<PlayerController>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void Update() {
        if (Input.GetKeyDown(swingKey))
            StartSwing();
        if (Input.GetKeyUp(swingKey))
            StopSwing();
    }

    // draws the player's web from where they are swinging
    private void LateUpdate() {
        DrawRope();
    }


    // player swing mechanic
    private void StartSwing() {
        // isSwinging = true;
        playerController.activeSwing = true;
        

        // finds the mouse position and converts it to world space
        Vector3 currMousePos = Input.mousePosition;
        currMousePos.z = raycastStartPoint.position.z;
        currMousePos = camera.ScreenToWorldPoint(currMousePos);
        currMousePos.z = raycastStartPoint.position.z;

        RaycastHit hit;
        Vector3 swingDirection = (currMousePos - raycastStartPoint.position).normalized;
        if (Physics.Raycast(raycastStartPoint.position, swingDirection, out hit, maxSwingDistance, whatIsGrappable)) {
            // store it in grapplePoint
            swingPoint = hit.point;
            Vector3 currPlayerPosition = player.transform.position;

            // actually grapple now that we have a valid grapplePoint
            // Debug.Log("Ray hit grappable object, executing swing");
            
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;
            playerController.successfulSwing = true;

            float distanceFromPoint = Vector3.Distance(player.transform.position, swingPoint);

            // the distance grapple will try to keep from grapple point
            joint.maxDistance = 1f;
            joint.minDistance = 0.1f;

            // customize as you like
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 10f;

            lineRenderer.enabled = true;

            // face player the correct way
            if (swingPoint.x < player.transform.position.x) { // swing left
                playerController.FacePlayerLeft(true);
            }
            else { // swing right or swing hang
                playerController.FacePlayerLeft(false);
            }
        }
    }

    // stops swinging
    private void StopSwing() {
        lineRenderer.enabled = false;
        playerController.successfulSwing = false;
        playerController.activeSwing = false;
        Destroy(joint);
    }

    // draws the player's web using a lineRenderer
    private void DrawRope() {
        if (!joint)
            return;
        
        lineRenderer.SetPosition(0, raycastStartPoint.position);
        lineRenderer.SetPosition(1, swingPoint);
    }

}

