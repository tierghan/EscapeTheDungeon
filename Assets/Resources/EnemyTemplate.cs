using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Make new Enemy")]

public class EnemyTemplate : ScriptableObject
{
    public string enemyName;
    public int enemyMaxHealth, enemyStr, enemyDex, enemyMagic, enemyDamageReduction, enemyDodgeChance, enemyCritChance, goldReward, actID;


}
