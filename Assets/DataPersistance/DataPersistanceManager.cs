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
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load save data from file.
        this.gameData = dataHandler.Load();
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
