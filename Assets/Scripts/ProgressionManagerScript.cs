using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressionManagerScript : MonoBehaviour
{
    int currentAct, actProgress = 1;

    List<GameObject>  generalEvents;

    [SerializeField] 
    GameObject act1BossEvent, act2BossEvent, act3BossEvent, combatManager, eventManager, ProgressionPanel, gameOverWindow;

    [SerializeField]
    GameObject combat1, combat2, combat3, event1, event2, event3;

    GameObject player, bossEvent;
    [SerializeField]
    TMP_Text actProgressText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FetchActFromPlayerData();
        generalEvents = new List<GameObject>(GameObject.FindGameObjectsWithTag("progression choice"));
        HideChoices();
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
        actProgress = 1;
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
            string catagory = eventCatagories[rand];
            GameObject currentEvent = null;
            Debug.Log("Generated " + catagory + " event for event slot " + (i+1));
            switch (i)
            {
                case 0:
                    if (catagory == "Combat")
                    {
                        currentEvent = combat1;
                    }
                    else if (catagory == "Event")
                    {
                        currentEvent = event1;
                    }
                    break;
                case 1:
                    if (catagory == "Combat")
                    {
                        currentEvent = combat2;
                    }
                    else if (catagory == "Event")
                    {
                        currentEvent = event2;
                    }
                    break;
                case 2:
                    if (catagory == "Combat")
                    {
                        currentEvent = combat3;
                    }
                    else if (catagory == "Event")
                    {
                        currentEvent = event3;
                    }
                    break;
                default:
                    Debug.Log("Invalid event index generated: " + i);
                    currentEvent = combat1;
                    break;
            }
            currentEvent.SetActive(true);
        }
        
    }
    public void GenerateBossEvent()
    {
        Debug.Log("Generating Boss Event | Current Act: " + currentAct);
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
        bossEvent.SetActive(true);
        Debug.Log("Generated " + bossEvent.name + " for Act " + currentAct);
        //TODO: Add boss event logic and stuff.
    }

    public void BossChoice()
    {
        combatManager.GetComponent<CombatManagerScript>().StartBossCombat(currentAct*-1);
        combatManager.GetComponent<CombatManagerScript>().EnableCombatUI();
        HideChoices();
    }

    public void HideChoices()
    {
        foreach (GameObject obj in generalEvents)
        {
            obj.SetActive(false);
        }
        act1BossEvent.SetActive(false);
        act2BossEvent.SetActive(false);
        act3BossEvent.SetActive(false);

    }

    public void IncrementActProgression()
    {
        actProgress++;
    }

    public void IncrementAct()
    {
        currentAct++;
        PushActToPlayerData();
        actProgress = 1;
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
