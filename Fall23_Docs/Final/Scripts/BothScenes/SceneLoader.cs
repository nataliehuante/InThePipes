/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages scene loading. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // loads a new scene, given its name
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
