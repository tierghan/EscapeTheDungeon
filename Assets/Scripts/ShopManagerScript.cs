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
    [SerializeField]
    AudioManagerScript audioManager;

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
        bool purchased = true;
        if (playerGold >= 100 && statIndex <= 2)
        {
            player.GetComponent<PlayerDataHandler>().AddStat(statIndex, 1);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -100);
        }
        else if((playerGold >= 150 && statIndex == 4) || (playerGold >= 150 && statIndex == 6))
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
            purchased = false;
            Debug.Log("Not enough gold to purchase stat increase or invalid stat index.");
        }

        if (purchased)
        {
            audioManager.PlaySFXBuy();
        }
        else
        {
            audioManager.PlaySFXClick();
        }

        updateText();
    }

    public void BuyHealing()
    {
        int playerGold = getPlayerGold();
        bool purchased = true;
        if (playerGold >= 50)
        {
            player.GetComponent<PlayerDataHandler>().HealPercentage(30);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -50);
        }
        else
        {
            purchased = false;
            Debug.Log("Not enough gold to purchase healing.");
        }

        if (purchased)
        {
            audioManager.PlaySFXBuy();
        }
        else
        {
            audioManager.PlaySFXClick();
        }
        updateText();

    }

    public void BuyPotion()
    {
        int playerGold = getPlayerGold();
        bool purchased = true;
        if (playerGold >= 50)
        {
            player.GetComponent<PlayerDataHandler>().AddStat(11, 1);
            player.GetComponent<PlayerDataHandler>().AddStat(10, -50);
        }
        else
        {
            purchased = false;
            Debug.Log("Not enough gold to purchase potion.");
        }

        if (purchased)
        {
            audioManager.PlaySFXBuy();
        }
        else
        {
            audioManager.PlaySFXClick();
        }
        updateText();
    }

    public void LeaveShop()
    {
        audioManager.PlaySFXClick();
        HideShop();
        progressionManager.GetComponent<ProgressionManagerScript>().NewExplore();
    }
}
