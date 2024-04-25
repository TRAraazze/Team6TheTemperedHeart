using System;
using System.Collections;
using System.Collections.Generic;
using DevionGames.StatSystem;
using TMPro;
using UnityEngine;

namespace CGP
{
    public class QuestManager : MonoBehaviour
    {
        private string[] names = { "", "Awakening", "The Royal Hall", "The Lost Heirloom", "Sound of Revenge", "The Hermit", "First Steps", "Moment of Truth", "Against the Grain", "A Kindled Flame", "Enchanting Return", "Trapped", "The Mountaineer", "Cave Diver", "Herbs n Spices", "A Mountainous Task", "Beyond the Summit", "One Final Push", "Take it Back", "Keystone", "Finale", "The Portal"};
        public TextMeshProUGUI[] buttonarr = new TextMeshProUGUI[22];
        public TextMeshProUGUI questName;
        public TextMeshProUGUI questDescription;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        void Update()
        {
            for (int i = 1; i < 22; i++)
            {
                if (i == 9 || i == 15) continue;
                if (!StateManager.questDialogue[i])
                {
                    if (StateManager.questComplete[i])
                    {
                        buttonarr[i].color = new Color32(0, 0, 0, 130);
                        buttonarr[i].text = "<s>" + names[i] + "</s>";
                    }
                    else
                    {
                        buttonarr[i].color = new Color32(0, 0, 0, 255);
                        buttonarr[i].text = names[i];
                    }
                }
                else
                {
                    buttonarr[i].color = new Color32(0,0,0,45);
                    buttonarr[i].text = names[i];
                }
            }
        }

        public void onQuest1()
        {
            questName.text = names[1];
            questDescription.text = "Talk to the Royal Elder in the Castle to find out more about what is happening.";
        }
        public void onQuest2()
        {
            questName.text = names[2];
            questDescription.text = "Go to the forest and meet the Elder's person of interest.";
        }
        public void onQuest3()
        {
            questName.text = names[3];
            questDescription.text = "Help the citizen in distress find their lost family heirloom. They last saw it over near the vendor market.";
        }
        public void onQuest4()
        {
            questName.text = names[4];
            questDescription.text = "Take revenge for the shepherd by killing 5 spiders.";
        }
        public void onQuest5()
        {
            questName.text = names[5];
            questDescription.text = "Retrive the Hermit's journal from the hostile encampment near the windmill.";
        }
        public void onQuest6()
        {
            questName.text = names[6];
            questDescription.text = "Retrieve 3 Pyralis Ore.";
        }
        public void onQuest7()
        {
            questName.text = names[7];
            questDescription.text = "Go to the cave at the edge of the forest and find the Auric Totem there.";
        }
        public void onQuest8()
        {
            questName.text = names[8];
            questDescription.text = "Deliver the rancher's package to him in Aurum.";
        }
        public void onQuest9()
        {
            questName.text = names[9];
            questDescription.text = "Take down the Fire Elemental in the Forest Keep.";
        }
        public void onQuest10()
        {
            questName.text = names[10];
            questDescription.text = "Return to the Elder in Aurum.";
        }
        public void onQuest11()
        {
            questName.text = names[11];
            questDescription.text = "Rescue the little boys treasure chest from the bandit encampment.";
        }
        
        public void onQuest12()
        {
            questName.text = names[12];
            questDescription.text = "Meet the man in the mountains that supposedly has information regarding another Auric Totem.";
        }
        
        public void onQuest13()
        {
            questName.text = names[13];
            questDescription.text = "Explore the cave in the mountains in search of another lost relic.";
        }
        
        public void onQuest14()
        {
            questName.text = names[14];
            questDescription.text = "Help the hungry adventurer gather 5 herbs from under nearby trees.";
        }
        
        public void onQuest15()
        {
            questName.text = names[15];
            questDescription.text = "Take down the Wind Golem inside the mountain keep. (Look for the castle double doors)";
        }
        
        public void onQuest16()
        {
            questName.text = names[16];
            questDescription.text = "Return to the Elder in Aurum with your newfound power and information.";
        }
        
        public void onQuest17()
        {
            questName.text = names[17];
            questDescription.text = "Seek out the Garrison Knights in the Tundra.";
        }
        
        public void onQuest18()
        {
            questName.text = names[18];
            questDescription.text = "Help the Knights defeat enough Titania Soldiers to gather 8 tainted metal, and report back inside of the Garrison.";
        }
        
        public void onQuest19()
        {
            questName.text = names[19];
            questDescription.text = "Take the final auric totem, along with the others, and reopen the portal to Titania.";
        }
        
        public void onQuest20()
        {
            questName.text = names[20];
            questDescription.text = "Explore the Titania Castle, find what has been threatening Aurum, and put a stop to it.";
        }
        
        public void onQuest21()
        {
            questName.text = names[21];
            questDescription.text = "Return through the portal once and for all, then talk to the Elder.";
        }
        
    }
}
