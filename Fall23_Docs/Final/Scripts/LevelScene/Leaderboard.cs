/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This manages the leaderboard. The data is kept in the GlobalVariables script and managed here.   
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    
    // adds the newScore to the leaderboard, if applicable
    public void updateLeaderboard(int newScore) {
        // check if the newScore is good enough to go on the leaderboard
        int eligibility = checkForEligibility(newScore);

        if (eligibility == -1) { // not eligible
            print("score not eligible");
        }
        else { // eligible, int returned is the index it belongs in
            print("score eligible, at index " + eligibility);

            // insert newScore into the leaderboard 
            GlobalVariables.currentScore = newScore;
            GlobalVariables.leaderboard.Insert(eligibility, newScore);
            GlobalVariables.leaderboardNames.Insert(eligibility, GlobalVariables.currentName);

            // FOR TESTING: PRINT LEADERBOARD
            string leaderboardPrint = "";
            string leaderboardNames = "";
            for (int i = 0; i < GlobalVariables.leaderboard.Count; i++) {
                leaderboardPrint += ", " + GlobalVariables.leaderboard[i];
                leaderboardNames += ", " + GlobalVariables.leaderboardNames[i]; 
            }
            print(leaderboardPrint);
        }
    }

    // checks whether the player's score is in the top 10
    private int checkForEligibility(int newScore) {
        int eligibleIndex = -1;
        for (int i = 0; i < GlobalVariables.leaderboard.Count; i++) {
            if (newScore > GlobalVariables.leaderboard[i]) {
                eligibleIndex = i;
                break;
            }
        }

        if (eligibleIndex > 9) {
            eligibleIndex = -1;
        }
        return eligibleIndex;
    }
}
