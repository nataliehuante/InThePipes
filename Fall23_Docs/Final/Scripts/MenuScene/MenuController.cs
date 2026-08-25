/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages all of the functionality of the main menu scene. It combines the functions of the gameView script, 
the scene loader, and the menu sounds. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private GameView gameView;
    private SceneLoader sceneLoader;
    private MenuSounds menuSounds;

    // called every time the MainMenu scene is loaded
    private void Start()
    {
        // finds and assigns objects needed
        gameView = GetComponentInChildren<GameView>();
        sceneLoader = GetComponent<SceneLoader>();
        menuSounds = FindObjectOfType<MenuSounds>();

        // updates to the current game state && shows the appropriate menu 
        StateUpdate(GlobalVariables.gameState, GlobalVariables.currentMenuIndex);

        if (!(GlobalVariables.gameState == GameStates.GameStatesType.GameWon)) {
            menuSounds.PlayMenuMusic();
        }
    }
    
    // handles when the game is won
    private void OnGameWon(int newMenu) {
        // Set the text value of our result text
        gameView.loadMenu(newMenu);

        // other win things
        menuSounds.PlayGameWonMusic();
        print("playing game won music");
        // menuSounds.MuteMenuMusic();
        menuSounds.PauseMenuMusic();
        print("pausing main menu music");
    }

    // handles when the game is lost
    private void OnGameLost(int newMenu) {
        gameView.loadMenu(newMenu);

        // other game lose things
        menuSounds.PlayLoseByEnemySounds(newMenu);
        menuSounds.PlayMenuMusic();
        // menuSounds.UnmuteMenuMusic();
        // menuSounds.PlayNormalMusic();
    }

    // updates state to GamePlaying , called when user clicks play/play again
    public void OnPlayButtonClicked() {
        StateUpdate(GameStates.GameStatesType.GamePlaying, 0);
    }

    // updates state to OnMainMenu, called when user clicks main menu button
    public void OnMainMenuClicked(int newMenu) {
        StateUpdate(GameStates.GameStatesType.OnMainMenu, newMenu);
    }

    // handles when the gameplay is started
    private void OnPlay() {
        GlobalVariables.gameState = GameStates.GameStatesType.GamePlaying;
        GlobalVariables.currentLevel = 0;
        GlobalVariables.currentScore = 0;
        sceneLoader.LoadScene("Level1_NHuante");
    }

    // handles when the user goes to the main menu
    private void OnMainMenu(int newMenu) {
        gameView.loadMenu(newMenu);

        // menuSounds.PlayNormalMusic();
        menuSounds.PauseLoseByEnemySounds();
        menuSounds.PauseGameWonMusic();
        if (!(menuSounds.IsMainMenuMusicPlaying()))
            menuSounds.PlayMenuMusic();
        // menuSounds.UnmuteMenuMusic();
    }

    // updates the state and calls the appropriate function to handle the state update
    public void StateUpdate(GameStates.GameStatesType newState, int newMenu) {

        switch (newState) {
            case GameStates.GameStatesType.GamePlaying: 
                Debug.Log("game playing state");
                OnPlay();
                break;
            case GameStates.GameStatesType.GameWon:
                Debug.Log("win state");
                OnGameWon(newMenu);
                break;
            case GameStates.GameStatesType.GameLost:
                Debug.Log("lose state");
                OnGameLost(newMenu);
                break;
            case GameStates.GameStatesType.OnMainMenu: 
                Debug.Log("main menu state");
                OnMainMenu(newMenu);
                break;
        }
    }


}
