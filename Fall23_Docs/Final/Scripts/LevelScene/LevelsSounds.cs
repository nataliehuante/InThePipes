/* 
Natalie Huante
2374481
huante@chapman.edu
CPSC 340 - Game Development

This file manages the game play sounds such as player jumping, player damage, 
background music, wave sfx, etc. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsSounds : MonoBehaviour
{
    public AudioClip ButtonClick;
    public AudioClip JumpSound;
    public AudioClip LevelPassedSound;
    public List<AudioClip> HurtSounds;
    public AudioClip SplashSound;
    public AudioClip PlayerFallSound;
    public AudioClip RatSound;
    public AudioClip CrocSound;
    public AudioClip SnakeSound;
    public AudioClip BatSound;
    public AudioClip CoinPickupSound;

    public static LevelsSounds Instance;

    public AudioSource MusicSource;
    public AudioSource NearbyEnemySource;
    public AudioSource SFXSource;

    private int lastHurtClip = 0;

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

    public void PlayJumpSound()
    {
        SFXSource.PlayOneShot(JumpSound);
    }

    public void PlayLevelPassedSound()
    {
        SFXSource.PlayOneShot(LevelPassedSound);
    }

    public void PlayHurtSound(float volume)
    {
        adjustVolume(volume);
        
        SFXSource.PlayOneShot(HurtSounds[lastHurtClip], 2f);
        lastHurtClip++;
        if (lastHurtClip == 5) {
            lastHurtClip = 0;
        }
    }

    public void PlaySplashSound()
    {
        SFXSource.PlayOneShot(SplashSound);
    }

    public void PlayFallSound() 
    {
        SFXSource.PlayOneShot(PlayerFallSound);
    }

    public void PlayNearbyEnemySound(string enemyType)
    {
        switch (enemyType) {
            case "Rat": // rat
                NearbyEnemySource.clip = RatSound;
                break;
            case "Snake": // snake
                NearbyEnemySource.clip = SnakeSound;
                break;
            case "Croc": // croc
                NearbyEnemySource.clip = CrocSound;
                break;
            case "Bat": // bat
                NearbyEnemySource.clip = BatSound;
                break;
        }
        NearbyEnemySource.Play();
    }

    public void PauseNearbyEnemySound() 
    {
        NearbyEnemySource.Pause();
    }

    public void PlayPickupCoinSound()
    {
        SFXSource.PlayOneShot(CoinPickupSound);
    }

    private void adjustVolume(float newVolume) 
    {
        SFXSource.volume = newVolume;
    }
     
}
