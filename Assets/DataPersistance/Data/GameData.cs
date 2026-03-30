using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData

{
    // add vars here to initialize and save. (public type varName;)
    public float playerHealth,playerEnergy,playerMaxHealth, playerMaxEnergy;
    public float playerStr,playerDex,playerMagic;
    public int playerDamageReduction, playerDodgeChance, playerCritChance, gold, playerHPPotions;

    //defined here are stating values for vars saved. (this.varName = defaultValue;)
    public GameData()
    {
        this.playerHealth = 100f;
        this.playerMaxHealth = 100f;
        this.playerEnergy = 10f;
        this.playerMaxEnergy = 10f;
        this.playerStr = 10f;
        this.playerDex = 10f;
        this.playerMagic = 10f;
        this.playerDamageReduction = 0;
        this.playerDodgeChance = 0;
        this.playerCritChance = 0;
        this.gold = 0;
        this.playerHPPotions = 0;
    }
}
