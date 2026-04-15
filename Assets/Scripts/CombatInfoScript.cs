using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInfoScript : MonoBehaviour
{
    [SerializeField]
    GameObject combatPanel;

    [SerializeField]
    AudioManagerScript audioManager;

    bool combatInfoClosed = false;

    public void EnableCombatInfo()
    {
        combatPanel.SetActive(true);
        audioManager.PlaySFXClick();
    }

    public void DisableCombatInfo()
    {
        combatPanel.SetActive(false);
        if (combatInfoClosed == true) audioManager.PlaySFXClick();
    }

    public void ToggleCombatInfo()
    {
        if (combatPanel.activeSelf)
        {
            DisableCombatInfo();
        }
        else
        {
            EnableCombatInfo();
        }
    }

    private void Start() 
    {
        DisableCombatInfo();
        combatInfoClosed = true;    
    }
}
