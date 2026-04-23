using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressionManagerScript : MonoBehaviour
{
    int currentAct, actProgress = 1;

    List<GameObject>  generalEvents;

    [SerializeField] 
    GameObject act1BossEvent, act2BossEvent, act3BossEvent, combatManager, eventManager, ProgressionPanel, gameOverWindow, shopManager, restManager, detourManager, trainingManager;

    [SerializeField]
    GameObject combat1, combat2, combat3, event1, event2, event3, train1, train2, train3, rest1, rest2, rest3, shop1, shop2, shop3, detour1, detour2, detour3, victoryWindow;

    GameObject player, bossEvent;
    [SerializeField]
    TMP_Text actProgressText, victoryText;

    [SerializeField]
    AudioManagerScript audioManager;

    void Start()
    {
        victoryWindow.SetActive(false);
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
        audioManager.PlayBackgroundMusic();
        currentAct = 1;
        actProgress = 1;
        combatManager.GetComponent<CombatManagerScript>().DisableCombatUI();
        eventManager.GetComponent<EventCardSystemScript>().HideEventWindow();
        gameOverWindow.SetActive(false);
        NewExplore();
    }

    public void RunVictory()
    {
        player.GetComponent<PlayerDataHandler>().AddStat(13, 100);
        victoryText.text = "You have defeated the final boss of Act 3 and escaped the dungeon!\nCongratulations on your victory!\n\nHere is some extra crystals for your efforts.\n(+100 Crystals)\n\nYou currently have " + player.GetComponent<PlayerDataHandler>().GetStats()[13] + " crystals.";
        victoryWindow.SetActive(true);
    }

    public void HideVictory()
    {
        victoryWindow.SetActive(false);
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
        if (actProgress<1)
        {
            actProgress = 1;
        }
    }

    public void GenerateGeneralEvents()
    {
        Debug.Log("Generating General Events");
        List<string> eventCatagories = new List<string>();
        
        //Manually comment out entries to disable player encountering them for testing or what not.
        eventCatagories.Add("Combat");
        eventCatagories.Add("Event");
        eventCatagories.Add("Shop");
        eventCatagories.Add("Rest");
        eventCatagories.Add("Detour");
        eventCatagories.Add("Training");
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
                    else if (catagory == "Shop")
                    {
                        currentEvent = shop1;
                    }
                     else if (catagory == "Rest")
                    {
                        currentEvent = rest1;
                    }
                     else if (catagory == "Detour")
                    {
                        currentEvent = detour1;
                    }
                     else if (catagory == "Training")
                    {
                        currentEvent = train1;
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
                    else if (catagory == "Shop")
                    {
                        currentEvent = shop2;
                    }
                     else if (catagory == "Rest")
                    {
                        currentEvent = rest2;
                    }
                     else if (catagory == "Detour")
                    {
                        currentEvent = detour2;
                    }
                     else if (catagory == "Training")
                    {
                        currentEvent = train2;
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
                    else if (catagory == "Shop")
                    {
                        currentEvent = shop3;
                    }
                     else if (catagory == "Rest")
                    {
                        currentEvent = rest3;
                    }
                     else if (catagory == "Detour")
                    {
                        currentEvent = detour3;
                    }
                     else if (catagory == "Training")
                    {
                        currentEvent = train3;
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
        currentAct = (int)player.GetComponent<PlayerDataHandler>().GetStats()[12];
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
        audioManager.PlaySFXClick();
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

    public void DecrementActProgression()
    {
        actProgress--;
        if (actProgress < 1)
        {
            actProgress = 1;
        }
    }

    public void IncrementAct()
    {
        currentAct++;
        PushActToPlayerData();
        actProgress = 1;
    }

    public void ExploreChoiceCombat()
    {
        audioManager.PlaySFXClick();
        combatManager.GetComponent<CombatManagerScript>().StartCombat();
        combatManager.GetComponent<CombatManagerScript>().EnableCombatUI();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceEvent()
    {
        audioManager.PlaySFXClick();
        eventManager.GetComponent<EventCardSystemScript>().ShowEventWindow();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceShop()
    {
        audioManager.PlaySFXClick();
        shopManager.GetComponent<ShopManagerScript>().ShowShop();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceRest()
    {
        audioManager.PlaySFXClick();
        restManager.GetComponent<RestManagerScript>().ShowRest();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceDetour()
    {
        audioManager.PlaySFXClick();
        detourManager.GetComponent<DetourManagerScript>().ShowDetour();
        HideChoices();
        IncrementActProgression();
    }

    public void ExploreChoiceTraining()
    {
        audioManager.PlaySFXClick();
        trainingManager.GetComponent<TrainingManagerScript>().ShowTraining();
        HideChoices();
        IncrementActProgression();
    }

    private void Update() 
    {
        actProgressText.text = "Act Progress: " + actProgress + "/15";
    }
}
