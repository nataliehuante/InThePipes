/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the UI in the main menu scene. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameView : MonoBehaviour
{   
    #region Public Variables

    [Header("Main Menus")]
    public CanvasGroup MainMenu_Canvas;
    public CanvasGroup HowToPlay_Canvas;
    public CanvasGroup Leaderboard_Canvas;
    public CanvasGroup UsernameInput_Canvas;

    [Header("IntroToLobby")]
    public CanvasGroup IntroLobby_Anim;
    public CanvasGroup IntroLobby_01_Canvas;
    public CanvasGroup IntroLobby_02_Canvas;

    [Header("IntroToLevel1")]
    public CanvasGroup IntroLevel1_01_Canvas;
    public CanvasGroup IntroLevel1_02_Canvas;

    [Header("IntroToLevel2")]
    public CanvasGroup IntroLevel2_01_Canvas;
    public CanvasGroup IntroLevel2_02_Canvas;

    [Header("Win")]
    public CanvasGroup IntroWin_01_Canvas;
    public CanvasGroup IntroWin_02_Canvas;
    public CanvasGroup FinalWin_Canvas;
    public TextMeshProUGUI WinScoreText;

    [Header("Death By Rat")]
    public CanvasGroup LoseByRat_Canvas;
    public TextMeshProUGUI LoseByRatScoreText;
    
    [Header("Death By Snake")]
    public CanvasGroup LoseBySnake_Canvas;
    public TextMeshProUGUI LoseBySnakeScoreText;

    [Header("Death By Bat")]
    public CanvasGroup LoseByBat_Canvas; 
    public TextMeshProUGUI LoseByBatScoreText;
    
    [Header("Death By Water")]
    public CanvasGroup LoseByWater_Canvas; 
    public TextMeshProUGUI LoseByWaterScoreText;

    [Header("Toggle UI Text")]
    public TextMeshProUGUI toggleUIText;

    [Header("Menu Game Objects")]
    public GameObject MainMenuGameObject;
    public GameObject HowToPlayGameObject;
    public GameObject LeaderboardGameObject;
    public GameObject LeaderboardBackButton;

    [Header("Volume Slider")]
    public Slider volumeSlider;

    #endregion

    

    #region Private Variables
    private int menuToShowFromLoad;
    private float desiredAlpha;
    private float currentAlpha;
    public List<CanvasGroup> MasterListCanvases = new List<CanvasGroup>(18);
    #endregion

    public void Awake() {
        MasterListCanvases.Add(MainMenu_Canvas);
        MasterListCanvases.Add(HowToPlay_Canvas);
        MasterListCanvases.Add(Leaderboard_Canvas);

        MasterListCanvases.Add(IntroLobby_01_Canvas);
        MasterListCanvases.Add(IntroLobby_02_Canvas);

        MasterListCanvases.Add(IntroLevel1_01_Canvas);
        MasterListCanvases.Add(IntroLevel1_02_Canvas);

        MasterListCanvases.Add(IntroLevel2_01_Canvas);
        MasterListCanvases.Add(IntroLevel2_02_Canvas);

        MasterListCanvases.Add(IntroWin_01_Canvas);
        MasterListCanvases.Add(IntroWin_02_Canvas);
        MasterListCanvases.Add(FinalWin_Canvas);

        MasterListCanvases.Add(LoseByBat_Canvas);
        MasterListCanvases.Add(LoseByRat_Canvas);
        MasterListCanvases.Add(LoseBySnake_Canvas);
        MasterListCanvases.Add(LoseByWater_Canvas);

        MasterListCanvases.Add(IntroLobby_Anim);
        MasterListCanvases.Add(UsernameInput_Canvas);
    }

    public void Start() {
        volumeSlider.value = GlobalVariables.masterVolume;
    }

    // loads the appropriate menu given its numeric representation
    public void loadMenu(string menuToLoad) {
        // get the index of the canvas we want to load from the master list 
        int menuIndex = findMenuIndex(menuToLoad);

        // update any text fields if necessary 
        if (menuIndex == 11) // win final
            WinScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
        else if (menuIndex == 12) // bat 
            LoseByBatScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
        else if (menuIndex == 13) // rat 
            LoseByRatScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
        else if (menuIndex == 14) // snake 
            LoseBySnakeScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
        else if (menuIndex == 15) // water 
            LoseByWaterScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;

        // show menu 
        Show(MasterListCanvases[menuIndex]);

        // hide all other menus 
        for (int i = 0; i < MasterListCanvases.Count; i++) {
            if (i != menuIndex) {
                Hide(MasterListCanvases[i]);
            }
        } 
    }

    public void ToggleUIClick() {
        // toggle UI
        if (GlobalVariables.showUI) {
            GlobalVariables.showUI = false;
            toggleUIText.text = "[Off]";
        } else {
            GlobalVariables.showUI = true;
            toggleUIText.text = "[On]";
        }
        
    }

    private int findMenuIndex(string menuToFind) {
        int indexToReturn = 0; // by default, load main menu 

        switch(menuToFind) {
            case "mainMenu":
                indexToReturn = 0;
                break;
            case "howToPlay":
                indexToReturn = 1;
                break;
            case "leaderboard":
                indexToReturn = 2;
                break;
            case "lobby_01":
                indexToReturn = 3;
                break;
            case "lobby_02":
                indexToReturn = 4;
                break;
            case "level1_01":
                indexToReturn = 5;
                break;
            case "level1_02":
                indexToReturn = 6;
                break;
            case "level2_01":
                indexToReturn = 7;
                break;
            case "level2_02":
                indexToReturn = 8;
                break;
            case "win_01":
                indexToReturn = 9;
                break;
            case "win_02":
                indexToReturn = 10;
                break;
            case "winFinal":
                indexToReturn = 11;
                break;
            case "bat":
                indexToReturn = 12;
                break;
            case "rat":
                indexToReturn = 13;
                break;
            case "snake":
                indexToReturn = 14;
                break;
            case "water":
                indexToReturn = 15;
                break;
            case "lobby_anim":
                indexToReturn = 16;
                break;
            case "usernameInput":
                indexToReturn = 17;
                break;
        }


        // Debug.Log("returning index " + indexToReturn + " for menu " + menuToFind);
        return indexToReturn;
    }

    

    public void QuitApplication() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    private void Show(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.gameObject.SetActive(true);
    }

    private void Hide(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.gameObject.SetActive(false);
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float currentAlpha, float desiredAlpha) {
        bool cont = true;
        while (cont) {
            if (currentAlpha == desiredAlpha) {
                cont = false;
            } else {
                canvasGroup.alpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 1.0f * Time.deltaTime);
            }
        }
        yield return null;
    }

    private void HideAllGrids() {
        MainMenuGameObject.SetActive(false);
        HowToPlayGameObject.SetActive(false);
        LeaderboardGameObject.SetActive(false);
    }
}
