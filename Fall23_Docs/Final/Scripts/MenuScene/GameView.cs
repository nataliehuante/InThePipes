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

public class GameView : MonoBehaviour
{
    public CanvasGroup MainMenuCanvas;
    public CanvasGroup HowToPlayCanvas;
    public CanvasGroup LeaderboardCanvas;
    public CanvasGroup OneStoryIntroCanvas;
    public CanvasGroup TwoStoryIntroCanvas;
    public CanvasGroup ThreeStoryIntroCanvas;
    public CanvasGroup WinStoryCanvas;

    public CanvasGroup WinCanvas;
    public TextMeshProUGUI WinScoreText;

    public CanvasGroup LoseByRatCanvas;
    public TextMeshProUGUI LoseByRatScoreText;

    public CanvasGroup LoseBySnakeCanvas;
    public TextMeshProUGUI LoseBySnakeScoreText;

    public CanvasGroup LoseByCrocCanvas;
    public TextMeshProUGUI LoseByCrocScoreText;

    public CanvasGroup LoseByFallCanvas; 
    public TextMeshProUGUI LoseByFallScoreText;

    public CanvasGroup LoseByWaterCanvas; 
    public TextMeshProUGUI LoseByWaterScoreText;

    public CanvasGroup LoseByBatCanvas; 
    public TextMeshProUGUI LoseByBatScoreText;

    private int menuToShowFromLoad;

    // loads the appropriate menu given its numeric representation
    public void loadMenu(int menuToLoad) {
        switch(menuToLoad) {
            case 0:
                ShowMainMenuScreen();
                break;
            case 1: 
                LoseByRatScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseByRatCanvas();
                break;
            case 2:
                LoseBySnakeScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseBySnakeCanvas();
                break;
            case 3:
                LoseByCrocScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseByCrocCanvas();
                break;
            case 4:
                WinScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowWinStoryCanvas();
                break;
            case 5:
                ShowLeaderboardCanvas();
                break;
            case 6:
                LoseByFallScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseByFallCanvas();
                break;
            case 7: 
                LoseByWaterScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseByWaterCanvas();
                break;
            case 8:
                LoseByBatScoreText.text = GlobalVariables.currentName + "   " + GlobalVariables.currentScore;
                ShowLoseByBatCanvas();
                break;
        }
    }

    public void ShowMainMenuScreen() {
        Show(MainMenuCanvas);

        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowHowToPlayCanvas() {
        Show(HowToPlayCanvas);

        Hide(MainMenuCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLeaderboardCanvas() {
        Show(LeaderboardCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowOneStoryIntroCanvas() {
        Show(OneStoryIntroCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowTwoStoryIntroCanvas() {
        Show(TwoStoryIntroCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowThreeStoryIntroCanvas() {
        Show(ThreeStoryIntroCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowWinStoryCanvas() {
        Show(WinStoryCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowWinCanvas() {
        Show(WinCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseByRatCanvas() {
        Show(LoseByRatCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseBySnakeCanvas() {
        Show(LoseBySnakeCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LoseByFallCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseByCrocCanvas() {
        Show(LoseByCrocCanvas);

        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByFallCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseByFallCanvas() {
        Show(LoseByFallCanvas);


        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByWaterCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseByWaterCanvas() {
        Show(LoseByWaterCanvas);


        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByFallCanvas);
        Hide(LoseByBatCanvas);
    }

    public void ShowLoseByBatCanvas() {
        Show(LoseByBatCanvas);


        Hide(MainMenuCanvas);
        Hide(HowToPlayCanvas);
        Hide(LeaderboardCanvas);
        Hide(OneStoryIntroCanvas);
        Hide(TwoStoryIntroCanvas);
        Hide(ThreeStoryIntroCanvas);
        Hide(WinStoryCanvas);
        Hide(WinCanvas);
        Hide(LoseByRatCanvas);
        Hide(LoseBySnakeCanvas);
        Hide(LoseByCrocCanvas);
        Hide(LoseByFallCanvas);
        Hide(LoseByWaterCanvas);
    }

    public void QuitApplication() {
        Application.Quit();
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
}
