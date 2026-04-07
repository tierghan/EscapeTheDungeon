using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressionManagerScript : MonoBehaviour
{
    int currentAct, actProgress;

    [SerializeField]
    List<GameObject>  generalEvents;

    [SerializeField] 
    GameObject act1BossEvent, act2BossEvent, act3BossEvent, combatManager, eventManager;
    GameObject player, bossEvent;
    [SerializeField]
    TMP_Text actProgressText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FetchActFromPlayerData();
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
    }

    public void NewExplore()
    {
        if(actProgress <= 15)
        {
            GenerateGeneralEvents();
        }
        else if(currentAct >= 1 && actProgress >= 15)
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
        int rand = Random.Range(0, generalEvents.Count);
        for (int i = 0; i<=3; i++)
        {
            GameObject currentEvent = generalEvents[rand];
            Instantiate(currentEvent);
            Debug.Log("Generated Event: " + currentEvent.name);
            if (i == 1)
            {
                currentEvent.transform.position = new Vector3(-220, 0, 0);
            }
            else if (i == 2)
            {
                currentEvent.transform.position = new Vector3(0, 0, 0);
            }
            else if (i == 3)
            {
                currentEvent.transform.position = new Vector3(220, 0, 0);
            }
            GameObject currentEventButton = currentEvent.transform.Find("ChoiceButton").gameObject;
            Debug.Log("Current Event Button: " + currentEventButton.name);
            switch (currentEvent.name)
            {
                case "ExploreChoiceCombat":
                    AddButtonCall(currentEventButton, 0);
                    break;
                case "ExploreChoiceEvent":
                    AddButtonCall(currentEventButton, 1);
                    break;
            }
        }
    }

    public void AddButtonCall(GameObject buttonObject, int choice)
    {
        /*
        Choice Values:
            -1 = Act 1 Boss
            0 = Combat
            1 = Event

        */
        
        switch (choice)
        {
            case 0:
                buttonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { this.ExploreChoiceCombat(); });
                break;
            case 1:
                buttonObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { this.ExploreChoiceEvent(); });
                break;
        }
        Debug.Log("Added Button Call to " + buttonObject.name + " for choice " + choice);
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
        int rand = Random.Range(0, bossEvent.transform.childCount);
        Instantiate(bossEvent.transform.GetChild(rand).gameObject);
        bossEvent.transform.position = new Vector3(0, 0, 0);
    }

    public void HideChoices()
    {
        List<GameObject> choices = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("progression choice"))
        {
            choices.Add(obj);
        }
        foreach (GameObject obj in choices)
        {
            Destroy(obj);
        }
    }

    public void IncrementActProgression()
    {
        actProgress++;
    }

    public void ExploreChoiceCombat()
    {
        combatManager.GetComponent<CombatManagerScript>().StartCombat();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceEvent()
    {
        eventManager.GetComponent<EventCardSystemScript>().ShowEventWindow();
        HideChoices();
        IncrementActProgression();
    }

    private void FixedUpdate() 
    {
        actProgressText.text = "Act Progress: " + actProgress + "/15";
    }
}
