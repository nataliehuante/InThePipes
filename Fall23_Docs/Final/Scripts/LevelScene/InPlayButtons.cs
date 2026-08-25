/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file contains functions called by buttons on the screen during gameplay. These
are used for development and testing so as to avoid having to play the entire level 
each time.  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayButtons : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private LevelController levelController;

    public void Start() {
        // set references
        sceneLoader = FindObjectOfType<SceneLoader>();
        levelController = FindObjectOfType<LevelController>();
    }

    // mimics player losing by snake attack 
    public void OnTestLoseBySnakeClick() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 2;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    // mimics player losing by rat attack 
    public void OnTestLoseByRatClick() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 1;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    // mimics player losing by bat attack 
    public void OnTestLoseByBatClick() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 8;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    // mimics player losing by croc attack 
    public void OnTestLoseByCrocClick() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 3;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    // mimics player passing the current level
    public void OnTestWinClick() {
        levelController.GoToNextLevel();
    }

    // mimics player losing by falling
    public void OnTestLoseByFall() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 6;
        sceneLoader.LoadScene("MainMenu_NHuante");   
    }
}
