using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugToolsScipt : MonoBehaviour
{
    [SerializeField]
    TMP_Text statText;
    GameObject player, debugParent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        debugParent = GameObject.Find("DebugToolsParent");
        debugParent.SetActive(false);
    }
    public void updateStatDisplay()
    {
        List<float> stats = player.GetComponent<PlayerDataHandler>().GetStats();
        statText.text = "Str: " + stats[0] + "\nDex: " + stats[1] + "\nMagic: " + stats[2] + "\nHealth: " + stats[3] + "/" + stats[4] + "\nEnergy: " + stats[5] + "/" + stats[6] + "\nDamage Reduction: " + stats[7] + "%\nDodge Chance: " + stats[8] + "%\nCrit Chance: " + stats[9] + "%\nGold: " + stats[10] + "\n " + "Health Potions: " + stats[11] + "\nCurrent Act: " + stats[12];
    }

    private void EnableDebugTools()
    {
        debugParent.SetActive(true);
    }
    
    private void DisableDebugTools()
    {
        debugParent.SetActive(false);
    }

    public void ToggleDebugTools()
    {
        if (debugParent.activeSelf)
        {
            DisableDebugTools();
        }
        else
        {
            EnableDebugTools();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ToggleDebugTools();
        }
        updateStatDisplay();
    }

    public void FullHealPlayer()
    {
        player.GetComponent<PlayerDataHandler>().HealPercentage(100);
        updateStatDisplay();
    }
}
