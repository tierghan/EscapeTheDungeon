using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManagerScript : MonoBehaviour
{
    [SerializeField] GameObject player, progressionManager;
    public void ShowTraining()
    {
        gameObject.SetActive(true);
    }

    public void HideTraining()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        HideTraining();
    }

    public void LeaveTraining()
    {
        HideTraining();
        progressionManager.GetComponent<ProgressionManagerScript>().NewExplore();
    }

    public void TrainStatIncrease(int statIndex)
    {
        player.GetComponent<PlayerDataHandler>().AddStat(statIndex, Random.Range(1, 4));
        LeaveTraining();
    }
}
