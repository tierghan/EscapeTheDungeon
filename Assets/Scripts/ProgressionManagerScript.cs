using System.Collections;
using System.Collections.Generic;
using Unity.UI;
using UnityEngine;
using TMPro;

public class ProgressionManagerScript : MonoBehaviour
{
    int currentAct, actProgress;

    List<GameObject>  generalEvents;

    [SerializeField] 
    GameObject act1BossEvent, act2BossEvent, act3BossEvent, combatManager, eventManager, ProgressionPanel, gameOverWindow;
    GameObject player, bossEvent;
    [SerializeField]
    TMP_Text actProgressText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FetchActFromPlayerData();
        generalEvents = new List<GameObject>(GameObject.FindGameObjectsWithTag("progression choice"));
        foreach (GameObject obj in generalEvents)
        {
            obj.transform.Translate(new Vector3(50,50,0));
        }
        NewExplore();
    }

    public void PushActToPlayerData()
    {
        player.GetComponent<PlayerDataHandler>().SetCurrentAct(currentAct);
    }

    public void FetchActFromPlayerData()
    {
        currentAct = (int)player.GetComponent<PlayerDataHandler>().GetStats()[12];
    }

    public void NewRun()
    {
        currentAct = 1;
        actProgress = 0;
        combatManager.GetComponent<CombatManagerScript>().DisableCombatUI();
        eventManager.GetComponent<EventCardSystemScript>().HideEventWindow();
        gameOverWindow.SetActive(false);
        NewExplore();
    }

    public void NewExplore()
    {
        HideChoices();
        Debug.Log("Current Act Progress: " + actProgress); 
        if(actProgress <= 15)
        {
            GenerateGeneralEvents();
        }
        else if(actProgress > 15)
        {
            GenerateBossEvent();
        }
    }

    public void ModifyActProgression(int amount)
    {
        actProgress += amount;
    }

    public void GenerateGeneralEvents()
    {
        Debug.Log("Generating General Events");
        List<string> eventCatagories = new List<string>();
        
        //Manually comment out entries to disable player encountering them for testing or what not.
        eventCatagories.Add("Combat");
        eventCatagories.Add("Event");
        // eventCatagories.Add("Shop");
        // eventCatagories.Add("Rest");
        // eventCatagories.Add("Detour");
        // eventCatagories.Add("Training");
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, eventCatagories.Count);
            GameObject currentEvent = GameObject.Find("ExploreChoice"+eventCatagories[rand]+i);
            Debug.Log("Generated " + currentEvent.name + " for choice " + i + " with event catagory " + eventCatagories[rand]);
            switch (i)
            {
                case 0:
                    currentEvent.transform.position = new Vector3(-2.5f, 1.5f, 0);
                    Debug.Log("Translated " + currentEvent.name + " to " + currentEvent.transform.position);
                    break;
                case 1:
                    currentEvent.transform.position = new Vector3(0, 1.5f, 0);
                    Debug.Log("Translated " + currentEvent.name + " to " + currentEvent.transform.position);
                    break;
                case 2:
                    currentEvent.transform.position = new Vector3(2.5f, 1.5f, 0);
                    Debug.Log("Translated " + currentEvent.name + " to " + currentEvent.transform.position);
                    break;
            }
        }
        
    }
    public void GenerateBossEvent()
    {
        switch (currentAct)
        {
            case 1:
                bossEvent = act1BossEvent;
                break;
            case 2:
                bossEvent = act2BossEvent;
                break;
            case 3:
                bossEvent = act3BossEvent;
                break;
        }
        bossEvent.transform.position = new Vector3(0, 0, 0);
        Debug.Log("Generated " + bossEvent.name + " for Act " + currentAct);
        //TODO: Add boss event logic and stuff.
    }

    public void HideChoices()
    {
        foreach (GameObject obj in generalEvents)
        {
            obj.transform.position = new Vector3(50, 50, 0);
        }
        act1BossEvent.transform.position = new Vector3(50, 50, 0);
        act2BossEvent.transform.position = new Vector3(50, 50, 0);
        act3BossEvent.transform.position = new Vector3(50, 50, 0);

    }

    public void IncrementActProgression()
    {
        actProgress++;
    }

    public void ExploreChoiceCombat()
    {
        combatManager.GetComponent<CombatManagerScript>().StartCombat();
        combatManager.GetComponent<CombatManagerScript>().EnableCombatUI();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceEvent()
    {
        eventManager.GetComponent<EventCardSystemScript>().ShowEventWindow();
        HideChoices();
        IncrementActProgression();
    }

    private void Update() 
    {
        actProgressText.text = "Act Progress: " + actProgress + "/15";
    }
}
