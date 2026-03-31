using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerScript : MonoBehaviour
{
    GameObject enemyPanel, optionPanel, player;
    PlayerDataHandler playerData;
    EnemyTemplate currentEnemy;
    List<float> playerStats = new List<float>();

    List<EnemyTemplate> act1Enemies = new List<EnemyTemplate>();

    void Start()
    {
        enemyPanel = GameObject.Find("EnemyPanel");
        optionPanel = GameObject.Find("OptionPanel");
        DisableCombatUI();
        act1Enemies.AddRange(Resources.LoadAll<EnemyTemplate>("Enemies/Act1"));
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerDataHandler>();
        playerStats = playerData.GetStats();
    }


    public void StartCombat()
    {
        UpdatePlayerStats();
        GenerateEnemy((int)playerStats[12]);

    }

    public void UpdatePlayerStats()
    {
        playerStats = playerData.GetStats();
    }

    private void GenerateEnemy(int currentAct)
    {
        switch (currentAct)
        {
            case 1:
                currentEnemy = act1Enemies[Random.Range(0, act1Enemies.Count)];
                break;
            case 2:
                //generate act 2 enemy
                break;
            case 3:
                //generate act 3 enemy
                break;
        }
    }




    void EnableCombatUI()
    {
        enemyPanel.SetActive(true);
        optionPanel.SetActive(true);
    }

    void DisableCombatUI()
    {
        enemyPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void ToggleCombatUI()
    {
        if (enemyPanel.activeSelf)
        {
            DisableCombatUI();
        }
        else
        {
            EnableCombatUI();
        }
    }

}
