using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoManager : MonoBehaviour
{
    // This script is to let the inventory system fetch item info from item tierID and typeID. This should return each items
    // scriptable object from a given tier and type. Thanks to this, only the tier and type need to be saved when saving the game.

    // Type Map:
    // 0 | Weapon
    // 1 | Armor
    // 2 | Spellbook


    public string GetItemName(int itemTier, int itemType)
    {
        switch (itemType)
        {
            //weapon
            case 0:
                return GetWeaponName(itemTier);
                
            //armor
            case 1:
                return GetArmorName(itemTier);

            //spellbook
            case 2:
                return GetSpellbookName(itemTier);

            //test item
            case 10:
                return "TestItem";

            //default
            default:
                Debug.Log("Invalid item type.");
                return "Error: Invalid Item Type";
        }
    }

    public int GetItemTier(string itemName)
    {
        if (itemName.Contains("Broken") || itemName.Contains("Leather") || itemName.Contains("Healing"))
        {
            return 0;
        }
        else if (itemName.Contains("Copper") || itemName.Contains("Hardhide") || itemName.Contains("Fireball"))
        {
            return 1;
        }
        else if (itemName.Contains("Iron") || itemName.Contains("Chainmail") || itemName.Contains("Earthquake"))
        {
            return 2;
        }
        else if (itemName.Contains("Silver") || itemName.Contains("Plate") || itemName.Contains("Ice Spike"))
        {
            return 3;
        }
        else if (itemName.Contains("Sacred") || itemName.Contains("Paladin") || itemName.Contains("Protection"))
        {
            return 4;
        }
        else if (itemName.Contains("TestItem"))
        {
            return 10;
        }
        else
        {
            Debug.Log("Invalid item name.");
            return -1;
        }
    }

    public int GetItemType(string itemName)
    {
        if (itemName.Contains("Sword"))
        {
            return 0;
        }
        else if (itemName.Contains("Armor"))
        {
            return 1;
        }
        else if (itemName.Contains("Spellbook"))
        {
            return 2;
        }
        else if (itemName.Contains("TestItem"))
        {
            return 10;
        }
        else
        {
            Debug.Log("Invalid item name.");
            return -1;
        }
    }

    private string GetWeaponName(int itemTier)
    {
        switch (itemTier)
        {
            case 0:
                return "Broken Sword";
            case 1:
                return "Copper Sword";
            case 2:
                return "Iron Sword";
            case 3:
                return "Silver Sword";
            case 4:
                return "Sacred Sword";
            default:
                Debug.Log("Invalid item tier.");
                return "Error: Invalid Item Tier";
        }
    }

    private string GetArmorName(int itemTier)
    {
        switch (itemTier)
        {
            case 0:
                return "Leather Armor";
            case 1:
                return "Hardhide Armor";
            case 2:
                return "Chainmail Armor";
            case 3:
                return "Plate Armor";
            case 4:
                return "Paladin Armor";
            default:
                Debug.Log("Invalid item tier.");
                return "Error: Invalid Item Tier";
        }
    }

    private string GetSpellbookName(int itemTier)
    {
        switch (itemTier)
        {
            case 0:
                return "Healing Spellbook";
            case 1:
                return "Fireball Spellbook";
            case 2:
                return "Earthquake Spellbook";
            case 3:
                return "Ice Spike Spellbook";
            case 4:
                return "Protection Spellbook";
            default:
                Debug.Log("Invalid item tier.");
                return "Error: Invalid Item Tier";
        }
    }

    public TestItem GetScriptableObject(string itemName)
    {
        TestItem itemSO = Resources.Load<TestItem>("Items/" + itemName);
        return itemSO;
    }
}
