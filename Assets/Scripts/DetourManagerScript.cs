using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetourManagerScript : MonoBehaviour
{
    [SerializeField] GameObject progressionManager;
    public void ShowDetour()
    {
        gameObject.SetActive(true);
    }
    public void HideDetour()
    {
        gameObject.SetActive(false);
    }
    public void LeaveDetour()
    {
        HideDetour();
        progressionManager.GetComponent<ProgressionManagerScript>().DecrementActProgression();
        progressionManager.GetComponent<ProgressionManagerScript>().NewExplore();
    }

    void Start()
    {
        HideDetour();
    }

    
}
