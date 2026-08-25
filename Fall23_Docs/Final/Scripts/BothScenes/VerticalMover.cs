/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file moves the object it is attached to vertically. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{ 
    // public variables
    [Header("Movement")]
    public float distanceToCover; // how much to move in either direction **the object should start in the center of its range**
    public float speed; // will always move at a constant speed
    public bool invertStartDirection = false;

    // private variables
    private Vector3 startingPosition;
    
    void Start()
    {
        // references assignments
        startingPosition = transform.position;
    }
    
    void Update()
    {
        Vector3 y = startingPosition;

        // if inverted, enemy will move down first
        if(invertStartDirection){
            y.y += distanceToCover * Mathf.Sin(Time.time * speed);
        }
        else { 
            y.y += distanceToCover * Mathf.Cos(Time.time * speed);
        }
        
        transform.position = y;

    }

    
}
