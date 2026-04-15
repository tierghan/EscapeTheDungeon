using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField]
    AudioSource backgroundMusicSource, clickSFXSource, healSFXSource, statUpSFXSource, playerSFXSource, buySFXSource, deadWaves;

    [SerializeField]
    AudioClip clickSFX, healSFX, statUpSFX, playerMeleeSFX, playerMagicSFX, playerDodgeSFX, playerBlockSFX, buySFX, mainMenuBackground, mainGameBackground, gameOverClip, enemyDeathClip;

    float timer = 0f;
    bool isDead = false;
    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Play Main Menu Music
            backgroundMusicSource.clip = mainMenuBackground;
        }
        else if (SceneManager.GetActiveScene().name == "MainGameScene")
        {
            // Play Main Game Music
            backgroundMusicSource.clip = mainGameBackground;
        }
        backgroundMusicSource.Play();
    }

    public void PlaySFXClick()
    {
        clickSFXSource.Play();
    }

    public void PlaySFXHeal()
    {
        healSFXSource.Play();
    }

    public void PlaySFXStatUp()
    {
        statUpSFXSource.Play();
    }

    public void PlaySFXPlayer(string action)
    {
        switch (action)
        {
            case "Melee":
                playerSFXSource.clip = playerMeleeSFX;
                break;
            case "Magic":
                playerSFXSource.clip = playerMagicSFX;
                break;
            case "Dodge":
                playerSFXSource.clip = playerDodgeSFX;
                break;
            case "Block":
                playerSFXSource.clip = playerBlockSFX;
                break;
            default:
                Debug.LogWarning("Invalid player action for SFX: " + action);
                break;
        }
        playerSFXSource.Play();
    }

    void FixedUpdate()
    {
        if (timer >= 5f && isDead)
        {
            DeadWaves();
            timer = 0f;
        }
        timer += Time.fixedDeltaTime;
    }

    void DeadWaves()
    {
        deadWaves.pitch = Random.Range(0.8f, 1.2f);
        deadWaves.Play();
    }

    public void PlaySFXGameOver()
    {
        backgroundMusicSource.Stop();
        playerSFXSource.clip = gameOverClip;
        playerSFXSource.Play();
        isDead = true;
    }

    public void PlaySFXEnemyDeath()
    {
        playerSFXSource.clip = enemyDeathClip;
        playerSFXSource.Play();
    }

    public void PlaySFXBuy()
    {
        buySFXSource.Play();
    }
}
