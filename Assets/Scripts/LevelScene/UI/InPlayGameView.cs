/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the UI elements during gameplay. These include the pause screen, 
the in-play screen (containing stats such as player score, lives, etc.), the username-input 
screen, and the damageTaken screen (aka Hurt Screen)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InPlayGameView : MonoBehaviour
{
    public CanvasGroup InGameCanvas;
    public CanvasGroup PauseCanvas;
    // public CanvasGroup TestButtonsCanvas;
    // public CanvasGroup InputUsernameCanvas;
    public CanvasGroup HurtCanvas;
    public CanvasGroup HowToPlayCanvas;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI usernameText;

    public Image heartOne;
    public Image heartTwo;
    public Image heartThree;
    public Image heartFour;
    public Slider volumeSlider;
    // variables for lobby pause text
    public TextMeshProUGUI SpiderJokeText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "0000";
        updateLives(3);
        volumeSlider.value = GlobalVariables.masterVolume;
    }

    public void chooseRandomSpiderJoke() {
        int randomIndex = Random.Range(0, GlobalVariables.spiderJokes.Count);
        SpiderJokeText.text = GlobalVariables.spiderJokes[randomIndex];
    }

    // Set the score 
    public void setScoreText(int newScore) {
        ScoreText.text = "" + newScore;
        // print("Score: " + newScore);
    }

    // Set the level
    public void setLevelText(int newLevel) {
        LevelText.text = "Level: " + newLevel;
    }

    // Set the username
    public void setUsernameText(string playerName) {
        usernameText.text = playerName;
    }

    // update the readouts
    public void UpdateReadouts(int currentLevel, int currentScore) {
        setLevelText(currentLevel);
        setScoreText(currentScore);
    }   

    // update lives
    public void updateLives(int numOfLives) {
        switch(numOfLives) {
            case 1:
                heartOne.enabled = true;
                heartTwo.enabled = false;
                heartThree.enabled = false;
                heartFour.enabled = false;
                break;
            case 2: 
                heartOne.enabled = true;
                heartTwo.enabled = true;
                heartThree.enabled = false;
                heartFour.enabled = false;
                break;
            case 3:
                heartOne.enabled = true;
                heartTwo.enabled = true;
                heartThree.enabled = true;
                heartFour.enabled = false;
                break;
            case 4:
                heartOne.enabled = true;
                heartTwo.enabled = true;
                heartThree.enabled = true;
                heartFour.enabled = true;
                break;
        }
    }

    public void ShowInGameCanvas() {
        
        Hide(PauseCanvas);
        Hide(HurtCanvas);
        Hide(HowToPlayCanvas);

        if (!GlobalVariables.showUI)
            return;
        Show(InGameCanvas);
    }

    public void ShowPauseScreen() {
        Show(PauseCanvas);
        Hide(InGameCanvas);
        Hide(HurtCanvas);
        Hide(HowToPlayCanvas);
    }

    public void ShowHowToPlayCanvas() {
        Show(HowToPlayCanvas);
        Hide(PauseCanvas);
        Hide(InGameCanvas);
        Hide(HurtCanvas);
    }

    public void HideHowToPlayCanvas() {
        Hide(HowToPlayCanvas);
    }

    public void ShowPlayerHurtCanvas() {
        StartCoroutine(ShowHurtCanvas());
    }


    private void Show(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void Hide(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator ShowHurtCanvas()
    {
        Show(HurtCanvas);
        yield return new WaitForSeconds((float)0.15);
        Hide(HurtCanvas);
    }


    
}
