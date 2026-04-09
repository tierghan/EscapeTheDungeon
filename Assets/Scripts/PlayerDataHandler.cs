using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataHandler : MonoBehaviour, IDataPersistance
{
    GameObject combatManager, eventManager;
    [SerializeField] 
    TMP_Text gameOverText;
    [SerializeField]
    GameObject gameOverWindow;
    private float playerHealth,playerEnergy;
    private float playerStr,playerDex,playerMagic,playerMaxHealth, playerMaxEnergy, crystals;

    private int playerDamageReduction, playerDodgeChance, playerCritChance, gold, playerHPPotions, currentAct;

    private int strUpgradeLevel, dexUpgradeLevel, magUpgradeLevel, drUpgradeLevel, crystalUpgradeLevel, goldUpgradeLevel, potionUpgradeLevel, maxHPUpgradeLevel;
    /*
        0 | Str = Damage Modifier.
        1 | Dex = Dodge + Crit chance
        2 | Magic = Magic Damage
        3 | Health = Health
        4 | Max Health = Max Health
        5 | Energy = Resource to use Melee and Magic Attacks.
        6 | Max Energy = Max Energy

        7 | Damage Reduction = % of damage reduced from incoming attacks.
        8 | Dodge Chance = % chance to dodge attacks.
        9 | Crit Chance = % chance to deal 1.5x damage on melee attacks.
        10| Gold = Currency for purchasing items.
        11| Health Potions = Number of health potions the player has.
        12| Current Act = The act the player is currently in.
        13| Crystals = Currency for purchasing permanent upgrades.
    */
    
    // Can be used to add or subtract from any of the player's stats. Used for events and item effects. Try to avoid passing floats into int stats.
    
    public List<float> GetStats()
    {
        List<float> stats = new List<float>();
        stats.Add(playerStr);
        stats.Add(playerDex);
        stats.Add(playerMagic);
        stats.Add(playerHealth);
        stats.Add(playerMaxHealth);
        stats.Add(playerEnergy);
        stats.Add(playerMaxEnergy);
        stats.Add(playerDamageReduction);
        stats.Add(playerDodgeChance);
        stats.Add(playerCritChance);
        stats.Add(gold);
        stats.Add(playerHPPotions);
        stats.Add(currentAct);
        stats.Add(crystals);
        return stats;
    }

    public List<int> GetUpgradeLevels()
    {
        List<int> upgradeLevels = new List<int>();
        upgradeLevels.Add(strUpgradeLevel);
        upgradeLevels.Add(dexUpgradeLevel);
        upgradeLevels.Add(magUpgradeLevel);
        upgradeLevels.Add(drUpgradeLevel);
        upgradeLevels.Add(crystalUpgradeLevel);
        upgradeLevels.Add(goldUpgradeLevel);
        upgradeLevels.Add(potionUpgradeLevel);
        upgradeLevels.Add(maxHPUpgradeLevel);
        return upgradeLevels;
    }

    public void SetUpgradeLevels(List<int> levels)
    {
        strUpgradeLevel = levels[0];
        dexUpgradeLevel = levels[1];
        magUpgradeLevel = levels[2];
        drUpgradeLevel = levels[3];
        crystalUpgradeLevel = levels[4];
        goldUpgradeLevel = levels[5];
        potionUpgradeLevel = levels[6];
        maxHPUpgradeLevel = levels[7];
    }

    public void SetCrystals(int amount)
    {
        crystals = (float)amount;
    }
    

    public int GetGold()
    {
        return gold;
    }

    public int GetHealthPotions()
    {
        return playerHPPotions;
    }

    private void FixedUpdate() 
    {   
        playerDodgeChance = Mathf.Clamp((int)playerDex, 0, 100);
        playerCritChance = Mathf.Clamp((int)playerDex, 0, 100);
    }


    // Deals % based damage to player. Ignores DR and attacker crit chance. Passed int is the % of players max hp to be delt in damage.
    public void PercentageDamage(int damage)
    {
        int damageAmount = (int)(playerMaxHealth * (damage / 100f));
        playerHealth -= damageAmount;
        if (playerHealth <= 0)
        {
            GameOver();
        }
        combatManager.GetComponent<CombatManagerScript>().UpdateHealthBars();
    }

    // Heals player based on the passed int as a % against player max hp.
    public void HealPercentage(int healAmount)
    {
        playerHealth += (int)(playerMaxHealth * (healAmount / 100f));
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        combatManager.GetComponent<CombatManagerScript>().UpdateHealthBars();
    }

    // Deals flat damage to player. DR is applied. Passed int is the attackers damage after crits and other modifiers applied.
    public void FlatDamage(int damage)
    {
        int damageAmount = Mathf.Clamp((int)(damage - (damage * (playerDamageReduction / 100f))), 0, int.MaxValue);
        playerHealth -= damageAmount;
        if (playerHealth <= 0)
        {
            GameOver();
        }
        combatManager.GetComponent<CombatManagerScript>().UpdateHealthBars();
    }
    
    public void AddStat(int stat, float amount)
    {
        switch (stat)
        {
            case 0:
                playerStr += amount;
                break;
            case 1:
                playerDex += amount;
                break;
            case 2:
                playerMagic += amount;
                break;
            case 3:
                playerHealth += amount;
                if (playerHealth <= playerMaxHealth)
                {
                    GameOver();
                }
                else if (playerHealth > playerMaxHealth)
                {
                    playerHealth = playerMaxHealth;
                }
                break;
            case 4:
                playerMaxHealth += amount;
                playerHealth += amount;
                break;
            case 5:
                playerEnergy += amount;
                if (playerEnergy > playerMaxEnergy)
                {
                    playerEnergy = playerMaxEnergy;
                }
                break;
            case 6:
                playerMaxEnergy += amount;
                break;
            case 7:
                playerDamageReduction += (int)amount;
                if (playerDamageReduction > 100)
                {
                    playerDamageReduction = 100;
                }
                break;
            case 8:
                playerDodgeChance += (int)amount;
                if (playerDodgeChance > 100)
                {
                    playerDodgeChance = 100;
                }
                break;
            case 9:
                playerCritChance += (int)amount;
                if (playerCritChance > 50)
                {
                    playerCritChance = 50;
                }
                break;
            case 10:
                if (goldUpgradeLevel == 1)
                {
                    gold += (int)(amount + (amount * 0.2f));
                }
                else
                {
                    gold += (int)amount;
                }
                gold += (int)amount;
                break;
            case 11:
                playerHPPotions += (int)amount;
                break;
            case 12:
                currentAct += (int)amount;
                break;
            case 13:
                if (crystalUpgradeLevel == 1)
                {
                    crystals += (amount + (amount * 0.2f));
                }
                else
                {
                    crystals += amount;
                    
                }
                break;
            default:
                Debug.Log("Invalid stat index.");
                break;
        }
    }

    public void AddRandomStat(int multiplier)
    {
        int stat = Random.Range(0, 6);
        switch (stat)
        {
            case 0:
                playerStr += multiplier;
                break;
            case 1:
                playerDex += multiplier;
                break;
            case 2:
                playerMagic += multiplier;
                break;
            case 3:
                playerMaxHealth += multiplier;
                break;
            case 4:
                playerMaxEnergy += multiplier;
                break;
            case 5:
                playerDamageReduction += multiplier;
                break;
        }
    }

    public void SetCurrentAct(int act)
    {
        currentAct = act;
    }

    void GameOver()
    {
        SetGameOverPlayerData();
        combatManager.GetComponent<CombatManagerScript>().DisableCombatUI();
        eventManager.GetComponent<EventCardSystemScript>().HideEventWindow();
        gameOverWindow.SetActive(true);
        gameOverText.text = "You have died.\n\nYou have " + crystals + " crystals.\n\nWould you like to spend them on upgrades before starting a new run?";
    }

    public void SetGameOverPlayerData()
    {
        playerHealth = 20f;
        playerMaxHealth = 20f + (maxHPUpgradeLevel * 5);
        playerEnergy = 10f;
        playerMaxEnergy = 10f;
        playerStr = 10f + strUpgradeLevel;
        playerDex = 10f + dexUpgradeLevel;
        playerMagic = 10f + magUpgradeLevel;
        playerDamageReduction = 0 + drUpgradeLevel;
        playerDodgeChance = 0;
        playerCritChance = 0;
        gold = 0;
        playerHPPotions = 0 + potionUpgradeLevel;
        currentAct = 1;
    }

    
    
    public void LoadData(GameData data)
    {
        this.playerHealth = data.playerHealth;
        this.playerMaxHealth = data.playerMaxHealth;
        this.playerEnergy = data.playerEnergy;
        this.playerMaxEnergy = data.playerMaxEnergy;
        this.playerStr = data.playerStr;
        this.playerDex = data.playerDex;
        this.playerMagic = data.playerMagic;
        this.playerDamageReduction = data.playerDamageReduction;
        this.playerDodgeChance = data.playerDodgeChance;
        this.playerCritChance = data.playerCritChance;
        this.gold = data.gold;
        this.playerHPPotions = data.playerHPPotions;
        this.currentAct = data.currentAct;
        this.crystals = data.crystals;
        this.strUpgradeLevel = data.strUpgradeLevel;
        this.dexUpgradeLevel = data.dexUpgradeLevel;
        this.magUpgradeLevel = data.magUpgradeLevel;
        this.drUpgradeLevel = data.drUpgradeLevel;
        this.crystalUpgradeLevel = data.crystalUpgradeLevel;
        this.goldUpgradeLevel = data.goldUpgradeLevel;
        this.potionUpgradeLevel = data.potionUpgradeLevel;
    }

    void Start()
    {
        combatManager = GameObject.Find("CombatWindow");      
        eventManager = GameObject.Find("EventWindow");  
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = this.playerHealth;
        data.playerMaxHealth = this.playerMaxHealth;
        data.playerEnergy = this.playerEnergy;
        data.playerMaxEnergy = this.playerMaxEnergy;
        data.playerStr = this.playerStr;
        data.playerDex = this.playerDex;
        data.playerMagic = this.playerMagic;
        data.playerDamageReduction = this.playerDamageReduction;
        data.playerDodgeChance = this.playerDodgeChance;
        data.playerCritChance = this.playerCritChance;
        data.gold = this.gold;
        data.playerHPPotions = this.playerHPPotions;
        data.currentAct = this.currentAct;
        data.crystals = this.crystals;
        data.strUpgradeLevel = this.strUpgradeLevel;
        data.dexUpgradeLevel = this.dexUpgradeLevel;
        data.magUpgradeLevel = this.magUpgradeLevel;
        data.drUpgradeLevel = this.drUpgradeLevel;
        data.crystalUpgradeLevel = this.crystalUpgradeLevel;
        data.goldUpgradeLevel = this.goldUpgradeLevel;
        data.potionUpgradeLevel = this.potionUpgradeLevel;
    }
}
