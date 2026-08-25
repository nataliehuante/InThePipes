using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_TriggerAnimation : MonoBehaviour
{
    // animator
    public Animator animator;

    // animations
    // public Animation outOfShell_Animation; 
    // public 
    public bool onlyOneAnimation;
    public bool hasThirdAnimation;
    private bool isPlayerInTrigger;
    private bool animationStarted;
    public bool animationDonePlaying;
    public TextMeshPro interactMessage;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerInTrigger = false;
        animationStarted = false;
        animationDonePlaying = false;

        // onlyOneAnimation = false;
        // hasThirdAnimation = false;

        animator = GetComponent<Animator>();
        animator.SetBool("animTwo", false);
        animator.SetBool("animThree", false);
        if (onlyOneAnimation)
            animator.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        // if the player is within the trigger and presses 'X'
        if (isPlayerInTrigger && (Input.GetKeyDown(KeyCode.X))) {
            // play the animation
            animationStarted = true;
            interactMessage.enabled = false;

            // if only one animation, play animation 
            if (onlyOneAnimation)
                animator.speed = 1f;
            else { // if more than one animation, play the second animation
                // animator.setBool("animOne")
                animator.SetBool("animTwo", true);
            }
        }

        // if the animation is done playing
        if (animationStarted && animationDonePlaying) {
            // reset variables
            animationStarted = false;
            animationDonePlaying = false;
            Debug.Log("animation done playing through");

            if (hasThirdAnimation) {
                animator.SetBool("animThree", true);
            }
        }
    }

    public void SetAnimationDone(string eventMessage) {
        if (eventMessage == "animationDone") {
            animationDonePlaying = true;
        }
    }


    void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "Player":
                isPlayerInTrigger = true;
                if ((!animationStarted) || (!animationDonePlaying))
                    interactMessage.enabled = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "Player":
                isPlayerInTrigger = false;
                interactMessage.enabled = false;
                break;
        }
    }
    
}
