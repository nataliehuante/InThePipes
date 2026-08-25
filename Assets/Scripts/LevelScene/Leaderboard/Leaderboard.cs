/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This manages the leaderboard. The data is kept in the GlobalVariables script and managed here.   
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;


[System.Serializable]
public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<string> leaderboardNames;
    [SerializeField]
    private List<int> leaderboardScores;
    
    private string filepath;


    void Awake()
    {
        this.filepath = Application.persistentDataPath + "/LeaderboardData.json";
        // Debug.Log(filepath);
        leaderboardNames = new List<string>();
        leaderboardScores = new List<int>();
        LoadFromJSON(filepath);
    }
    
    void OnApplicationQuit()
    {
        this.SaveToJSON(filepath);
    }

    // adds the newScore to the leaderboard, if applicable
    public void updateLeaderboard() {
        // check if the newScore is good enough to go on the leaderboard
        int eligibility = checkForEligibility(GlobalVariables.currentScore);

        if (eligibility == -1) { // not eligible
            // print("score not eligible");
        }
        else { // eligible, int returned is the index it belongs in
            // print("score eligible, at index " + eligibility);

            // insert newScore into the leaderboard 
            leaderboardNames.Insert(eligibility, GlobalVariables.currentName);
            leaderboardScores.Insert(eligibility, GlobalVariables.currentScore);

            // FOR TESTING: PRINT LEADERBOARD
            // print(this.ToString());
        }
    }

    // checks whether the player's score is in the top 10
    private int checkForEligibility(int newScore) {
        int eligibleIndex = -1;
        int ctr = 0;

        // Debug.Log(this.ToString());
        for (int i = 0; i < 10; ++i)
        {
            if (newScore >= leaderboardScores[i])
            {
                eligibleIndex = ctr;
                break;
            }
            ++ctr;
        }

        if (eligibleIndex > 9) {
            eligibleIndex = -1;
        }
        return eligibleIndex;
    }

    private void SaveToJSON(string filepath)
    {
        string listData = JsonUtility.ToJson(this);
        // Debug.Log(listData);
        File.WriteAllText(filepath, listData);
    }

    private void LoadFromJSON(string filepath)
    {
        // Read from save file, if existent.
        if (File.Exists(filepath))
        {
            string jsonString = File.ReadAllText(filepath);
            // Debug.Log(jsonString);
            JsonUtility.FromJsonOverwrite(jsonString, this);
        }
        // Initialize empty list if no save file found.
        else
        {
            for (int i = 0; i < 10; i++)
            {
                leaderboardNames.Add("");
                leaderboardScores.Add(0);
            }
        }
    }
    
    public override string ToString()
    {
        string leaderboardText = "";
        
        for (int i = 0; i < 9; ++i)
        {
            leaderboardText += $"{i + 1}.    {leaderboardNames[i]}  ...........  {leaderboardScores[i]} points\n";
        }
        leaderboardText += $"{10}.   {leaderboardNames[9]}  ...........  {leaderboardScores[9]} points\n";

        return leaderboardText;
    }
}
