using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour, IDataPersistance
{
    private float playerHealth,playerEnergy;
    private float playerStr,playerDex,playerMagic;

    private int playerDamageReduction, playerDodgeChance, playerCritChance;
    /*
        Str = Damage Modifier.
        Dex = Dodge + Crit chance
        Magic = Magic Damage
        Health = Health
        Energy = Resource to use Melee and Magic Attacks.

        Damage Reduction = % of damage reduced from incoming attacks.
        Dodge Chance = % chance to dodge attacks.
        Crit Chance = % chance to deal 1.5x damage on melee attacks.
    */
    public void LoadData(GameData data)
    {
        this.playerHealth = data.playerHealth;
        this.playerEnergy = data.playerEnergy;
        this.playerStr = data.playerStr;
        this.playerDex = data.playerDex;
        this.playerMagic = data.playerMagic;
        this.playerDamageReduction = data.playerDamageReduction;
        this.playerDodgeChance = data.playerDodgeChance;
        this.playerCritChance = data.playerCritChance;    
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = this.playerHealth;
        data.playerEnergy = this.playerEnergy;
        data.playerStr = this.playerStr;
        data.playerDex = this.playerDex;
        data.playerMagic = this.playerMagic;
        data.playerDamageReduction = this.playerDamageReduction;
        data.playerDodgeChance = this.playerDodgeChance;
        data.playerCritChance = this.playerCritChance;
    }
}
