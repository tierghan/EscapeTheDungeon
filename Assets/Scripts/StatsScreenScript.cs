using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsScreenScript : MonoBehaviour
{
    [SerializeField]
    GameObject statsPanel, player;
    [SerializeField]
    TMP_Text statsTextLeft, statsTextRight;

    List<float> playerStats;

     void Start()
    {
        CloseStatsPanel();
    }

    public void OpenStatsPanel()
    {
        statsPanel.SetActive(true);
        UpdateStatsText();
    }
    public void CloseStatsPanel()
    {
        statsPanel.SetActive(false);
    }

    public void ToggleStatsPanel()
    {
        if (statsPanel.activeSelf)
        {
            CloseStatsPanel();
        }
        else
        {
            OpenStatsPanel();
        }
    }

    void CheckPlayerStats()
    {
        playerStats = player.GetComponent<PlayerDataHandler>().GetStats();
    }


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
     public void UpdateStatsText()
    {
        CheckPlayerStats();
        statsTextLeft.text = $"HP: {playerStats[3]}/{playerStats[4]}\nEnergy: {playerStats[5]}/{playerStats[6]}\nCurrent Act: {playerStats[12]}\nGold: {playerStats[10]}";
        statsTextRight.text = $"STR: {playerStats[0]}\nDEX: {playerStats[1]}\nWis: {playerStats[2]}\nDodge: {playerStats[8]}%\nCrit: {playerStats[9]}%\nDR: {playerStats[7]}";
    }

    private void FixedUpdate()
    {
        UpdateStatsText();
    }

}
