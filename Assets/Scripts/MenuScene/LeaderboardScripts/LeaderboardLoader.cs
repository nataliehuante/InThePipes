/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages loading the data kept in GlobalVariables to the UI elements in the leaderboard canvas. 
*/
using System.Collections;
using System.Collections.Generic;
// using System.Text.Json;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class LeaderboardLoader : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public Leaderboard leaderboard;

    void Awake() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        leaderboardText.text = leaderboard.ToString();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode lsm) {
        if (scene.name == "MainMenu_NHuante")
        {
            leaderboard.updateLeaderboard();
        }
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}

    