using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set;}
    public int NormInputX { get; private set;}
    public int NormInputY { get; private set;}
    public bool JumpInput { get; private set;}
    public bool JumpInputStop { get; private set;}
    public bool GrabInput { get; private set;}
    public bool GrappleInput { get; private set;}
    public bool GrapplePullInput { get; private set;}
    public bool ShootInput { get; private set;}
    public Vector2 MousePosition { get; private set;}



    [SerializeField]
    private float inputHoldTime = 0.3f;
    private float jumpInputStartTime;

    private LevelController levelController;

    private void Awake() {
        levelController = FindObjectOfType<LevelController>();
    }


    private void Update() {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        RawMovementInput = context.ReadValue<Vector2>();
        
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
    }

    public void OnVerticalMoveInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        RawMovementInput = context.ReadValue<Vector2>();
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        if(context.started) {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled) {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        if(context.started) {
            GrabInput = true;
        }

        if (context.canceled) {
            GrabInput = false;
        } 
    }

    public void UseJumpInput() {
        if (levelController.isPaused) {
            return;
        }
        JumpInput = false;
    } 

    private void CheckJumpInputHoldTime() {
        if (levelController.isPaused) {
                return;
            }
        

        if (Time.time >= jumpInputStartTime + inputHoldTime) {
            JumpInput = false;
        }
    }

    public void OnGrappleInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        if (context.started) {
            GrappleInput = true;
        }   

        if (context.canceled) {
            GrappleInput = false;
        }
    }

    public void OnGrapplePullInput(InputAction.CallbackContext context) {
        if (levelController.isPaused) {
            return;
        }

        if (context.started) {
            GrapplePullInput = true;
        }   

        if (context.canceled) {
            GrapplePullInput = false;
        }
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (levelController.isPaused)
        {
            return;
        }

        if (context.started)
        {
            ShootInput = true;
            StartCoroutine(ResetShootInput());
        }

        // if (context.canceled)
        // {
        //     ShootInput = false;
        // }
    }

    private IEnumerator ResetShootInput() {
        yield return new WaitForEndOfFrame();
        ShootInput = false;
    }

    public void OnAim(InputAction.CallbackContext context) {
        MousePosition = context.ReadValue<Vector2>();
    }
}
