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
    private int scenesLeft = 2;
    public List<Image> sceneImages;
    public List<TextMeshProUGUI> sceneCaptions;
    private GameView gameView;
    private MenuController menuController;
    public int scenePageOrder = -1;

    void Start() {
        // find references
        gameView = FindObjectOfType<GameView>();
        menuController = FindObjectOfType<MenuController>();

        // initally shows only the first comic square and the first caption
        sceneImages[0].enabled = true;
        sceneImages[1].enabled = false;
        sceneImages[2].enabled = false;

        showCaptionNumber(1);
    } 

    // shows the caption given its number
    private void showCaptionNumber(int captionNum) {
        switch(captionNum) {
            case 1:
                sceneCaptions[0].enabled = true;
                sceneCaptions[1].enabled = false;
                sceneCaptions[2].enabled = false;
                break;
            case 2:
                sceneCaptions[0].enabled = false;
                sceneCaptions[1].enabled = true;
                sceneCaptions[2].enabled = false;
                break;
            case 3:
                sceneCaptions[0].enabled = false;
                sceneCaptions[1].enabled = false;
                sceneCaptions[2].enabled = true;
                break;
        }

    }

    // shows the next comic square
    public void ShowNextScene() {
        if (scenesLeft == 2) {
            sceneImages[1].enabled = true;
            showCaptionNumber(2);
            scenesLeft--;
        }
        else if (scenesLeft == 1) {
            sceneImages[2].enabled = true;
            showCaptionNumber(3);
            scenesLeft--;
        }
        else if (scenesLeft == 0) { // if all three comic square are shown, start the next scene
            if (scenePageOrder == 1) { // first page 
                gameView.ShowTwoStoryIntroCanvas();
            } else if (scenePageOrder == 2) { // second page 
                gameView.ShowThreeStoryIntroCanvas();
            } else if (scenePageOrder == 3) { // third page 
                menuController.OnPlayButtonClicked();
            } else if (scenePageOrder == 4) { // win story page
                gameView.ShowWinCanvas();
            } else if (scenePageOrder == -1) { // order not inputted
                print("An error occurred: Please check the order variable of the scene page");
            } 
        }
        else {
            print("An error occurred.");
        }
    }

    public void SkipStory() {
        // skips all of the story scenes and goes straight to gameplay
        menuController.OnPlayButtonClicked();
    }

    public void SkipWinStory() {
        // skipd the win story scenes and goes straight to the ending win scene with the player's score
        gameView.ShowWinCanvas();
    }
}
