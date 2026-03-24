using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IDataPersistance
{
    private int saveVar1;

    public void Update()
    {
        saveVar1++;
    }

    public void LoadData(GameData data)
    {
        this.saveVar1 = data.saveVar1;
    }

    public void SaveData(ref GameData data)
    {
        data.saveVar1 = this.saveVar1;
    }
}
