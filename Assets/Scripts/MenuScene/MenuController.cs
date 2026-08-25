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

        // as long as we are not in the win story sequence, play the menu music
        if (GlobalVariables.gameState == GameStates.GameStatesType.OnMainMenu) {
            menuSounds.PlayMenuMusic();
        }

        // the first menu to show will be the one stored in the global variables file (on start up it will be by default "mainMenu")
        StateUpdate(GlobalVariables.gameState, GlobalVariables.currentMenuIndex);

        Cursor.visible = true;
    }

    void Update() {
        // CheckHotKeys();
    }

    // hotkeys for IEEE demo - shifts to diffferent lose screens
    public void CheckHotKeys() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (GlobalVariables.gameState == GameStates.GameStatesType.GameLost)) { // 
            gameView.loadMenu("bat");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && (GlobalVariables.gameState == GameStates.GameStatesType.GameLost)) { // 
            gameView.loadMenu("rat");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && (GlobalVariables.gameState == GameStates.GameStatesType.GameLost)) { // 
            gameView.loadMenu("snake");
        }
    }
    
    // handles when the game is won
    private void OnGameWon() {
        // update menu state 
        GlobalVariables.gameState = GameStates.GameStatesType.GameWon;

        // start the win story sequence 
        gameView.loadMenu("win_01");

        // play the win music
        menuSounds.PlayGameWonMusic();
        menuSounds.PauseMenuMusic();
    }

    // handles when the game is lost
    private void OnGameLost(string newMenu) {
        // update menu state 
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;

        // load the correct menu 
        gameView.loadMenu(newMenu);

        // lose game sounds
        menuSounds.PlayLoseMusic();
    }

    // called when level story is clicked through or player skips through lobby story
    private void OnLoadLevel() {
        // update menu state to game playing
        GlobalVariables.gameState = GameStates.GameStatesType.GamePlaying;

        // level variables should already have been set by levelController or lobbySpecificInputs
        Debug.Log("should load level " + GlobalVariables.levelToStartAt);

        // load levels scene
        sceneLoader.LoadScene("Level1_NHuante");
    }

    // called when lobby story is clicked through or player skips through lobby story
    public void OnLoadLobby() {
        // update menu state to game playing
        GlobalVariables.gameState = GameStates.GameStatesType.GamePlaying;

        // reset level and score variables
        GlobalVariables.currentLevel = 0;
        GlobalVariables.currentScore = 0;

        // load the lobby
        sceneLoader.LoadScene("Lobby");
    }

    // handles when the user goes to the main menu
    private void OnMainMenu(string newMenu) {
        // update menu state
        GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;

        // load the correct canvas
        gameView.loadMenu(newMenu);

        // menuSounds.PlayNormalMusic();

        // pause any other music going on 
        menuSounds.PauseLoseMusic();
        menuSounds.PauseGameWonMusic();

        // if main menu music isn't already playing, start playing (prevents restarting the track every time)
        if (!(menuSounds.IsMainMenuMusicPlaying()))
            menuSounds.PlayMenuMusic();
        // menuSounds.UnmuteMenuMusic();
    }

    // updates the state and calls the appropriate function to handle the state update
    public void StateUpdate(GameStates.GameStatesType newState, string newMenu) {

        switch (newState) {
            case GameStates.GameStatesType.GamePlaying: 
                Debug.Log("game playing state");
                if (newMenu == "lobby") {
                    Debug.Log("loading lobby...");
                    OnLoadLobby();
                } else if (newMenu == "levels") {
                    Debug.Log("loading levels...");
                    OnLoadLevel();
                }
                break;
            case GameStates.GameStatesType.GameWon:
                Debug.Log("win state");
                OnGameWon();
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


    private string playerUsername;
    public void readInput(string playerInput) {
        
        // read in user input 
        playerUsername = playerInput;
        Debug.Log(playerUsername);
        Debug.Log(playerInput);

        // if no username entered, give a default name
        if (playerInput == "") {
            playerUsername = "player1";
        }

        // check for dexter mode
        if ((playerInput == "dexter") || (playerInput == "DEXTER")) {
            GlobalVariables.dexterMode = true;
        } else {
            GlobalVariables.dexterMode = false;
        }

        // assign it to our global currentUsername
        GlobalVariables.currentName = playerUsername;
        GlobalVariables.isLoggedIn = true;

        // start the intro to lobby story sequence 
        gameView.loadMenu("lobby_anim");
    }

    public void OnBackToLeaderboard_ButtonClick(string menuToGoBackTo) {
        // update the 'back' button to go back to to the appropriate page 
        GlobalVariables.menuToReturnToFromLeaderboard = menuToGoBackTo;

        // change the position of the button 
        // if (menuToGoBackTo == "mainMenu") {
        //     gameView.LeaderboardBackButton.GetComponent<RectTransform>().anchoredPosition = new Vector3 (-1230, 85, 0);
        // }
        // else {
        //     gameView.LeaderboardBackButton.GetComponent<RectTransform>().anchoredPosition = new Vector3 (-112, 85, 0);
        // }

        //TODO: if coming from not the main menu, the back button on the leaderboard canvas should lead back to that canvas
        // GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;
        gameView.loadMenu("leaderboard");
        // GlobalVariables.currentMenuIndex = "leaderboard";
        // StateUpdate(GlobalVariables.gameState, GlobalVariables.currentMenuIndex);
    }

    public void OnPlayAgain_ButtonClick() {
        if (GlobalVariables.gameState == GameStates.GameStatesType.GameLost) {
            // if lost and coming from level 1
            if (GlobalVariables.currentLevel == 1) {
                // update level variables for level 1
                GlobalVariables.startAtDifferentLevel = false;
                GlobalVariables.levelToStartAt = 1;

                // load level scene
                sceneLoader.LoadScene("Level1_NHuante");
            }
            // if lost and coming from level 2
            else if (GlobalVariables.currentLevel == 2) {
                // update level variables for level 1
                GlobalVariables.startAtDifferentLevel = true;
                GlobalVariables.levelToStartAt = 2;

                // load level scene
                sceneLoader.LoadScene("Level1_NHuante");
            }
        }
        // if won and wants to play again, restart from level 1
        else if (GlobalVariables.gameState == GameStates.GameStatesType.GameWon){
            // update level variables for level 1
            GlobalVariables.startAtDifferentLevel = false;
            GlobalVariables.levelToStartAt = 1;

            // load level scene
            sceneLoader.LoadScene("Level1_NHuante");
        }
    }

    public void OnBackToLobby_ButtonClick() {
        GlobalVariables.gameState = GameStates.GameStatesType.GamePlaying;
        GlobalVariables.currentMenuIndex = "lobby";
        StateUpdate(GlobalVariables.gameState, GlobalVariables.currentMenuIndex);
    }

    public void Leaderboard_BackButtonClick() {
        gameView.loadMenu(GlobalVariables.menuToReturnToFromLeaderboard);
    }
}
