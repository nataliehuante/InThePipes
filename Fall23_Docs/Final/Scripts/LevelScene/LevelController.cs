/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the game play during the levels. This includes functions such as level loading, pausing, 
resuming, etc. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    // public variables
    public List<GameObject> levels;
    public SceneLoader sceneLoader;
    public InPlayGameView inPlayGameView;
    public Leaderboard leaderboard;
    public bool GameOver = false;
    public bool isPaused = false;

    // private variables
    private PlayerController Player;
    private GameObject levelGameObject;
    private int currentLevel = 0;
    private int currentScore = 0;
    private LevelsSounds levelsSounds;

    void Start() {

        // print("showing in-game canvas");
        inPlayGameView.ShowInGameCanvas();
        // print("first goToNextLevel()");
        levelsSounds = FindObjectOfType<LevelsSounds>();
        GoToNextLevel();
    }

    // increment level, load it or win 
        // THIS IS WHAT IS CALLED TO START THE LOAD LEVEL CHAIN 
    public void GoToNextLevel()
    {
        currentLevel++;
        // print("current level loading: " + currentLevel);
        LoadNextLevel();
        inPlayGameView.UpdateReadouts(currentLevel, currentScore);
    }

    // returns the current level
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // sets the current level
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    // returns true if we are completed all levels
    private bool IsOutOfLevelRange()
    {
        if (currentLevel > levels.Count)
            return true;
        return false;
    }

    // destroys the current level instance
    private void DestroyCurrentLevel(){
       
        if ((levelGameObject != null))
        {
            Destroy(levelGameObject);
        }
    }

    // load the next level
    private void LoadNextLevel()
    {
        if (currentLevel <= levels.Count)
        {
            // award points for passing a level - excluding the first one
            if (currentLevel != 1) {
                levelsSounds.PlayLevelPassedSound();
                Player.playerScore += 150;
                currentScore = Player.playerScore;
            }
            DestroyCurrentLevel();
            levelGameObject = CreateLevel();
            Player = FindObjectOfType<PlayerController>();
            Player.playerScore = currentScore;

            if (currentLevel == 1) {
                OnWaitForInput();
            }
        }

        if (currentLevel > levels.Count)
        {
            // if we try to load past the last level, then we beat the game
            OnPlayerWinLastLevel();
        }

        
    }

    private GameObject CreateLevel()
    {
        // print("creating level " + (currentLevel));
        return Instantiate(levels[currentLevel-1], new Vector3(960f, 540f, 0.0f), Quaternion.identity);
    }


    public void OnResume() {
        // set pause screen
        inPlayGameView.ShowInGameCanvas();

        // set time scale to zero 
        Time.timeScale = 1;

        // reset pause variables
        isPaused = false;
    }

    // if player pauses and hits quit, returns to main menu
    public void OnQuit() {
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = 0;
        sceneLoader.LoadScene("MainMenu_NHuante");   
    }

    public void OnPause() {
        // set pause screen
        inPlayGameView.ShowPauseScreen();

        // set time scale to zero 
        Time.timeScale = 0;

        // set levelControler to isPaused
        isPaused = true;
    }

    private void OnWaitForInput() {
        // show the input screen
        inPlayGameView.ShowInputUsernameCanvas();

        // set time scale to zero 
        Time.timeScale = 0;

        // set levelControler to isPaused
        isPaused = true;
    }

    private void OnPlayerWinLastLevel() {
        // update leaderboard
        currentScore = Player.playerScore; 
        leaderboard.updateLeaderboard(currentScore);

        GlobalVariables.gameState = GameStates.GameStatesType.GameWon;
        GlobalVariables.currentMenuIndex = 4;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }


    public void OnPlayerLoseLevel(int methodOfLoss) {
        // called if the player loses the level & switches over to the appropriate menu scene

        // update leaderboard
        currentScore = Player.playerScore; 
        leaderboard.updateLeaderboard(currentScore);

        // load menu screen
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;
        GlobalVariables.currentMenuIndex = methodOfLoss;
        sceneLoader.LoadScene("MainMenu_NHuante");
    }
}
