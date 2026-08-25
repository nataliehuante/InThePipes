using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySpecificInputs : MonoBehaviour
{
    [Header("menu canvases - make sure to hook up")]
    public CanvasGroup webShopInteractability;
    public CanvasGroup costumeShopInteractability;
    public CanvasGroup levelSelectInteractibility;

    [Header("do not change these variables - public for debugging purposes")]
    public bool loadedOutOfLobby;
    public string currentTrigger;
    public WebShopInteractions webShopInteractions;
    public costumeShopInteraction CostumeShopInteractions;
    // private variables 
    private Player player;
    private SceneLoader sceneLoader;
    public LevelsSounds sounds;
    public LevelController levelController;


    
    // Start is called before the first frame update
    void Start()
    {
        // initialize variables
        currentTrigger = "";
        loadedOutOfLobby = false;
        sceneLoader = FindObjectOfType<SceneLoader>();
        // find the player 
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Cursor.visible = false;
    }

    // Update
    void Update()
    {
        // if we are outside all triggers, we don't care about user input
        if (currentTrigger != "")
        {
            CheckInput();
        }
    }

    // check for user input for menu interaction 
    private void CheckInput()
    {
        // if the user presses 'X'
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (currentTrigger)
            {
                case "LevelSelect":
                    levelSelectInteractibility.alpha = 1; // set visible
                    levelSelectInteractibility.blocksRaycasts = true; // block raycasts
                    levelSelectInteractibility.interactable = true; // enable UI interaction
                    // note that we are now interacting with the menu
                    currentTrigger = "inLevelSelect";
                    // enable cursor
                    Cursor.visible = true;
                    // disable player movement
                    player.disableAllMovement = true;
                    player.SetVelocityX(0f);
                    sounds.PlayOpenShop();
                    levelController.lookingAtMenu = true;
                    break;
                case "CostumeShop":
                    costumeShopInteractability.alpha = 1; // set visible
                    costumeShopInteractability.blocksRaycasts = true; // block raycasts
                    costumeShopInteractability.interactable = true; // enable UI interaction
                    // note that we are now interacting with the menu
                    currentTrigger = "inCostumeShop";
                    // enable cursor
                    Cursor.visible = true;
                    // disable player movement
                    player.disableAllMovement = true;
                    player.SetVelocityX(0f);
                    CostumeShopInteractions.onLoadMenu();
                    sounds.PlayOpenShop();
                    levelController.lookingAtMenu = true;
                    break;
                case "WebShop":
                    webShopInteractability.alpha = 1; // set visible
                    webShopInteractability.blocksRaycasts = true; // block raycasts
                    webShopInteractability.interactable = true; // enable UI interaction

                    // note that we are now interacting with the menu
                    currentTrigger = "inWebShop";
                    // enable cursor
                    Cursor.visible = true;

                    // disable player movement
                    player.disableAllMovement = true;
                    player.SetVelocityX(0f);

                    webShopInteractions.onLoadMenu();
                    sounds.PlayOpenShop();
                    levelController.lookingAtMenu = true;
                    // webShopInteractions.onLoadMenu();
                    break;
                case "Bed":
                    loadedOutOfLobby = true; // will remind us to stop trying to interact with menus, we don't care anymore
                    // update menu variables
                    GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;
                    GlobalVariables.currentMenuIndex = "mainMenu";
                    // load menu scene
                    sceneLoader.LoadScene("MainMenu_NHuante"); 
                    break;
                default:
                    Debug.Log("Impossible state reached, check logic");
                    break;
            }
        }
    }

    public void LevelSelect_LevelOneCLick() {
        enableLoadedOutOfLobby();

        // update level variables for level 1
        GlobalVariables.startAtDifferentLevel = false;
        GlobalVariables.levelToStartAt = 1;

        // update menu variables for level 1 story
        GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;
        GlobalVariables.currentMenuIndex = "level1_01";

        // load menu scene
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    public void LevelSelect_LevelTwoCLick() {
        enableLoadedOutOfLobby();
        
        // update level variables for level 2
        GlobalVariables.startAtDifferentLevel = true;
        GlobalVariables.levelToStartAt = 2;

        // update menu variabls for level 2 story
        GlobalVariables.gameState = GameStates.GameStatesType.OnMainMenu;
        GlobalVariables.currentMenuIndex = "level2_01";

        // load menu scene
        sceneLoader.LoadScene("MainMenu_NHuante");
    }

    public void disableLevelSelectMenu() {
        levelSelectInteractibility.alpha = 0; // Make invisible
        levelSelectInteractibility.blocksRaycasts = false; // Disable interaction
        levelSelectInteractibility.interactable = false; // Disable UI interaction
        // update to we are in trigger but not interacting with menu
        currentTrigger = "LevelSelect";
        // enable player movement
        player.disableAllMovement = false;
        Cursor.visible = false;
        sounds.PlayOpenShop();
        levelController.lookingAtMenu = false;
    }

    public void disableWebShop() {
        webShopInteractability.alpha = 0; // Make invisible
        webShopInteractability.blocksRaycasts = false; // Disable interaction
        webShopInteractability.interactable = false; // Disable UI interaction
        // update to we are in trigger but not interacting with menu
        currentTrigger = "WebShop";
        // enable player movement
        player.disableAllMovement = false;
        Cursor.visible = false;
        sounds.PlayOpenShop();
        levelController.lookingAtMenu = false;
    }

    public void disableCostumeShop() {
        costumeShopInteractability.alpha = 0; // Make invisible
        costumeShopInteractability.blocksRaycasts = false; // Disable interaction
        costumeShopInteractability.interactable = false; // Disable UI interaction
        currentTrigger = "CostumeShop";
        player.disableAllMovement = false;
        Cursor.visible = false;
        sounds.PlayOpenShop();
        levelController.lookingAtMenu = false;
    }

    public void enableLoadedOutOfLobby() {
        loadedOutOfLobby = true;
    }
}
