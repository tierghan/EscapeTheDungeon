using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MetaUpgradeManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject upgradeWindow, player;
    [SerializeField]
    AudioManagerScript audioManager;
    int playerCrystals = 0;
    int strUpgradeMaxLevel = 20, dexUpgradeMaxLevel = 20, magUpgradeMaxLevel = 20, drUpgradeMaxLevel = 10, crystalUpgradeMaxLevel = 1, goldUpgradeMaxLevel = 1, potionUpgradeMaxLevel = 3, maxHPUpgradeMaxLevel = 10;
    List<int> upgradeLevels;
    bool windowClosed = false;

    [SerializeField]
    TMP_Text strNameText, dexNameText, magNameText, drNameText, crystalGainNameText, goldGainNameText, potionGainNameText, maxHPGainNameText, crystalAmountText;

    public void OpenUpgradeWindow()
    {
        upgradeWindow.SetActive(true);
        GetPlayerCrystals();
        GetPlayerUpgradeLevels();
        UpdateUpgradeText();
    }
    public void CloseUpgradeWindow()
    {
        if (windowClosed) audioManager.PlaySFXClick();
        upgradeWindow.SetActive(false);
        UpdatePlayerCrystals();
        UpdatePlayerUpgradeLevels();
        player.GetComponent<PlayerDataHandler>().SetGameOverPlayerData();
    }
    void GetPlayerCrystals()
    {
        playerCrystals = (int)player.GetComponent<PlayerDataHandler>().GetStats()[13];
    }

    void GetPlayerUpgradeLevels()
    {
        upgradeLevels = player.GetComponent<PlayerDataHandler>().GetUpgradeLevels();
    }

    void UpdatePlayerCrystals()
    {
        player.GetComponent<PlayerDataHandler>().SetCrystals(playerCrystals);
    }

    void UpdatePlayerUpgradeLevels()
    {
        player.GetComponent<PlayerDataHandler>().SetUpgradeLevels(upgradeLevels);
    }

    public void UpdateUpgradeText()
    {
        strNameText.text = "Increased Strength\n("+upgradeLevels[0]+"/"+strUpgradeMaxLevel+")";
        dexNameText.text = "Increased Dexterity\n("+upgradeLevels[1]+"/"+dexUpgradeMaxLevel+")";
        magNameText.text = "Increased Magic\n("+upgradeLevels[2]+"/"+magUpgradeMaxLevel+")";
        drNameText.text = "Increased Damage Reduction\n("+upgradeLevels[3]+"/"+drUpgradeMaxLevel+")";
        crystalGainNameText.text = "Increased Crystal Gain\n("+upgradeLevels[4]+"/"+crystalUpgradeMaxLevel+")";
        goldGainNameText.text = "Increased Gold Gain\n("+upgradeLevels[5]+"/"+goldUpgradeMaxLevel+")";
        potionGainNameText.text = "Starting Health Potions\n("+upgradeLevels[6]+"/"+potionUpgradeMaxLevel+")";
        maxHPGainNameText.text = "Increased Max Health\n("+upgradeLevels[7]+"/"+maxHPUpgradeMaxLevel+")";
        crystalAmountText.text = "Crystals: " + playerCrystals;
    }

    void Start()
    {
        CloseUpgradeWindow();
        windowClosed = true;
        GetPlayerCrystals();
        GetPlayerUpgradeLevels();
    }

    public void BuyStatUpgrade(int statIndex)
    {
        /*
        Upgrade Levels:
        0- STR
        1- DEX
        2- MAG
        3- DR
        4- Crystal Gain
        5- Gold Gain
        6- Health Potion Gain
        7- Max Health Gain
        */
        switch(statIndex)
        {
            //STR
            case 0:
                if(playerCrystals >= 5 && upgradeLevels[0] < strUpgradeMaxLevel)
                {
                    upgradeLevels[0]++;
                    playerCrystals -= 5;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //DEX
            case 1:
                if(playerCrystals >= 5 && upgradeLevels[1] < dexUpgradeMaxLevel)
                {
                    upgradeLevels[1]++;
                    playerCrystals -= 5;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //MAG
            case 2:
                if(playerCrystals >= 5 && upgradeLevels[2] < magUpgradeMaxLevel)
                {
                    upgradeLevels[2]++;
                    playerCrystals -= 5;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //DR
            case 3:
                if(playerCrystals >= 10 && upgradeLevels[3] < drUpgradeMaxLevel)
                {
                    upgradeLevels[3]++;
                    playerCrystals -= 10;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
             //Crystal Gain
            case 4:
                if(playerCrystals >= 30 && upgradeLevels[4] < crystalUpgradeMaxLevel)
                {
                    upgradeLevels[4]++;
                    playerCrystals -= 30;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //Gold Gain
            case 5:
                if(playerCrystals >= 20 && upgradeLevels[5] < goldUpgradeMaxLevel)
                {
                    upgradeLevels[5]++;
                    playerCrystals -= 20;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //Health Potion Gain
            case 6:
                if(playerCrystals >= 15 && upgradeLevels[6] < potionUpgradeMaxLevel)
                {
                    upgradeLevels[6]++;
                    playerCrystals -= 15;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            //Max Health Gain
            case 7:
                if(playerCrystals >= 5 && upgradeLevels[7] < maxHPUpgradeMaxLevel)
                {
                    upgradeLevels[7]++;
                    playerCrystals -= 5;
                    audioManager.PlaySFXBuy();
                }
                else
                {
                    audioManager.PlaySFXClick();
                }
                break;
            default:
                Debug.Log("Invalid stat index. Given index: " + statIndex);
                break;
            
        }
        UpdateUpgradeText();
        UpdatePlayerUpgradeLevels();
        UpdatePlayerCrystals();
    }
}
