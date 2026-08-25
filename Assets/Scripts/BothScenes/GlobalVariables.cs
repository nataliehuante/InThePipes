/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file contains any global variables. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{
    /* represents which menu should be shown by gameView 
        - used when loading the main menu scene from gameplay scene to track which menu should be shown
        and not defaulting to the main menu
        [0] mainMenu
        [1] howToPlay
        [2] leaderboard
        [3] lobby_01
        [4] lobby_02
        [5] level1_01
        [6] level1_02
        [7] level2_01
        [8] level2_02
        [9] win_01
        [10] win_02
        [11] winFinal
        [12] bat
        [13] rat
        [14] snake
        [15] water

    */
    // menu variables
    public static string currentMenuIndex = "mainMenu";
    public static string menuToReturnToFromLeaderboard = "mainMenu";
    // initial menu state
    public static GameStates.GameStatesType gameState = GameStates.GameStatesType.OnMainMenu; 

    // level variables
    public static int currentLevel = 1;

    // player stats
    public static int currentScore = 0; // within the level we are playing
    public static string currentName = "player1"; // username
    public static int totalCoins = 5500; // across all times we have played

    // level loading 
    public static int levelToStartAt = 1;
    public static bool startAtDifferentLevel = false;

    // toggle ui state
    public static bool showUI = true;

    
    // shop item statuses
    public static List<string> webShopStatus = new List<string>() {"equipped", "canBuy", "canBuy", "locked"};
    public static List<string> costumeShopStatus = new List<string>() {"canBuy", "canBuy", "canBuy", "locked"};

    // log in w/ username variables 
    public static bool isLoggedIn = false;

    // master volume variables 
    public static float masterVolume = 0.3f;

    // web customization variables 
    public static int currentWebColorIndex = 0;
    public static string currentWebColor = "white";
    
    // costumes variables 
    public static string equippedCostumeAnimName = "";
    
    // dexter mode variables 
    public static bool dexterMode = true;

    // spider jokes list 
    
    public static List<string> spiderJokes = new List<string>() { 
        "What do you get when you cross a spider with a snowman?\nFrostbite!", 
        "What did the spider do when he broke his computer?\nHe called the webmaster!", 
        "What do you get when you cross a spider with a computer?\nAn itchy byte!", 
        "What do spider order with their burgers?\nFrench flies!", 
        "Why did the spider go to his computer?\nTo check his webmail!", 
        "Why is the spider so clever?\nHe is always on the web!", 
        "What do you call a spider that can dance?\nA jitterbug!", 
        "Why did the spider get a job in tech support?\nBecause he was great at debugging!", 
        "What kind of music do spider like?\nCountry and Web-ern music!", 
        "What do you get when you cross a spider and a corn?\nCobwebs!", 
        "What do you call it when Spider-Man doesn't have his costume?\nPeter Parked!", 
        "What do you call spiders that just got married?\nNewly webs!", 
        "What's a spider's favorite hobby?\nFly fishing!", 
        "What did the spider say to her therapist?\nI feel like I'm spinning out of control!", 
        "Why do spiders make great tennis players?\nThey have an excellent top spin!", 
        "Why did Spider-Man fail his driving test?\nHe is a terrible parallel Parker!", 
        "Which app do spiders use to listen to music?\nSpot-a-fly!", 
        "What do you call an undercover bug?\nA Spy-der!", 
        "What spider comes out on a full moon?\nA wolf spider!", 
        "What do you call a spider with 10 eyes\nA spiiiiiiiiiider!", 
        "What is a spider's favorite day of the week?\nWebs-day", 
        "How do you know a spider is rich?\nThey have a huge net worth!", 
        "Does hairspray kill spiders?\nNo, but their hair looks stunning!", 
        "Why did the spider become a DJ?\nIt can sping amazing tunes!", 
        "Which type of spider enjoys Father's Day?\nDaddy long legs!"
    };

    // checkpoint variables 
    public static int latestCheckpointPriority = 0; // higher = more priority
    public static Vector3 latestCheckpointPosition; // starts as NULL


}


 