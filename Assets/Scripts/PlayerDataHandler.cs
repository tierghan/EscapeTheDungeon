using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour, IDataPersistance
{
    private float playerHealth,playerEnergy;
    private float playerStr,playerDex,playerMagic,playerMaxHealth, playerMaxEnergy;

    private int playerDamageReduction, playerDodgeChance, playerCritChance, gold;
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
        return stats;
    }

    public int GetGold()
    {
        return gold;
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
    }

    // Heals player based on the passed int as a % against player max hp.
    public void HealPercentage(int healAmount)
    {
        playerHealth += (int)(playerMaxHealth * (healAmount / 100f));
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    // Deals flat damage to player. DR is applied. Passed int is the attackers damage after crits and other modifiers applied.
    public void FlatDamage(int damage)
    {
        int damageAmount = (int)(damage - (damage * (playerDamageReduction / 100f)));
        playerHealth -= damageAmount;
        if (playerHealth <= 0)
        {
            GameOver();
        }
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
                break;
            case 5:
                playerEnergy += amount;
                break;
            case 6:
                playerMaxEnergy += amount;
                break;
            case 7:
                playerDamageReduction += (int)amount;
                break;
            case 8:
                playerDodgeChance += (int)amount;
                break;
            case 9:
                playerCritChance += (int)amount;
                break;
            case 10:
                gold += (int)amount;
                break;
            default:
                Debug.Log("Invalid stat index.");
                break;
        }
    }

    void GameOver()
    {
        //TODO: Game Over State
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
    }
}
