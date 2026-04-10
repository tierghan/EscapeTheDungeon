using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInfoScript : MonoBehaviour
{
    [SerializeField]
    GameObject combatPanel;

    public void EnableCombatInfo()
    {
        combatPanel.SetActive(true);
    }

    public void DisableCombatInfo()
    {
        combatPanel.SetActive(false);
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
    }
}
