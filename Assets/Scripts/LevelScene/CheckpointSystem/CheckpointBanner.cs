using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBanner : MonoBehaviour
{
    public int priority;
    public Sprite bannerInteractedSprite;
    public GameObject bannerEffect;
    private LevelsSounds sounds;
    private bool hasAnimated;

    void Start() {
        sounds = FindObjectOfType<LevelsSounds>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Player") && !hasAnimated)
        {
            // note we have already activated this checkpoint 
            hasAnimated = true;
            // update the player's latest checkpoint to be this one 
            if (priority > GlobalVariables.latestCheckpointPriority) {
                GlobalVariables.latestCheckpointPosition = gameObject.transform.position;
                GlobalVariables.latestCheckpointPriority = priority;
                Debug.Log(GlobalVariables.latestCheckpointPosition);
            }
            // change banner sprite 
            GetComponent<SpriteRenderer>().sprite = bannerInteractedSprite;
            // instantiate animation
            Instantiate(bannerEffect, gameObject.transform.position, Quaternion.identity);
            // play pickup sound 
            sounds.PlayCheckpointSound();
        }
    }
}
