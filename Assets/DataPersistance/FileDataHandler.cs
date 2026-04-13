using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    #if UNITY_WEBGL && !UNITY_EDITOR
        private string webGLDataKey = "escapeTheDungeonSaveData1";
    #endif

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }


    public GameData Load()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        GameData loadedData = new GameData();
        loadedData.playerHealth = PlayerPrefs.GetFloat("playerHealth", 20);
        loadedData.playerMaxHealth = PlayerPrefs.GetFloat("playerMaxHealth", 20);
        loadedData.playerEnergy = PlayerPrefs.GetFloat("playerEnergy", 10);
        loadedData.playerMaxEnergy = PlayerPrefs.GetFloat("playerMaxEnergy", 10);
        loadedData.playerStr = PlayerPrefs.GetFloat("playerStr", 10);
        loadedData.playerDex = PlayerPrefs.GetFloat("playerDex", 10);
        loadedData.playerMagic = PlayerPrefs.GetFloat("playerMagic", 10);
        loadedData.playerDamageReduction = PlayerPrefs.GetInt("playerDamageReduction", 0);
        loadedData.playerDodgeChance = PlayerPrefs.GetInt("playerDodgeChance", 0);
        loadedData.playerCritChance = PlayerPrefs.GetInt("playerCritChance", 0);
        loadedData.gold = PlayerPrefs.GetInt("gold", 0);
        loadedData.playerHPPotions = PlayerPrefs.GetInt("playerHPPotions", 0);
        loadedData.currentAct = PlayerPrefs.GetInt("currentAct", 1);
        loadedData.crystals = PlayerPrefs.GetFloat("crystals", 0);
        loadedData.strUpgradeLevel = PlayerPrefs.GetInt("strUpgradeLevel", 0);
        loadedData.dexUpgradeLevel = PlayerPrefs.GetInt("dexUpgradeLevel", 0);
        loadedData.magUpgradeLevel = PlayerPrefs.GetInt("magUpgradeLevel", 0);
        loadedData.drUpgradeLevel = PlayerPrefs.GetInt("drUpgradeLevel", 0);
        loadedData.crystalUpgradeLevel = PlayerPrefs.GetInt("crystalUpgradeLevel", 0);
        loadedData.goldUpgradeLevel = PlayerPrefs.GetInt("goldUpgradeLevel", 0);
        loadedData.potionUpgradeLevel = PlayerPrefs.GetInt("potionUpgradeLevel", 0);
        loadedData.maxHPUpgradeLevel = PlayerPrefs.GetInt("maxHPUpgradeLevel", 0);
        return loadedData;
        #else
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError($"Error occured when trying to load data from file: {fullPath}. Message: {e.Message}");
            }
        }
        return loadedData;
        #endif
    }

    public void Save(GameData data)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        PlayerPrefs.SetFloat("playerHealth", data.playerHealth);
        PlayerPrefs.SetFloat("playerMaxHealth", data.playerMaxHealth);
        PlayerPrefs.SetFloat("playerEnergy", data.playerEnergy);
        PlayerPrefs.SetFloat("playerMaxEnergy", data.playerMaxEnergy);
        PlayerPrefs.SetFloat("playerStr", data.playerStr);
        PlayerPrefs.SetFloat("playerDex", data.playerDex);
        PlayerPrefs.SetFloat("playerMagic", data.playerMagic);
        PlayerPrefs.SetInt("playerDamageReduction", data.playerDamageReduction);
        PlayerPrefs.SetInt("playerDodgeChance", data.playerDodgeChance);
        PlayerPrefs.SetInt("playerCritChance", data.playerCritChance);
        PlayerPrefs.SetInt("gold", data.gold);
        PlayerPrefs.SetInt("playerHPPotions", data.playerHPPotions);
        PlayerPrefs.SetInt("currentAct", data.currentAct);
        PlayerPrefs.SetFloat("crystals", data.crystals);
        PlayerPrefs.SetInt("strUpgradeLevel", data.strUpgradeLevel);
        PlayerPrefs.SetInt("dexUpgradeLevel", data.dexUpgradeLevel);
        PlayerPrefs.SetInt("magUpgradeLevel", data.magUpgradeLevel);
        PlayerPrefs.SetInt("drUpgradeLevel", data.drUpgradeLevel);
        PlayerPrefs.SetInt("crystalUpgradeLevel", data.crystalUpgradeLevel);
        PlayerPrefs.SetInt("goldUpgradeLevel", data.goldUpgradeLevel);
        PlayerPrefs.SetInt("potionUpgradeLevel", data.potionUpgradeLevel);
        PlayerPrefs.SetInt("maxHPUpgradeLevel", data.maxHPUpgradeLevel);
        PlayerPrefs.Save();
                
                
                
        #else
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError($"Error occured when trying to save data to file: {fullPath}. Message: {e.Message}");
        }
        #endif
        
    }
}
