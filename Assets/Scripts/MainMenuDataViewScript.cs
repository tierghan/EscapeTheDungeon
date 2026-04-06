using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuDataViewScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    GameObject player;
    GameObject dataPersistanceManager;

    float crystals;
    List<float> playerStats = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dataPersistanceManager = GameObject.Find("DataPersistanceManager");
        
        // dataPersistanceManager.GetComponent<DataPersistanceManager>().LoadGame();
        
        playerStats = player.GetComponent<PlayerDataHandler>().GetStats();
        

    }

    // Update is called once per frame
    void Update()
    {
        crystals = playerStats[13];
        text.text = "Crystals: " + crystals.ToString();
    }
}
