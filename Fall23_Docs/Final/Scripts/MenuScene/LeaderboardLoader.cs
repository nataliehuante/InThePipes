/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages loading the data kept in GlobalVariables to the UI elements in the leaderboard canvas. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardLoader : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    void Start()
    {
        string newLeaderboard = "";

        for (int i = 0; i < 10; i++) {
            newLeaderboard += (i+1) + ".    " + GlobalVariables.leaderboardNames[i] + " ...........   " + GlobalVariables.leaderboard[i] + " points\n";
        }

        leaderboardText.text = newLeaderboard;
    }

}
