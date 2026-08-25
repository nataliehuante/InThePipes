/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the tidal wave's movement through the level. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWave : MonoBehaviour
{
    // public variables
    public float speed = (float)2;

    // private variables
    private Player player;

    void Start() {
        player = FindObjectOfType<Player>();
    }

    void Update() {
        // don't start scrolling until the player has made a move, otherwise, move
        // if (player.hasMoved) {
        //     gameObject.transform.Translate(speed*Time.deltaTime,0,0);
        // }
        
    }
}
