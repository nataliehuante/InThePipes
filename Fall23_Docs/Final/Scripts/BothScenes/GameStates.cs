/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file contains the states used by the state machine. 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public enum GameStatesType
    {
        GamePlaying,
        GameWon,
        GameLost, 
        OnMainMenu,
    };
}
