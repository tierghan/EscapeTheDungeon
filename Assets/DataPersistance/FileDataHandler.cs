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
        loadedData.playerHealth = PlayerPrefs.GetFloat(webGLDataKey + "Health");
        loadedData.playerMaxHealth = PlayerPrefs.GetFloat(webGLDataKey + "MaxHealth");
        loadedData.playerEnergy = PlayerPrefs.GetFloat(webGLDataKey + "Energy");   
        loadedData.playerMaxEnergy = PlayerPrefs.GetFloat(webGLDataKey + "MaxEnergy");
        loadedData.playerStr = PlayerPrefs.GetFloat(webGLDataKey + "Str");
        loadedData.playerDex = PlayerPrefs.GetFloat(webGLDataKey + "Dex");
        loadedData.playerMagic = PlayerPrefs.GetFloat(webGLDataKey + "Magic");
        loadedData.playerDamageReduction = PlayerPrefs.GetInt(webGLDataKey + "DamageReduction");
        loadedData.playerDodgeChance = PlayerPrefs.GetInt(webGLDataKey + "DodgeChance");
        loadedData.playerCritChance = PlayerPrefs.GetInt(webGLDataKey + "CritChance");
        loadedData.gold = PlayerPrefs.GetInt(webGLDataKey + "Gold");
        loadedData.playerHPPotions = PlayerPrefs.GetInt(webGLDataKey + "HPPotions");
        loadedData.currentAct = PlayerPrefs.GetInt(webGLDataKey + "CurrentAct");
        loadedData.crystals = PlayerPrefs.GetFloat(webGLDataKey + "Crystals");
        loadedData.strUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "StrUpgradeLevel");
        loadedData.dexUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "DexUpgradeLevel");
        loadedData.magUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "MagUpgradeLevel");
        loadedData.drUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "DrUpgradeLevel");
        loadedData.crystalUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "CrystalUpgradeLevel");
        loadedData.goldUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "GoldUpgradeLevel");
        loadedData.potionUpgradeLevel = PlayerPrefs.GetInt(webGLDataKey + "PotionUpgradeLevel");
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
        PlayerPrefs.SetFloat(webGLDataKey + "Health", data.playerHealth);
        PlayerPrefs.SetFloat(webGLDataKey + "MaxHealth", data.playerMaxHealth);
        PlayerPrefs.SetFloat(webGLDataKey + "Energy", data.playerEnergy);
        PlayerPrefs.SetFloat(webGLDataKey + "MaxEnergy", data.playerMaxEnergy);
        PlayerPrefs.SetFloat(webGLDataKey + "Str", data.playerStr);
        PlayerPrefs.SetFloat(webGLDataKey + "Dex", data.playerDex);
        PlayerPrefs.SetFloat(webGLDataKey + "Magic", data.playerMagic);
        PlayerPrefs.SetInt(webGLDataKey + "DamageReduction", data.playerDamageReduction);
        PlayerPrefs.SetInt(webGLDataKey + "DodgeChance", data.playerDodgeChance);
        PlayerPrefs.SetInt(webGLDataKey + "CritChance", data.playerCritChance);
        PlayerPrefs.SetInt(webGLDataKey + "Gold", data.gold);
        PlayerPrefs.SetInt(webGLDataKey + "HPPotions", data.playerHPPotions);
        PlayerPrefs.SetInt(webGLDataKey + "CurrentAct", data.currentAct);
        PlayerPrefs.SetFloat(webGLDataKey + "Crystals", data.crystals);
        PlayerPrefs.SetInt(webGLDataKey + "StrUpgradeLevel", data.strUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "DexUpgradeLevel", data.dexUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "MagUpgradeLevel", data.magUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "DrUpgradeLevel", data.drUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "CrystalUpgradeLevel", data.crystalUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "GoldUpgradeLevel", data.goldUpgradeLevel);
        PlayerPrefs.SetInt(webGLDataKey + "PotionUpgradeLevel", data.potionUpgradeLevel);
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
