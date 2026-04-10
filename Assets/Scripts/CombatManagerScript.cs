using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CombatManagerScript : MonoBehaviour
{
    GameObject enemyPanel, optionPanel, player;
    [SerializeField]
    GameObject combatUIParent, progressionPanel;
    [SerializeField]
    TMP_Text enemyNameText, combatLogText, playerStatsText, enemyStatsText;
    [SerializeField]
    public Slider playerHealthBar, enemyHealthBar;
    PlayerDataHandler playerData;
    EnemyTemplate currentEnemy;
    float enemyHealth, enemyStr, enemyDex, enemyMagic, enemyDodgeChance, enemyCritChance, enemyDamageReduction;
    string enemyFightingStyle, combatLogOutput, enemyName;
    int turnCounter, combatRewardGold, combatRewardCrystals;
    bool enemyDefending,enemyDodging, allowPlayerInput, playerDodging, playerDefending, playerFleeing, enemyIsBoss;
    List<float> playerStats;

    List<EnemyTemplate> act1Enemies = new List<EnemyTemplate>();
    List<EnemyTemplate> act1BossEnemies = new List<EnemyTemplate>();

    void Start()
    {

        enemyPanel = GameObject.Find("EnemyPanel");
        optionPanel = GameObject.Find("CombatOptionPanel");
        act1Enemies.AddRange(Resources.LoadAll<EnemyTemplate>("Enemies/Act1/Normal"));
        act1BossEnemies.AddRange(Resources.LoadAll<EnemyTemplate>("Enemies/Act1/Boss"));
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerDataHandler>();
        playerStats = playerData.GetStats();
        GenerateEnemy(1);
        DisableCombatUI();
    }


    public void StartCombat()
    {
        UpdatePlayerStats();
        GenerateEnemy((int)playerStats[12]);
        playerData.FillEnergy();
        enemyName = currentEnemy.enemyName;
        turnCounter = 0;
        combatLogOutput = "A wild " + enemyName + " appears!";
        UpdateHealthBars();
        enemyNameText.text = enemyName;
        combatLogText.text = combatLogOutput;
        playerFleeing = false;
        PlayerTurn();
        EnableCombatUI();
    }

    public void EndCombat()
    {
        allowPlayerInput = false;
        if (playerFleeing)
        {
            combatRewardCrystals = 0;
            combatRewardGold = 0;
            combatLogOutput = "You ran from the fight. You earned no rewards.";
        }
        else 
        {
            combatRewardGold = (int)currentEnemy.goldReward;
            combatRewardCrystals = (int)currentEnemy.actID;
            if (enemyIsBoss)
            {
                combatRewardCrystals *= 2;
                combatRewardGold *= 2;
                progressionPanel.GetComponent<ProgressionManagerScript>().IncrementAct();
            }
            combatLogOutput += "You defeated " + enemyName + "! You earned " + combatRewardGold + " gold and " + combatRewardCrystals + " crystals.";
            playerData.AddStat(10, combatRewardGold);
            playerData.AddStat(13, combatRewardCrystals);
        }
        PrintCombatLogLine();
        DisableCombatUI();
        progressionPanel.GetComponent<ProgressionManagerScript>().NewExplore();
    }

    void PlayerTurn()
    {
        turnCounter++;
        playerDefending = false;
        playerDodging = false;
        combatLogOutput = "\n\nTurn " + turnCounter + ": ";
        playerData.AddStat(5, 2);
        UpdatePlayerStats();
        if (enemyHealth > 0)
        {
            allowPlayerInput = true;
        }
        else
        {
            EndCombat();
        }
    }

    void EnemyTurn()
    {
        UpdateHealthBars();
        turnCounter++;
        int enemyDecision = 1;
        bool crit = false;
        enemyDefending = false;
        enemyDodging = false;
        combatLogOutput = "\n\nTurn " + turnCounter + ": ";
        if (enemyFightingStyle == "Brute")
        {
            enemyDecision = Random.Range(0, 2);
            if (enemyDecision == 0)            {
                crit = EnemyStrAttack();
                if (crit)
                {
                    //TODO - play crit sfx
                }
                else
                {
                    //TODO - play normal attack sfx
                }
            }
            else
            {
                EnemyDefend();
            }
        }
        else if (enemyFightingStyle == "Agile")
        {
            enemyDecision = Random.Range(0, 10);
            if (enemyDecision <= 4)
            {
                EnemyDodge();
            }
            else if (enemyDecision > 4 && enemyDecision <= 7)
            {
                crit = EnemyStrAttack();
                if (crit)
                {
                    //TODO - play crit sfx
                }
                else
                {
                    //TODO - play normal attack sfx
                }
            }
            else
            {
                EnemyDefend();
            }
        }
        else if (enemyFightingStyle == "Cowardly")
        {
            enemyDecision = Random.Range(0, 10);
            if (enemyDecision <= 6)
            {
                EnemyDodge();
            }
            else if (enemyDecision > 6 && enemyDecision <= 8)
            {
                EnemyDefend();
            }
            else
            {
                crit = EnemyStrAttack();
                if (crit)
                {
                    //TODO - play crit sfx
                }
                else
                {
                    //TODO - play normal attack sfx
                }
            }
        }
        else if (enemyFightingStyle == "Confidant")
        {
            enemyDecision = Random.Range(0, 10);
            if (enemyDecision <= 2)
            {
                EnemyDodge();
            }
            else if (enemyDecision > 2 && enemyDecision <= 4)
            {
                EnemyDefend();
            }
            else
            {
                crit = EnemyStrAttack();
                if (crit)
                {
                    //TODO - play crit sfx
                }
                else
                {
                    //TODO - play normal attack sfx
                }
            }
        }

        PrintCombatLogLine();
        PlayerTurn();
    }

    public void PrintCombatLogLine()
    {
        combatLogText.text += combatLogOutput;
    }

    bool EnemyStrAttack()
    {
        int damage = (int)enemyStr;
        int critCheck = Random.Range(0, 100);
        if (critCheck < enemyCritChance)
        {
            damage *= 2;
            combatLogOutput += "CRIT. ";
        }
        if (playerDodging)
        {
            int dodgeCheck = Random.Range(0, 100);
            if (dodgeCheck < playerStats[8])
            {
                combatLogOutput += "You dodged the attack! ";
                damage = 0;
            }
        }
        else if (playerDefending)
        {
            damage = (int)(damage - (damage * 0.5f));
            combatLogOutput += "You were defending, reducing the damage. ";
        }
        damage = (int)(damage - (playerData.GetStats()[7]));
        playerData.FlatDamage(damage);
        combatLogOutput += enemyName + " dealt " + damage + " damage. ";
        UpdateHealthBars();
        return true;
    }

    void EnemyMagAttack()
    {
        int damage = (int)enemyMagic;
        damage = (int)(damage - (playerData.GetStats()[7]));
        if (playerDodging)
        {
            int dodgeCheck = Random.Range(0, 100);
            if (dodgeCheck < playerStats[8])
            {
                combatLogOutput += "You dodged the attack! ";
                damage = 0;
            }
        }
        playerData.FlatDamage(damage);
        combatLogOutput += enemyName + " dealt " + damage + " magic damage, ignoring your defenses. ";
        UpdateHealthBars();
    }

    void EnemyDefend()
    {
        enemyDefending = true;
        combatLogOutput += enemyName + " goes on guard. ";
    }

    void EnemyDodge()
    {
        enemyDodging = true;
        combatLogOutput += enemyName + " readies itself to dodge. ";
    }

    public void PlayerMeleeStrike()
    {
        if (allowPlayerInput == true)
        {
            if (playerStats[5]>=3)
            {
                playerData.AddStat(5, -3);
                int damage = (int)playerStats[0];
                damage = (int)(damage - (enemyDamageReduction));
                if (enemyDefending)
                {
                    damage = (int)(damage - (damage * 0.5f));
                    combatLogOutput += "You strike " + enemyName + " for " + damage + " damage, but they were defending, reducing the damage. ";
                }
                else if (enemyDodging)
                {
                    int dodgeCheck = Random.Range(0, 100);
                    if (dodgeCheck < enemyDodgeChance)
                    {
                        combatLogOutput += "You strike at " + enemyName + ", but they dodged the attack! ";
                        damage = 0;
                    }
                    else
                    {
                        combatLogOutput += "You strike " + enemyName + " for " + damage + " damage. ";
                    }
                }
                else
                {
                    combatLogOutput += "You strike " + enemyName + " for " + damage + " damage. ";
                }
                int critCheck = Random.Range(0, 100);
                if (critCheck < playerStats[9])
                {
                    damage *= 2;
                    combatLogOutput += "CRITICAL HIT.";
                }
                enemyHealth -= damage;
                UpdateHealthBars();
                allowPlayerInput = false;
                PrintCombatLogLine();
                if (enemyHealth > 0)
                {
                    EnemyTurn();
                }
                else
                {
                    EndCombat();
                }
            }
        }
    }

    public void PlayerMagicStrike()
    {
        if (allowPlayerInput == true)
        {
            if (playerStats[5]>=5)
            {
                playerData.AddStat(5, -5);
                int damage = (int)playerStats[2];
                combatLogOutput += "You strike " + enemyName + " for " + damage + " magic damage, ignoring their defenses. ";
                if (enemyDodging)
                {
                    int dodgeCheck = Random.Range(0, 100);
                    if (dodgeCheck < enemyDodgeChance)
                    {
                        combatLogOutput += "They dodged the attack! ";
                        damage = 0;
                    }
                }
                enemyHealth -= damage;
                UpdateHealthBars();
                allowPlayerInput = false;
                PrintCombatLogLine();
                if (enemyHealth > 0)
                {
                    EnemyTurn();
                }
                else
                {
                    EndCombat();
                }
            }
        }
    }

    public void PlayerDodge()
    {
        if (allowPlayerInput == true)
        {
            if (playerStats[5]>=3)
            {
                playerData.AddStat(5, -3);
                combatLogOutput += "You ready yourself to dodge. ";
                playerDodging = true;
                allowPlayerInput = false;
                PrintCombatLogLine();
                EnemyTurn();
            }
        }
    }

    public void PlayerDefend()
    {
        if (allowPlayerInput == true)
        {
            if (playerStats[5] >= 0)
            {
                combatLogOutput += "You go on guard. ";
                playerDefending = true;
                allowPlayerInput = false;
                playerData.AddStat(5, 0);
                PrintCombatLogLine();
                EnemyTurn();
            }
            
        }
        
    }

    public void PlayerFlee()
    {
        if (allowPlayerInput == true)
        {
            
            playerFleeing = true;
            allowPlayerInput = false;
            combatLogOutput += "You flee from the fight. ";
            PrintCombatLogLine();
            EndCombat();
        }
    }



    public void UpdatePlayerStats()
    {
        playerStats = playerData.GetStats();
        playerStatsText.text = "Str: " + playerStats[0] + "\nDex: " + playerStats[1] + "\nMagic: " + playerStats[2] + "\nHealth: " + playerStats[3] + "/" + playerStats[4] + "\nEnergy: " + playerStats[5] + "/" + playerStats[6] + "\nDamage Reduction: " + playerStats[7] + "%\nDodge Chance: " + playerStats[8] + "%\nCrit Chance: " + playerStats[9] + "%\nGold: " + playerStats[10];
    }

    public void UpdateHealthBars()
    {
        playerHealthBar.value = playerStats[3] / playerStats[4];
        enemyHealthBar.value = enemyHealth / currentEnemy.enemyMaxHealth;
        UpdatePlayerStats();
        UpdateEnemyStats();
    }

    public void UpdateEnemyStats()
    {
        enemyStatsText.text = "Str: " + enemyStr + "\nDex: " + enemyDex + "\nMagic: " + enemyMagic + "\nHealth: " + enemyHealth + "/" + currentEnemy.enemyMaxHealth + "\nDodge Chance: " + enemyDodgeChance + "%\nCrit Chance: " + enemyCritChance + "%\nDamage Reduction: " + enemyDamageReduction;
    }


    // Act Bosses are on the negated act values.
    // ex. Act 1 Boss = -1
    private void GenerateEnemy(int currentAct)
    {
        switch (currentAct)
        {
            case 1:
                currentEnemy = act1Enemies[Random.Range(0, act1Enemies.Count)];
                enemyHealth = currentEnemy.enemyMaxHealth;
                enemyStr = currentEnemy.enemyStr;
                enemyDex = currentEnemy.enemyDex;
                enemyMagic = currentEnemy.enemyMagic;
                enemyDodgeChance = currentEnemy.enemyDodgeChance;
                enemyCritChance = currentEnemy.enemyCritChance;
                enemyDamageReduction = currentEnemy.enemyDamageReduction;
                enemyFightingStyle = currentEnemy.fightingStyle.ToString();
                enemyIsBoss = currentEnemy.isBoss;
                break;
            case -1: // Act 1 Boss
                currentEnemy = act1BossEnemies[Random.Range(0, act1BossEnemies.Count)];
                enemyHealth = currentEnemy.enemyMaxHealth;
                enemyStr = currentEnemy.enemyStr;
                enemyDex = currentEnemy.enemyDex;
                enemyMagic = currentEnemy.enemyMagic;
                enemyDodgeChance = currentEnemy.enemyDodgeChance;
                enemyCritChance = currentEnemy.enemyCritChance;
                enemyDamageReduction = currentEnemy.enemyDamageReduction;
                enemyFightingStyle = currentEnemy.fightingStyle.ToString();
                enemyIsBoss = currentEnemy.isBoss;
                break;
            case 2:
                //generate act 2 enemy
                break;
            case 3:
                //generate act 3 enemy
                break;
        }
    }

    public void StartBossCombat(int currentAct)
    {

        Debug.Log("Starting boss combat for Act " + currentAct);
        UpdatePlayerStats();
        GenerateEnemy(currentAct);
        playerData.FillEnergy();
        enemyName = currentEnemy.enemyName;
        turnCounter = 0;
        combatLogOutput = "Boss Fight! The " + enemyName + " stands before you!";
        UpdateHealthBars();
        enemyNameText.text = enemyName;
        combatLogText.text = combatLogOutput;
        playerFleeing = false;
        PlayerTurn();
    }



    public void EnableCombatUI()
    {
        combatUIParent.SetActive(true);
    }

    public void DisableCombatUI()
    {
        combatUIParent.SetActive(false);
    }

    public void ToggleCombatUI()
    {
        if (combatUIParent.activeSelf)
        {
            DisableCombatUI();
        }
        else
        {
            EnableCombatUI();
        }
    }
}
