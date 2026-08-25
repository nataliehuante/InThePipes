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
    public AudioClip CoinMultiplierSound;
    public AudioClip ItemPurchasedSound;
    public AudioClip LevelPassedSound;
    public List<AudioClip> HurtSounds;
    public List<AudioClip> SwingingSounds;
    public List<AudioClip> ShootSounds;
    public AudioClip OpenShopSound;
    public AudioClip CoinPickupSound;
    public AudioClip SnakeAttackSound;
    public AudioClip RatBatAttackSound;
    public AudioClip CheckpointSound;
    public AudioClip SoftThudSound;

    public static LevelsSounds Instance;

    public AudioSource MusicSource;
    public AudioSource NearbyEnemySource;
    public AudioSource SFXSource;

    private int lastHurtClip = 0;
    private int lastSwingingClip = 0;
    private int lastShootClip = 0;

    private void Awake()
    {
        if (Instance != null & Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start() {
        adjustMusicVolume(GlobalVariables.masterVolume);
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
        SFXSource.PlayOneShot(ButtonClick);
    }
    public void PlayItemPurchased()
    {
        SFXSource.PlayOneShot(ItemPurchasedSound, 2f);
    }

    public void PlayCoinMultiplier()
    {
        SFXSource.PlayOneShot(CoinMultiplierSound);
    }

    public void PlayOpenShop() {
        SFXSource.PlayOneShot(OpenShopSound, 2f);
    }

    public void PlayJumpSound()
    {
        SFXSource.PlayOneShot(JumpSound);
    }

    public void PlaySnakeAttack() {
        SFXSource.PlayOneShot(SnakeAttackSound);
    }

    public void PlayRatBatAttack() {
        SFXSource.PlayOneShot(RatBatAttackSound);
    }

    public void PlayThudSound() {
        SFXSource.PlayOneShot(SoftThudSound, 3f);
    }



    public void PlayLevelPassedSound()
    {
        SFXSource.PlayOneShot(LevelPassedSound);
    }

    public void PlaySwingingSound() {
        SFXSource.PlayOneShot(SwingingSounds[lastSwingingClip], 2f);
        if (lastSwingingClip == 0)
            lastSwingingClip = 1;
        else if (lastSwingingClip == 1)
            lastSwingingClip = 0;
    }

    public void PlayHurtSound(float volume)
    {
        adjustVolume(volume);
        
        SFXSource.PlayOneShot(HurtSounds[lastHurtClip], 1f);
        lastHurtClip++;
        if (lastHurtClip == 5) {
            lastHurtClip = 0;
        }
    }

    public void PlayShootSound() {
        SFXSource.PlayOneShot(ShootSounds[lastShootClip], 1f);
        lastShootClip++;
        if (lastShootClip == 2)
            lastShootClip = 0;
    }

    public void PlayPickupCoinSound()
    {
        SFXSource.PlayOneShot(CoinPickupSound, 0.125f);
    }

    public void PlayCheckpointSound() {
        SFXSource.PlayOneShot(CheckpointSound);
    }

    private void adjustVolume(float newVolume) 
    {
        SFXSource.volume = newVolume;
    }

    public void adjustMusicVolume(float newVolume) {
        MusicSource.volume = newVolume;
        SFXSource.volume = newVolume;
    }

    public void updateMasterVolume(float newVolume) {
        GlobalVariables.masterVolume = newVolume;
    }
     
}
