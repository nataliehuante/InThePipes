/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the comic book story telling part of the game. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComicController : MonoBehaviour
{
    // comic squares left to load 
    private int squaresLeft;
    // list of comic square images
    public List<Image> sceneImages;
    // list of captions
    public List<TextMeshProUGUI> sceneCaptions;
    // menu game view controller
    private GameView gameView;
    // menu controller
    private MenuController menuController;
    // page we are on in the total comic sequence
    public string pageName;
    public bool isLobbyAnimDone = false;
    public Animator bookAnimator;

    void Awake() {
        // set all animation speeds for comic squares to 0
        for (int i = 1; i < sceneImages.Count; i++) {
            sceneImages[i].gameObject.GetComponent<Animator>().speed = 0;
        }
    }
    void Start() {

        
        // find scene object references
        gameView = FindObjectOfType<GameView>();
        menuController = FindObjectOfType<MenuController>();

        if (pageName == "lobby_anim") 
            return;
            

        // initally shows only the first comic square and the first caption
        showComicSquareNumber(1);
        showCaptionNumber(1);

        // calculate comic squares left to load 
        squaresLeft = sceneImages.Count - 1;

        
    } 

    void Update() {
        if ((pageName == "lobby_anim") && (isLobbyAnimDone)) {
            onNextClick();
        }
    }

    public void skipBookAnimation() {
        bookAnimator.speed = 0;
        isLobbyAnimDone = true;
    }

    // shows the caption given its number
    private void showCaptionNumber(int captionNum) {
        // enable correct caption (ex: caption 1 is element 0 in the list)
        sceneCaptions[captionNum - 1].enabled = true;

        // disable all other captions
        for (int i = 0; i < sceneCaptions.Count; i++) {
            if (i != captionNum - 1) {
                sceneCaptions[i].enabled = false;
            }
        }

    }

    // shows the comic square given its number 
    private void showComicSquareNumber(int comicSquareNumber) {
        // enable correct comic square (ex: comic 1 is element 0 in the list)
        sceneImages[comicSquareNumber - 1].enabled = true;

        // only if we are on the first image do we disable all other images
        if (comicSquareNumber == 1) {
            for (int i = 1; i < sceneImages.Count; i++) {
                sceneImages[comicSquareNumber].gameObject.GetComponent<Animator>().speed = 0;
                sceneImages[i].enabled = false;
            }
        }

        // find the animator component and then set the speed to 1 of the animator
        sceneImages[comicSquareNumber - 1].gameObject.GetComponent<Animator>().speed = 1;
    }
    
    // player clicked on next button
    public void onNextClick() {

        // if there are comic squares left to load, load next
        if (!(squaresLeft == 0)) {
            showCaptionNumber(sceneCaptions.Count - (squaresLeft - 1));
            showComicSquareNumber(sceneCaptions.Count - (squaresLeft - 1));
            squaresLeft -= 1;
        } 
        // if we have loaded the last comic square, load the next page or load level
        else { 
            switch(pageName) {
                // current (lobby_anim) --> next (introLobby_01)
                case "lobby_anim":
                    gameView.loadMenu("lobby_01");
                    break;
                // current (introLobby_01) --> next (introLobby_02)
                case "lobby_01": 
                    gameView.loadMenu("lobby_02");
                    break;
                // current (introLobby_02) --> next load lobby
                case "lobby_02":
                    menuController.StateUpdate(GameStates.GameStatesType.GamePlaying, "lobby");
                    break;
                // current (introLevel1_01) --> next (introLevel1_02)
                case "level1_01":
                    gameView.loadMenu("level1_02");
                    break;
                // current (introLevel1_02) --> next load level 1
                case "level1_02":
                    menuController.StateUpdate(GameStates.GameStatesType.GamePlaying, "levels");
                    break;
                // current (introLevel2_01) --> next (introLevel2_02)
                case "level2_01":
                    gameView.loadMenu("level2_02");
                    break;
                // current (introLevel2_02) --> next load level 2
                case "level2_02":
                    menuController.StateUpdate(GameStates.GameStatesType.GamePlaying, "levels");
                    break;
                // current (win_01) --> next (win_02)
                case "win_01":
                    gameView.loadMenu("win_02");
                    break;
                // current (win_02) --> next (finalWin)
                case "win_02":
                    gameView.loadMenu("winFinal");
                    break;
                case "":
                    Debug.Log("please enter the correct page name for the canvas");
                    break;
                default: 
                    Debug.Log("an error has occurred. please check that everything is hooked up properly and all variables have a value");
                    break;
            }
        }
    }

    public void SkipLobbyStory() {
        // skips all of the lobby scenes and goes straight to loading the lobby
        menuController.StateUpdate(GameStates.GameStatesType.GamePlaying, "lobby");
    }

    public void SkipWinStory() {
        // skips the win story scenes and goes straight to the ending win scene with the player's score
        gameView.loadMenu("winFinal");
    }

    public void SkipLevelStory() {
        // skips the intro level scenes and goes straight to loading the level 
        menuController.StateUpdate(GameStates.GameStatesType.GamePlaying, "levels");
    }
}
