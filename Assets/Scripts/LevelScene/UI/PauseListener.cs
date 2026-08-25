/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file listens for pausing the game. It will listen for the player to press 'P' and prompt the 
levelController for the actual functionality. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    private InPlayGameView inPlayGameView;
    private LevelController levelController;

    
    void Start()
    {
        inPlayGameView = FindObjectOfType<InPlayGameView>();
        levelController = FindObjectOfType<LevelController>();
    }

    // listen for 'P'
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Escape))) && !levelController.isPaused) {
            if (levelController.inLobby) {
                inPlayGameView.chooseRandomSpiderJoke();
            }
            levelController.OnPause();
        }
    }

    
}
