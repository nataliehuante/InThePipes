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
        (0) Main Menu   (1) Lose By Rat     (2) Lose By Snake 
        (3) Lose By Croc    (4) Win Story   (5) Leaderboard
        (6) Lose By Fall    (7) Lose By Water (8) Lose By Bat
    */
    public static int currentMenuIndex = 0;
    public static int currentLevel = 0;
    public static int currentScore = 0;
    public static string currentName = "";

    // leaderboard
    public static List<int> leaderboard = new List<int>{0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public static List<string> leaderboardNames = new List<string>{"", "", "", "", "", "", "", "", "", ""};

    // initial game state
    public static GameStates.GameStatesType gameState = GameStates.GameStatesType.OnMainMenu; 
}
 