using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    public static DataPersistanceManager instance { get; private set; }
    private FileDataHandler dataHandler;
    private List<IDataPersistance> dataPersistanceObjects;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found another DPManager in scene.");
        }
        instance = this;
    }

    private void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            string fullPath = Path.Combine("idbfs", Application.productName);
            if (!File.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
                this.dataHandler = new FileDataHandler(Path.Combine(Application.persistentDataPath, "idbfs"), fileName);
                this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        #else
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        #endif
        LoadGame();
    }


    public void NewGame()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            string webGLDataKey = "escapeTheDungeonSaveData1";

            PlayerPrefs.SetFloat(webGLDataKey + "Health", 20);
            PlayerPrefs.SetFloat(webGLDataKey + "MaxHealth", 20);
            PlayerPrefs.SetFloat(webGLDataKey + "Energy", 10);
            PlayerPrefs.SetFloat(webGLDataKey + "MaxEnergy", 10);
            PlayerPrefs.SetFloat(webGLDataKey + "Str", 10);
            PlayerPrefs.SetFloat(webGLDataKey + "Dex", 10);
            PlayerPrefs.SetFloat(webGLDataKey + "Magic", 10);
            PlayerPrefs.SetInt(webGLDataKey + "DamageReduction", 0);
            PlayerPrefs.SetInt(webGLDataKey + "DodgeChance", 0);
            PlayerPrefs.SetInt(webGLDataKey + "CritChance", 0);
            PlayerPrefs.SetInt(webGLDataKey + "Gold", 0);
            PlayerPrefs.SetInt(webGLDataKey + "HPPotions", 0);
            PlayerPrefs.SetInt(webGLDataKey + "CurrentAct", 1);
            PlayerPrefs.SetFloat(webGLDataKey + "Crystals", 0);
            PlayerPrefs.SetInt(webGLDataKey + "StrUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "DexUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "MagUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "DrUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "CrystalUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "GoldUpgradeLevel", 0);
            PlayerPrefs.SetInt(webGLDataKey + "PotionUpgradeLevel", 0);
            PlayerPrefs.Save();

        #else
        this.gameData = new GameData();
        #endif
    }

    public void LoadGame()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            GameData loadedData = new GameData();
            string webGLDataKey = "escapeTheDungeonSaveData1";
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
            this.gameData = loadedData;
            foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
            {
                dataPersistanceObj.LoadData(gameData);
                Debug.Log("Loaded data to: " + dataPersistanceObj.ToString());
            }
        #else
        // Load save data from file.
        this.gameData = dataHandler.Load();
        Debug.Log("Loaded " + gameData.crystals + " crystals.");
        // No data = New game.
        if (this.gameData == null)
        {
            Debug.Log("No data found. Initializing data to defaults.");
            NewGame();
        }
        // Push loaded data to other scripts.
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
            Debug.Log("Loaded data to: " + dataPersistanceObj.ToString());
        }
        #endif
    }

    public void SaveGame()
    {
        // pass data to other scripts so they can update.
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }
        // Save game data to file.
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
