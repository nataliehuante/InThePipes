/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the sounds in the main menu scene. This includes background music and other sfx. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioClip ButtonClick;
    public AudioClip GameWonMusic;
    public AudioClip RatSound;
    public AudioClip CrocSound;
    public AudioClip SnakeSound;
    public AudioClip BatSound;

    public static MenuSounds Instance;

    public AudioSource MusicSource;
    public AudioSource GameWonMusicSource;
    public AudioSource LoseByEnemySource;
    public AudioSource SFXSource;

    private void Awake()
    {
        if (Instance != null & Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public void PlayMenuMusic()
    {
        MusicSource.Play();
    }

    public void PauseMenuMusic()
    {
        MusicSource.Pause();
    }

    public void PlayButtonClick()
    {
        SFXSource.PlayOneShot(ButtonClick, 0.5f);
    }

    public void PlayGameWonMusic() {
        GameWonMusicSource.Play();
    }

    public void PauseGameWonMusic() {
        GameWonMusicSource.Pause();
    }

    public void MuteMenuMusic() {
        MusicSource.volume = 0;
    }

    public void UnmuteMenuMusic() {
        MusicSource.volume = 1;
    }

    public bool IsMainMenuMusicPlaying() {
        return (MusicSource.isPlaying);
    }

    public void PlayLoseByEnemySounds(int methodOfLoss) {
        switch (methodOfLoss) {
            case 1: // rat
                PlayCorrectLoseSound("Rat");
                break;
            case 2: // snake
                PlayCorrectLoseSound("Snake");
                break;
            case 3: // croc
                PlayCorrectLoseSound("Croc");
                break;
            case 8: // bat
                PlayCorrectLoseSound("Bat");
                break; 
        }
    }

    private void PlayCorrectLoseSound(string enemyType)
    {
        switch (enemyType) {
            case "Rat": // rat
                LoseByEnemySource.clip = RatSound;
                break;
            case "Snake": // snake
                LoseByEnemySource.clip = SnakeSound;
                break;
            case "Croc": // croc
                LoseByEnemySource.clip = CrocSound;
                break;
            case "Bat": // bat
                LoseByEnemySource.clip = BatSound;
                break;
        }
        LoseByEnemySource.Play();
    }

    public void PauseLoseByEnemySounds() 
    {
        LoseByEnemySource.Pause();
    }
     
}
