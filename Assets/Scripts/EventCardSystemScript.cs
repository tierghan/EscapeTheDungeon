using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventCardSystemScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text option1Text, option2Text, option3Text, option4Text, titleText, descriptionText;
    [SerializeField]
    GameObject eventPanel,continueButton;
    [SerializeField]
    AudioManagerScript audioManager;
    GameObject optionButton1, optionButton2, optionButton3, optionButton4, player, titleObject, descriptionObject;
    int randomStat;
    List<string> stats = new List<string>(){"STR", "DEX", "MAG", "Max HP", "Max Energy", "Damage Reduction"};
    


    int eventCount = 4;
    /*
        0 | Mystery Potion - A potion that has a random effect on the player. Could be good or bad.
        1 | Adventurer Donation - An adventurer that offers to heal the player for a price. Player can also choose to rob him or ignore him.
        2 | Mimic Event - A chest that may or may not be a mimic. Player can choose to open it or destroy it.
        3 | Abandoned Camp - A small camp with some supplies. Player can choose to search it for useful items, rest at the camp to heal, or ignore it and continue on their way.
    */
    int eventSelector;

    bool isMimic = false;

    void Start()
    {
        titleObject = GameObject.Find("EventTitleText");
        descriptionObject = GameObject.Find("EventDescriptionText");

        optionButton1 = GameObject.Find("EventButtonOption1");
        optionButton2 = GameObject.Find("EventButtonOption2");
        optionButton3 = GameObject.Find("EventButtonOption3");
        optionButton4 = GameObject.Find("EventButtonOption4");

        player = GameObject.FindGameObjectWithTag("Player");
        List<string> stats = new List<string>(){"STR", "DEX", "MAG", "Max HP", "Max Energy", "Damage Reduction"};

        HideEventWindow();
    }

    public void ShowEventWindow()
    {
        eventPanel.SetActive(true);
        PickRandomEvent();
    }
    public void HideEventWindow()
    {
        eventPanel.SetActive(false);
    }

    public void toggleEventWindow()
    {
        if (eventPanel.activeInHierarchy)
        {
            HideEventWindow();
        }
        else
        {
            ShowEventWindow();
        }
    }

    public void PickRandomEvent()
    {
        continueButton.SetActive(false);
        eventSelector = Random.Range(0, eventCount);
        Debug.Log("Event Selected: " + eventSelector);
        switch (eventSelector)
        {
            case 0:
                MysteryPotionEvent();
                break;
            case 1:
                AdventurerDonationEvent();
                break;
            case 2:
                MimicEvent();
                break;
            case 3:
                AbandonedCampEvent();
                break;
            default:
                Debug.Log("No event selected." + eventSelector);
                break;
        }
    }

    void hideOptions()
    {
        optionButton1.SetActive(false);
        optionButton2.SetActive(false);
        optionButton3.SetActive(false);
        optionButton4.SetActive(false);
        continueButton.SetActive(true);

    }

    void MysteryPotionEvent()
    {
        titleText.text = "Mystery Potion";
        descriptionText.text = "You find a mysterious potion on the ground. It shines a stange purple color and small, glittery particles swirl around inside the container.";
        option1Text.text = "Drink the potion.";
        option2Text.text = "Ignore the potion and continue on your way.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(false);
        optionButton4.SetActive(false);
    }

    void AdventurerDonationEvent()
    {
        titleText.text = "Cheap Charity";
        descriptionText.text = "You come across an adventurer sitting against a nearby stone wall. He waves you over, offering to heal you for a donation.";
        option1Text.text = "Give a small donation (20 Gold)";
        option2Text.text = "Give a large donation (50 Gold)";
        option3Text.text = "Rob the adventurer instead.";
        option4Text.text = "Ignore the adventurer and continue on your way.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(true);
        optionButton4.SetActive(true);
    }

    void MimicEvent()
    {
        titleText.text = "Treasue Chest(?)";
        descriptionText.text = "As you walk down the stone hallways, you see an alcove with a tresure chest inside. It could be the torchlight, but you feel like the container moved slightly as looked it over.";
        option1Text.text = "Open the chest.";
        option2Text.text = "Destroy the chest.";
        if (Random.Range(0, 2) == 0)
        {
            isMimic = true;
        }
        else
        {
            isMimic = false;
        }
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton4.SetActive(false);
        optionButton3.SetActive(false);
    }

    void AbandonedCampEvent()
    {
        titleText.text = "Abandoned Camp";
        descriptionText.text = "You find the remains of a small camp, a dead fire in the center with some abandoned supplies and sleeping equipment along side it.";
        option1Text.text = "Search the camp for useful supplies.";
        option2Text.text = "Rest at the camp.";
        option3Text.text = "Ignore the camp and continue on your way.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(true);
        optionButton4.SetActive(false);
    }
    public void Option1Selected()
    {
        switch (eventSelector)
        {
            // Event 0: Mystery Potion - Drink the potion
            case 0:
                player.GetComponent<PlayerDataHandler>().FlatDamage(5);
                int randomStatType = Random.Range(0, 5);
                int randomStat = 0;
                string statName = "";
                if(randomStatType <= 3)
                {
                    // Adds 1 to STR, DEX, or MAG.
                    randomStat = Random.Range(0,4);
                    player.GetComponent<PlayerDataHandler>().AddStat(randomStat, 1f);
                    switch (randomStat)
                    {
                        case 0:
                            statName = "STR";
                            break;
                        case 1:
                            statName = "DEX";
                            break;
                        case 2:
                            statName = "MAG";
                            break;
                        default:
                            statName = "Unknown";
                            break;
                    }
                }
                else
                {
                    // Adds 1 to max DR, DC, CC.
                    randomStat = 7;
                    player.GetComponent<PlayerDataHandler>().AddStat(randomStat, 1f );
                    switch (randomStat)
                    {
                        case 7:
                            statName = "Damage Reduction";
                            break;
                        default:
                            statName = "Unknown";
                            break;
                    }
                }
                descriptionText.text = "You drink the stange potion, feeling some pain in your gut. Once it subsides, you feel a little better than before.\n\n-5 HP\n+1 " + statName;
                hideOptions();
                break;
            
            // Event 1: Adventurer Donation - Give a small donation (20 Gold)
            case 1:
                if (player.GetComponent<PlayerDataHandler>().GetGold() >= 20)
                {
                    descriptionText.text = "You give the adventurer a small donation of 20 gold. He thanks you and heals your wounds.\n\n-20 Gold\n+20 HP";
                    player.GetComponent<PlayerDataHandler>().AddStat(10, -20);
                    player.GetComponent<PlayerDataHandler>().AddStat(3, 20);
                    hideOptions();
                }
                break;
            
            // Event 2: Mimic Event - Open the chest.
            case 2:
                if (isMimic)
                {
                    player.GetComponent<PlayerDataHandler>().PercentageDamage(25);
                    player.GetComponent<PlayerDataHandler>().AddStat(10, 30);
                    descriptionText.text = "You get closer, opening the chest only to find rows of teeth lurhcing towards your face. Suprised, you take decent damage before you can make an escape from the sudden attack. \n\n-25% HP";
                }
                else
                {
                    // TODO: Item system should give the player a random item from this outcome.
                    randomStat = Random.Range(0, stats.Count);
                    descriptionText.text = "You open the chest, finding inside a magic item that increases your " + stats[randomStat] + " by 5 points.\n\n+5 " + stats[randomStat];
                    if (randomStat == 0)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(0, 5f);
                    }
                    else if (randomStat == 1)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(1, 5f);
                    }
                    else if (randomStat == 2)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(2, 5f);
                    }
                    else if (randomStat == 3)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(4, 5f);
                    }
                    else if (randomStat == 4)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(6, 5f);
                    }
                    else if (randomStat == 5)
                    {
                        player.GetComponent<PlayerDataHandler>().AddStat(7, 5f);
                    }    
                }
                hideOptions();
                break;
            
            // Event 3: Abandoned Camp - Search the camp for useful supplies.
            case 3:
                descriptionText.text = "You look around in the supplies, finding a potion and a coin pouch.\n\n+1 Health Potion\n+30 Gold";
                player.GetComponent<PlayerDataHandler>().AddStat(11, 1);
                player.GetComponent<PlayerDataHandler>().AddStat(10, 30);
                hideOptions();
                break;

            default:
                Debug.Log("How did you select this?? Current event: " + eventSelector);
                break;
        }
        audioManager.PlaySFXClick();
    }
    public void Option2Selected()
    {
        switch (eventSelector)
        {
            // Event 0: Mystery Potion - Ignore the potion
            case 0:
                descriptionText.text = "You decide it's best not to mess with the strange potion and continue on your way.";
                hideOptions();
                break;
            
            // Event 1: Adventurer Donation - Give a large donation (50 Gold)
            case 1:
                if (player.GetComponent<PlayerDataHandler>().GetGold() >= 50)
                {
                    descriptionText.text = "You give the adventurer a large donation of 50 gold. He thanks you and heals your wounds.\n\n-50 Gold\n+30 HP";
                    player.GetComponent<PlayerDataHandler>().AddStat(10, -50);
                    player.GetComponent<PlayerDataHandler>().AddStat(3, 30);
                    hideOptions();
                }
                break;

            // Event 2: Mimic Event - Destroy the chest.
            case 2:
                if (isMimic)
                {
                    player.GetComponent<PlayerDataHandler>().AddStat(10, 50);
                    descriptionText.text = "You decide to destroy the chest, attacking it wiht your weapon. The wooden container bleeds red as your suprise attack against the mimic works wonders. Inside the creature was a coin pouch from a previous victim with 50 gold inside, nice.\n\n +50 Gold";
                }
                else
                {
                    descriptionText.text = "You shatter the chest, destroying the wooden container along side anything that was inside. Oops.";
                }
                hideOptions();
                break;
            
            // Event 3: Abandoned Camp - Rest at the camp.
            case 3:
                player.GetComponent<PlayerDataHandler>().HealPercentage(50);
                descriptionText.text = "You decide to rest at the camp, taking a moment to recover your strength. You feel much better after a few moments of rest.\n\n+50% HP";
                hideOptions();
                break;

            default:
                Debug.Log("How did you select this?? Current event: " + eventSelector);
                break;
        }
        audioManager.PlaySFXClick();
    }

    public void Option3Selected()
    {
        switch (eventSelector)
        {
            // Event 1: Adventurer Donation - Rob the adventurer instead.
            case 1:
                descriptionText.text = "You decide to rob the adventurer. You take all his gold and leave him with nothing.\n\n+20 Gold\n";
                player.GetComponent<PlayerDataHandler>().AddStat(10, 20);
                hideOptions();
                break;
            
            // Event 3: Abandoned Camp - Ignore the camp and continue on your way.
            case 3:
                descriptionText.text = "You decide to ignore the camp and continue on your way.";
                hideOptions();
                break;
                
            default:
                Debug.Log("How did you select this?? Current event: " + eventSelector);
                break;
        }
        audioManager.PlaySFXClick();

    }

    public void Option4Selected()
    {
        switch (eventSelector)
        {

            // Adventurer Donation - Ignore the adventurer and continue on your way.
            case 1:
                descriptionText.text = "You decide to ignore the adventurer and continue on your way.";
                hideOptions();
                break;
                
            default:
                Debug.Log("How did you select this?? Current event: " + eventSelector);
                break;
        }
        audioManager.PlaySFXClick();
    }

}
