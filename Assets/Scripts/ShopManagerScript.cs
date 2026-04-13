using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    [SerializeField] 
    GameObject player, progressionManager;
    [SerializeField]
    TMP_Text goldText;
    public void ShowShop()
    {
        gameObject.SetActive(true);
        updateText();
    }
    public void HideShop()
    {
        gameObject.SetActive(false);
    }

     void Start()
    {
        HideShop();
    }

    int getPlayerGold()
    {
        return (int)player.GetComponent<PlayerDataHandler>().GetStats()[10];
    }

    void updateText()
    {
        int playerGold = getPlayerGold();
        goldText.text = "Current Gold\n" + playerGold;
    }

    public void BuyStatIncrease(int statIndex)
    {
        int playerGold = getPlayerGold();
        if (playerGold >= 100 && statIndex <= 2)
        {
            player.GetComponent<PlayerDataHandler>().AddStat(statIndex, 1);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -100);
        }
        else if(playerGold >= 150 && statIndex == 4 || statIndex == 6)
        {
            player.GetComponent<PlayerDataHandler>().AddStat(statIndex, 5);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -150);
        }
        else if(playerGold >= 250 && statIndex == 7)
        {
            player.GetComponent<PlayerDataHandler>().AddStat(statIndex, 1);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -250);
        }
        else
        {
            Debug.Log("Not enough gold to purchase stat increase or invalid stat index.");
        }
        updateText();
    }

    public void BuyHealing()
    {
        int playerGold = getPlayerGold();
        if (playerGold >= 50)
        {
            player.GetComponent<PlayerDataHandler>().HealPercentage(30);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -50);
        }
        else
        {
            Debug.Log("Not enough gold to purchase healing.");
        }
        updateText();

    }

    public void LeaveShop()
    {
        HideShop();
        progressionManager.GetComponent<ProgressionManagerScript>().NewExplore();
    }
}
