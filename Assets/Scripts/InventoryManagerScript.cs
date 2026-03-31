using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManagerScript : MonoBehaviour, IDataPersistance
{
    GameObject player;
    [SerializeField] private TextMeshProUGUI consumableText;

    List<string> inventory = new List<string>();

    GameObject itemInfoManager;
    TMP_Text inventorySlot1Name, inventorySlot1Desc, inventorySlot2Name, inventorySlot2Desc, inventorySlot3Name, inventorySlot3Desc, inventorySlot4Name, inventorySlot4Desc;

    TestItem itemSO;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        itemInfoManager = GameObject.Find("ItemInfoManager");
        UpdateConsumableText();

        inventorySlot1Name = GameObject.Find("InventorySlot1ItemName").GetComponent<TextMeshProUGUI>();
        inventorySlot1Desc = GameObject.Find("InventorySlot1ItemDesc").GetComponent<TextMeshProUGUI>();
        inventorySlot2Name = GameObject.Find("InventorySlot2ItemName").GetComponent<TextMeshProUGUI>();
        inventorySlot2Desc = GameObject.Find("InventorySlot2ItemDesc").GetComponent<TextMeshProUGUI>();
        inventorySlot3Name = GameObject.Find("InventorySlot3ItemName").GetComponent<TextMeshProUGUI>();
        inventorySlot3Desc = GameObject.Find("InventorySlot3ItemDesc").GetComponent<TextMeshProUGUI>();
        inventorySlot4Name = GameObject.Find("InventorySlot4ItemName").GetComponent<TextMeshProUGUI>();
        inventorySlot4Desc = GameObject.Find("InventorySlot4ItemDesc").GetComponent<TextMeshProUGUI>();
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




    public void AddItem(string itemName)
    {
        if(inventory.Count < 5)
        {
            inventory.Add(itemName);
        }
    }

    public void DisplayItemInSlot(int slotIndex)
    {
        switch (slotIndex)
        {   
            //Slot 1
            case 0:
                itemSO = itemInfoManager.GetComponent<ItemInfoManager>().GetScriptableObject(inventory[0]);
                inventorySlot1Name.text = itemSO.itemName;
                inventorySlot1Desc.text = itemSO.description;
                break;
            //Slot 2
            case 1:
                itemSO = itemInfoManager.GetComponent<ItemInfoManager>().GetScriptableObject(inventory[1]);
                inventorySlot2Name.text = itemSO.itemName;
                inventorySlot2Desc.text = itemSO.description;
                break;
            //Slot 3
            case 2:
                itemSO = itemInfoManager.GetComponent<ItemInfoManager>().GetScriptableObject(inventory[2]);
                inventorySlot3Name.text = itemSO.itemName;
                inventorySlot3Desc.text = itemSO.description;
                break;
            //Slot 4
            case 3:
                itemSO = itemInfoManager.GetComponent<ItemInfoManager>().GetScriptableObject(inventory[3]);
                inventorySlot4Name.text = itemSO.itemName;
                inventorySlot4Desc.text = itemSO.description;
                break;
            default:
                Debug.Log("Invalid inventory slot index. Tried to display item in slot index: " + slotIndex);
                break;
        }
    }



    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
