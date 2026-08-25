/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the player entering their username and storing that info throughout the system. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUsername : MonoBehaviour
{
    private string playerUsername;
    private LevelController levelController;
    private InPlayGameView inPlayGameView;

    void Start() {
        levelController = FindObjectOfType<LevelController>();
        inPlayGameView = FindObjectOfType<InPlayGameView>();
    }

    public void readInput(string playerInput) {
        // read in user input 
        playerUsername = playerInput;
        Debug.Log(playerUsername);

        // assign it to our global currentUsername
        GlobalVariables.currentName = playerUsername;
        inPlayGameView.setUsernameText(GlobalVariables.currentName);

        // resume the game
        levelController.OnResume();
    }
}
