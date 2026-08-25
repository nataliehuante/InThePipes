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
    [Header("Variables To Set")]
    public bool inLobby = false;
    public List<GameObject> levels;
    public Timer timer;

    [Header("Don't Touch - Variables Accessed From Other Scripts")]
    public bool GameOver = false;
    public bool isPaused = false;
    public bool lookingAtMenu = false;
    
    // private variables 

    // scene object references
    private Player player;
    private GameObject camera;
    private SceneLoader sceneLoader;
    private InPlayGameView inPlayGameView;
    private LevelsSounds levelsSounds;

    // variables used to track level loading & player stats
    private GameObject levelGameObject;
    public int currentLevel = 0;
    private int currentScore = 0;
    public static List<Material> webColors = new List<Material>();
    public static List<Sprite> webAnchors = new List<Sprite>();


    [SerializeField]
    private PlayerData playerData;

    void Start() {
        Cursor.visible = false;
        inPlayGameView = FindObjectOfType<InPlayGameView>();
        // find our levelsSounds object
        levelsSounds = FindObjectOfType<LevelsSounds>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        // if this level controller is meant to control the lobby, just load the lobby and do nothing else
        if (inLobby) {
            LoadLobby();
            inPlayGameView.HideHowToPlayCanvas();
            return;
        }

        // if this level controller is meant to control levels
        else {
            // enable the in game canvas
            inPlayGameView.ShowInGameCanvas();

            // if we should load a different level, update currentLevel to reflect this
            if (GlobalVariables.startAtDifferentLevel) {
                currentLevel = GlobalVariables.levelToStartAt - 1;
            }
            // development log 
            // Debug.Log("current level: " + currentLevel + " should load at index: " + (currentLevel - 1));
            // load the next level (if currentLevel above was not edited, we should start at level 1 by default)
            GoToNextLevel();
        }

        
    }

    void Update() {
        // only enable hotkeys if dexter mode is enabled
        if (GlobalVariables.dexterMode)
            CheckHotKeys();
    }

    public void CompleteLevelTrigger() {
        if (currentLevel == 1) 
            CompleteLevelOneTrigger();
        else if (currentLevel == 2)
            CompleteLevelTwoTrigger();
        // else 
            // Debug.Log("supposedly completed unknown level: " + currentLevel);
    }

    // called when the player completed level 1
    public void CompleteLevelOneTrigger() {
        // update level variables for when we load level 2
        GlobalVariables.startAtDifferentLevel = true;
        GlobalVariables.levelToStartAt = 2;
        GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;
        GlobalVariables.currentMenuIndex = "level2_01";

        // load menu scene
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    // called when the player completed level 2
    public void CompleteLevelTwoTrigger() {
        OnPlayerWinLastLevel();
    }
    

    // increment level, load it or win 
        // THIS IS WHAT IS CALLED TO START THE LOAD LEVEL CHAIN 
    public void GoToNextLevel()
    {
        // increment our current level count
        currentLevel++;
        GlobalVariables.currentLevel = currentLevel;
        // reset timer 
        timer.OnResetLevel_ButtonClick();

        // load the next level based on our incremented count
        LoadNextLevel();

        // update our in game readouts
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
        // if our current level is out of range of the indexes for our level list, return true
        // keep in mind: when we load a level we index as [currentLevel - 1], so we check for > here, not >=
        if (currentLevel > levels.Count)
            return true;
        return false;
    }

    // destroys the current level instance
    private void DestroyCurrentLevel(){
       // as long as there is a level to destroy, destroy it 
        if ((levelGameObject != null))
        {
            Destroy(levelGameObject);
        }
    }

    // load the next level
    private void LoadNextLevel()
    {
        // if there is no next level to load, we must have won, load win story
        if (IsOutOfLevelRange())
        {
            OnPlayerWinLastLevel();
        }

        // otherwise, load the next level 
        
        // play level passed sound for any level loading past level 1 (that is just a given)
        // also carry over the score from previous level passed (score must be 0 at start of level 1 always)
         if (currentLevel != 1) {
            // levelsSounds.PlayLevelPassedSound();
            currentScore = playerData.score;
        }

        // let's destory out current level, if there is one, before we load the next one           
        DestroyCurrentLevel();

        // load the next level and update our reference to it
        levelGameObject = CreateLevel();
        // find our new player instance & camera instance
        player = FindObjectOfType<Player>();

        // update web color 
        // player.updateWebColor();

        camera = GameObject.FindWithTag("MainCamera");
        // pass through our carried over score into the new player instance
        playerData.score = currentScore;

        // update our game hud to include username
        inPlayGameView.setUsernameText(GlobalVariables.currentName);

        // make our cursor invisible for game play
        Cursor.visible = false;

        // place the player and camera in the correct positions for the current level
        placePlayerTransform();
        placeCameraTransform();
        updatePlayerCostume();
    }        

    private void updatePlayerCostume() {
        player.GetComponent<Animator>().SetBool(GlobalVariables.equippedCostumeAnimName, true);
    }

    private void placePlayerTransform() {
        if (currentLevel == 1) { // level 1 part 1 coordinates
            player.transform.position = new Vector3(1798f, -102.6f, -10f); 
            
        } else if (currentLevel == 2) { // level 1 part 2 coordinates
            player.transform.position = new Vector3(2281f, -109.8f, -10f);
        }
        GlobalVariables.latestCheckpointPriority = 0;
        GlobalVariables.latestCheckpointPosition = player.transform.position;
    }

    private void placeCameraTransform() {
        if (currentLevel == 1) { // level 1 part 1 coordinates
            camera.transform.position = new Vector3(1798f, -97.5f, -20f);
        } else if (currentLevel == 2) { // level 1 part 2 coordinates
            camera.transform.position = new Vector3(2293.4f, -104.2f, -20f);
        }
    }

    private GameObject CreateLevel()
    {
        // instantiate the next level prefab at the position everything is centered at (960, 540, 0)
        // ex: currentLevel = 1 will load level at index levels[1-0] = levels[0]
        return Instantiate(levels[currentLevel-1], new Vector3(960f, 540f, 0.0f), Quaternion.identity);
    }


    public void OnResume() {
        if (inLobby) {
            inPlayGameView.HideHowToPlayCanvas();
        } else {
            // set pause screen
            inPlayGameView.ShowInGameCanvas();
        }

        // set time scale to zero 
        Time.timeScale = 1;

        // reset pause variables
        isPaused = false;

        // disable cursor
        Cursor.visible = false;
    }

    // if we are loading the lobby from the start
    public void LoadLobby() {
        // yes i know this is repetitive, but this helps for now in terms of function names and readability
        // sceneLoader.LoadScene("Lobby");  
        // player = FindObjectOfType<Player>();
        // player.updateWebColor();
    }

    // if player pauses and hits quit, returns to main menu
    public void OnQuit() {
        // load the lobby scene (we don't want to save any player stats bc they quit)
        sceneLoader.LoadScene("Lobby");   
    }

    public void OnPause() {
        if (inLobby && lookingAtMenu) {
            return;
        }

        if (inLobby) {
            inPlayGameView.ShowHowToPlayCanvas();
        } else {
            // set pause screen
            inPlayGameView.ShowPauseScreen();
        }

        // set time scale to zero 
        Time.timeScale = 0;

        // set levelControler to isPaused
        isPaused = true;

        // enable cursor
        Cursor.visible = true;
    }

    // called from the restart button in the level pause menu 
    public void OnRestartLevel() {
        // reset timer 
        // timer.OnResetLevel_ButtonClick();

        // reset player stats
        currentLevel--;
        currentScore = 0;

        // reload level 
        GoToNextLevel();

        // resume 
        OnResume();
    }


    private void OnPlayerWinLastLevel() {
        // update leaderboard with player stats
        GlobalVariables.currentScore = playerData.score; 
        GlobalVariables.totalCoins += playerData.score;

        // set the menu state to game won 
        GlobalVariables.gameState = GameStates.GameStatesType.GameWon;

        // load the menu scene
        sceneLoader.LoadScene("MainMenu_NHuante");
    }


    public void OnPlayerLoseLevel(string methodOfLoss) {
        // update leaderboard with player stats
        GlobalVariables.currentScore = playerData.score; 
        GlobalVariables.totalCoins = playerData.score;

        // set the menu state to game lost
        GlobalVariables.gameState = GameStates.GameStatesType.GameLost;

        // store which canvas we should load upon menu scene load
        GlobalVariables.currentMenuIndex = methodOfLoss;

        // load the menu scene
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    public void CheckHotKeys() {
        if (Input.GetKeyDown(KeyCode.Alpha4) && (currentLevel == 1)) { // 
            player.transform.position = new Vector3(2030f, -64f, -10f); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) && (currentLevel == 2)) {
            player.transform.position = new Vector3(2357f, -106.615f, -10f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && (currentLevel == 2)) {
            player.transform.position = new Vector3(2344f, -61f, -10f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && (currentLevel == 2)) {
            player.transform.position = new Vector3(2452f, -92.7f, -10f);
        }
    }


}
