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

public class Coin : MonoBehaviour
{
    // once the coin is picked up by the player, it will destroy itself
    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
