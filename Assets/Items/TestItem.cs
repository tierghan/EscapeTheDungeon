using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Test Item (Change me)", menuName = "Make new Item")]
public class TestItem : ScriptableObject 
{
    public string itemName;
    public string description;

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
    */
    public bool equiped;
    public int strMod, dexMod, magicMod, maxHealthMod, maxEnergyMod, damageReductionMod, dodgeChanceMod, critChanceMod;
    public int itemTier; // Tier is value + 1, up to 5.
    public int itemType; // 0 = Weapon, 1 = Armor, 2 = Spellbook


}
