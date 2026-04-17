using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyFightingStyle
{
    Brute,
    Agile,
    Cowardly,
    Confidant,
    MagicAggressive,
    MagicDefensive,
    Wild,
    MagicCowardly
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Make new Enemy")]
public class EnemyTemplate : ScriptableObject
{
    public string enemyName;
    public int enemyMaxHealth, enemyStr, enemyDex, enemyMagic, enemyDamageReduction, enemyDodgeChance, enemyCritChance, goldReward, actID, magicResistance;
    public EnemyFightingStyle fightingStyle;

    public bool isBoss;

}
