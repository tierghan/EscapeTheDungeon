using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestManagerScript : MonoBehaviour
{
    [SerializeField] GameObject player, progressionManager;
    public void Rest()
    {
        player.GetComponent<PlayerDataHandler>().HealPercentage(30);
    }
    
    public void ShowRest()
    {
        gameObject.SetActive(true);
        Rest();
    }

    public void HideRest()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        HideRest();
    }

    public void LeaveRest()
    {
        HideRest();
        progressionManager.GetComponent<ProgressionManagerScript>().NewExplore();
    }
}
