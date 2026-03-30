using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugToolsScipt : MonoBehaviour
{
    [SerializeField]
    TMP_Text statText;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void updateStatDisplay()
    {
        List<float> stats = player.GetComponent<PlayerDataHandler>().GetStats();
        statText.text = "Str: " + stats[0] + "\nDex: " + stats[1] + "\nMagic: " + stats[2] + "\nHealth: " + stats[3] + "/" + stats[4] + "\nEnergy: " + stats[5] + "/" + stats[6] + "\nDamage Reduction: " + stats[7] + "%\nDodge Chance: " + stats[8] + "%\nCrit Chance: " + stats[9] + "%\nGold: " + stats[10] + "\n ";
    }
}
