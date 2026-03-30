using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManagerScript : MonoBehaviour, IDataPersistance
{
    GameObject player;
    [SerializeField] private TextMeshProUGUI consumableText;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UpdateConsumableText();
    }

    public void UsePotion()
    {
        int currentPotions = player.GetComponent<PlayerDataHandler>().GetHealthPotions();
        if (currentPotions > 0)
        {
            player.GetComponent<PlayerDataHandler>().HealPercentage(30);
            player.GetComponent<PlayerDataHandler>().AddStat(11, -1);
            UpdateConsumableText();
        }
    }

    public void UpdateConsumableText()
    {
        consumableText.text = "Number of Health Potions: \n"+ player.GetComponent<PlayerDataHandler>().GetHealthPotions();
    }

    public void ToggleInventory()
    {
        if (gameObject.activeSelf)
        {
            DisableInventory();
        }
        else
        {
            EnableInventory();
        }

    }

    public void EnableInventory()
    {
        gameObject.SetActive(true);
    }

    public void DisableInventory()
    {
        gameObject.SetActive(false);
    }




    




    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
