/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages actions a coin should take. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool shouldFlash = false;
    // public bool shouldCancelFlash = false;
    public bool isFlashing = false;
    // private bool hasCanceledFlash = false;
    public float multiplerCoinPickup_TimeToActivate = 5f;

    private SpriteRenderer spriteRenderer;

    // once the coin is picked up by the player, it will destroy itself
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // flash object
    private void Update() {
        if (shouldFlash && !isFlashing) 
            StartCoroutine(flashCollectible());
            
    }


    private IEnumerator flashCollectible() {
        isFlashing = true;
        
        if (spriteRenderer.enabled == true)
            spriteRenderer.enabled = false;
        else    
            spriteRenderer.enabled = true;

        yield return new WaitForSeconds(0.4f);

        isFlashing = false;

    }

}