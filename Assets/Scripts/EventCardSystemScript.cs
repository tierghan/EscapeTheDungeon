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
    [SerializeField]
    GameObject optionButton1, optionButton2, optionButton3, optionButton4, player, titleObject, descriptionObject;
    [SerializeField]
    PlayerDataHandler playerScript;
    [SerializeField]
    ProgressionManagerScript progressionScript;
    int randomStat;
    List<string> stats = new List<string>(){"STR", "DEX", "MAG", "Max HP", "Max Energy", "Damage Reduction"};
    


    int eventCount = 9;
    /*
        0 | Mystery Potion - A potion that has a random effect on the player. Could be good or bad.
        1 | Adventurer Donation - An adventurer that offers to heal the player for a price. Player can also choose to rob him or ignore him.
        2 | Mimic Event - A chest that may or may not be a mimic. Player can choose to open it or destroy it.
        3 | Abandoned Camp - A small camp with some supplies. Player can choose to search it for useful items, rest at the camp to heal, or ignore it and continue on their way.
        4 | Slot Machine - Player finds a slot machine where they can gamble coins for crystals.
        5 | Split Paths - Player finds a split path, where one way has gold and the other is a large detour [TODO]
        6 | Mysterious Wizard - Player encounters a wizard who offer to make them stronger. [TODO]
        7 | Potion Seller - A merchant the player encounters who offers to sell them cheaper potions than what the shop can provide. [TODO]
        8 | Glowing Rock - a rock that can be made into a potion that increase hp and energy or sold for gold. [TODO]
    */
    int eventSelector;

    bool isMimic = false;

    void Start()
    {
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
            case 4:
                GoldGamblerEvent();
                break;
            case 5:
                SplitPathsEvent();
                break;
            case 6:
                WizardEvent();
                break;
            case 7:
                PotionSellerEvent();
                break;
            case 8:
                GlowingRockEvent();
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

    void GoldGamblerEvent()
    {
        titleText.text = "A Wild Slot Machine Appears!";
        descriptionText.text = "You round a corner and find a strange slot machine accepting gold coins. A nearby sign suggests that you can gamble your money here for rewards.";
        option1Text.text = "Insert 20 gold.";
        option2Text.text = "Insert half your gold.";
        option3Text.text = "Insert all your gold.";
        option4Text.text = "Gambling is bad, better to leave it alone.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(true);
        optionButton4.SetActive(true);
    }

    void SplitPathsEvent()
    {
        titleText.text = "A fork in the road";
        descriptionText.text = "Ahead is a split in the road. You can choose to go down the left path or the right path, one is likly to take you further away from your goal and the other may lead you to riches, or so your intuition says.";
        option1Text.text = "Go left.";
        option2Text.text = "Go right.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(false);
        optionButton4.SetActive(false);
    }

    void WizardEvent()
    {
        titleText.text = "A stange Wizard appears";
        descriptionText.text = "You bump into a old wizard who lives in the dungeon. He greets you, and offers to lend you some of his magic to increase your power.";
        option1Text.text = "Ask for Max HP";
        option2Text.text = "Ask for Max Energy";
        option3Text.text = "Ask for DR";
        option4Text.text = "Refuse the offer, he is suspicious.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(true);
        optionButton4.SetActive(true);
    }

    void PotionSellerEvent()
    {
        titleText.text = "Potion Seller";
        descriptionText.text = "Someone calls out to you from behind, a shady looking man in a dark cloak. He waves you closer, explaining that he is a potion seller who sells potions cheaper than the dungeon shops will sell them for.";
        option1Text.text = "Buy 2 Potions (30 Gold)";
        option2Text.text = "Buy 6 Potions (90 Gold)";
        option3Text.text = "Give me your strongest potions.";
        option4Text.text = "Refuse the offer, he is suspicious.";
        optionButton1.SetActive(true);
        optionButton2.SetActive(true);
        optionButton3.SetActive(true);
        optionButton4.SetActive(true);
    }

    void GlowingRockEvent()
    {
        titleText.text = "Glowing Rock";
        descriptionText.text = "You find a stange glowing rock on the ground. Looking at it, you could probably sell it for a decent amount of cash. On the other, you could add it into a potion and drink it to see what happens...";
        option1Text.text = "Sell the rock.";
        option2Text.text = "Drink the rock.";
        option3Text.text = "Leave the rock alone.";
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
            
            // Event 4: Slot Machine - Insert 20 Gold.
            case 4:
                if (playerScript.GetGold() >= 20)
                {
                    playerScript.AddStat(10,-20);
                    int randomNumber = Random.Range(0,2);
                    if (randomNumber == 0)
                    {
                        descriptionText.text = "You input your coins and pull the lever. Unfortunatly the slots do not go in your favor and you lose what you bet to the machine. Better luck next time I suppose.\n\n -20 Gold";
                        hideOptions();
                    }
                    else
                    {
                        descriptionText.text = "Your bet pays off! A clink sound is heard in the tray on the bottom on the machine, and inside is a crystal!\n\n-20 Gold\n+1 Crystal";
                        playerScript.AddStat(13, 1);
                        hideOptions();
                    }

                }
                break;
            
            // Event 5: Split Paths - Go left.
            case 5:
                descriptionText.text = "You decide to go left, finding the path loops backwards for quite a while.\n\n-4 Act progress";
                progressionScript.ModifyActProgression(-4);
                hideOptions();
                break;
            
            //Event 6: Wizard Event - Ask for Max HP.
            case 6: 
                descriptionText.text = "The wizard nods his head, and mutters a few words before blasting you with some magic. You feel a bit healthier now!\n\n+15 Max HP";
                playerScript.AddStat(4,15);
                hideOptions();
                break;
            
            // Event 7: Potion Seller - Buy 2 Potions (30 Gold)
            case 7:
                if (playerScript.GetGold() >= 20)
                {
                    descriptionText.text = "You buy some cheap potions off of the man. They look like normal potions so you add them to your stash.\n\n-20 Gold\n+2 Potions";
                    playerScript.AddStat(10,-20);
                    playerScript.AddStat(11,2);
                    hideOptions();
                }
                break;
            
            // Event 8: Glowing Rock - Sell the rock.
            case 8:
                descriptionText.text = "You decide to sell the rock. Later on you sell the rock to another wandering adventurer for 200 Gold.\n\n+200 Gold";
                playerScript.AddStat(10,200);
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

            //Event 4: Slot Machine - Insert half your gold.
            case 4:
                if (playerScript.GetGold() >1)
                {
                    int betGold = (int)(Mathf.Round((playerScript.GetGold()/2)));
                    playerScript.AddStat(10,betGold*-1);
                    int randomNumber = Random.Range(0,2);
                    if (randomNumber == 0)
                    {
                        descriptionText.text = "You insert " + betGold + " gold into the machine, immediatly losing the money spent as the slots roll onto skulls. Better luck next time I suppose.\n\n-" + betGold +" Gold";
                        hideOptions();
                    }
                    else
                    {
                        float gainedCrystals = (Mathf.Round((betGold/20)));
                        descriptionText.text = "You insert " + betGold + " gold into the machine. The slot spin and land on three symbols of a crystal. It looks like you won!\n\n-"+betGold+" gold\n+"+gainedCrystals+" Crystals";
                        hideOptions();
                    }
                }
                else if (playerScript.GetGold() == 1)
                {
                    Option3Selected();
                }
                break;
            
            // Event 5: Split Paths - Go right.
            case 5:
                descriptionText.text = "You decide to go right, finding a small trasure room to the side of the path ahead.\n\n+200 Gold";
                playerScript.AddStat(10,200);
                hideOptions();
                break;
            
            // Event 6: Wizard Event - Ask for Max Energy.
            case 6:
                descriptionText.text = "The wizard nods his head. He makes some strange hand symbols then places a hand on your head. you suddenly feel a bit more energestic than before!\n\n+10 Max Energy";
                playerScript.AddStat(6,10);
                hideOptions();
                break;
            
            // Event 7: Potion Seller - Buy 6 Potions (90 Gold)
            case 7:
                if (playerScript.GetGold() >=90)
                {
                    descriptionText.text = "You buy several potions from the seller, making sure to take full advantage of the discount.\n\n-90 Gold\n+6 Potions";
                    playerScript.AddStat(10,-90);
                    playerScript.AddStat(11,6);
                    hideOptions();
                }
                break;
            
            // Event 8: Glowing Rock - Drink the Rock
            case 8: 
                descriptionText.text = "You decide to grind the rock up and add it to a potion, drinking the mixture. After a bit you feel healthier and more energized!\n\n+10 Max HP\n+10 Max Energy";
                playerScript.AddStat(4,10);
                playerScript.AddStat(6,10);
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
            
            // Event 4: Slot Machine - Insert all your gold.
            case 4:
                if (playerScript.GetGold()>0)
                {
                    int betGold = (int)(Mathf.Round((playerScript.GetGold())));
                    playerScript.AddStat(10,betGold*-1);
                    int randomNumber = Random.Range(0,2);
                    if (randomNumber == 0)
                    {
                        descriptionText.text = "You insert " + betGold + " gold into the machine, immediatly losing the money spent as the slots roll onto skulls. Better luck next time I suppose.\n\n-" + betGold +" Gold";
                        hideOptions();
                    }
                    else
                    {
                        float gainedCrystals = (Mathf.Round((betGold/20)));
                        descriptionText.text = "You insert " + betGold + " gold into the machine. The slot spin and land on three symbols of a crystal. It looks like you won!\n\n-"+betGold+" gold\n+"+gainedCrystals+" Crystals";
                        hideOptions();
                    }
                }
                break;
            // Event 6: Wizard Event - Ask for more DR.
            case 6:
                descriptionText.text = "The wizard nods, grabbing his staff from the nearby wall and infusing it with a glow. Then he whacks it against you, which does not hurt as much as you thing it would.\n\n+3 DR";
                playerScript.AddStat(7,3);
                hideOptions();
                break;
            
            // Event 7: Potion Seller - Give me your strongest potions.
            case 7: 
                descriptionText.text = "The potion seller remarks that you wouldnt be able to handle his strongest potions before hobbleing off, annoyed.";
                hideOptions();
                break;
            
            // Event 8: Glowing Rock - Leave the rock alone
            case 8: 
                descriptionText.text = "You decide to leave the glowing rock alone. Better to no mess with the mystical when given the choice.";
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

            // Event 0: Mystery Potion - Ignore the potion and continue on your way.
            case 0:
                descriptionText.text = "You decide to ignore the stange potion and move on.";
                hideOptions();
                break;
            // Event 1: Adventurer Donation - Ignore the adventurer and continue on your way.
            case 1:
                descriptionText.text = "You decide to ignore the adventurer and continue on your way.";
                hideOptions();
                break;
            
            // Event 4: Slot Machine - Gambling is bad, better to leave it alone.
            case 4:
                descriptionText.text = "Better to keep your money in the case you will need it, then throw it to the wind on a gamble.";
                hideOptions();
                break;
            
            // Event 6: Wizard Event - Refuse the offer, he is suspicious
            case 6:
                descriptionText.text = "You refuse the offer and quickly make your way forward. It rarely does well to trust stange old men in deep underground dungeons.";
                hideOptions();
                break;
            
            // Event 7: Potion Seller - refuse the offer, he is suspicious.
            case 7:
                descriptionText.text = "You refuse, quickly moving away from the shady figure. His potions must be cheap for a reason.";
                hideOptions();
                break;
            default:
                Debug.Log("How did you select this?? Current event: " + eventSelector);
                break;
        }
        audioManager.PlaySFXClick();
    }

}
