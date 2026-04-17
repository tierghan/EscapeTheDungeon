using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuDataViewScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    GameObject player;
    GameObject dataPersistanceManager;

    float crystals, act;
    List<float> playerStats = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dataPersistanceManager = GameObject.Find("DataPersistanceManager");  
    }

    void FixedUpdate()
    {
        UpdateText();
    }

    void UpdateText()
    {
        playerStats = player.GetComponent<PlayerDataHandler>().GetStats();
        crystals = playerStats[13];
        act = (int)playerStats[12];
        text.text = "Current Save:\n\nCrystals: " + crystals.ToString() + "\nAct: " + act.ToString();
    }
}
