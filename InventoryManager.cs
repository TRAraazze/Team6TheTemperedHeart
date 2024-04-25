using System.Collections;
using System.Collections.Generic;
using DevionGames.StatSystem;
using TMPro;
using UnityEngine;

namespace CGP
{
    public class InventoryManager : MonoBehaviour
    {
        private string[] names = { "", "Sword", "Health Potion", "Pyralis Ore", "Cryonis Ore", "Aeris Ore", "Monster Limb", "Delicious Herb", "Elemental Scrap", "Tainted Metal", "Mystic Token", "Auric Totem"};
        public TextMeshProUGUI[] buttonarr = new TextMeshProUGUI[12];

        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemDescription;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 1; i < 12; i++)
            {
                if (StateManager.inventory[i] > 0)
                {
                    buttonarr[i].faceColor = new Color32(0,0,0,255);
                    buttonarr[i].text = names[i] + " x" + StateManager.inventory[i];
                }
                else
                {
                    buttonarr[i].faceColor = new Color32(0,0,0,45);
                    buttonarr[i].text = names[i];
                }
            }
        }

        void Update()
        {
            for (int i = 1; i < 12; i++)
            {
                if (StateManager.inventory[i] > 0)
                {
                    buttonarr[i].faceColor = new Color32(0,0,0,255);
                    buttonarr[i].text = names[i] + " x" + StateManager.inventory[i];
                }
                else
                {
                    buttonarr[i].faceColor = new Color32(0,0,0,45);
                    buttonarr[i].text = names[i];
                }
            }
        }

        public void onItem1()
        {
            itemName.text = names[1];
            itemDescription.text = "A trusty sword your Father passed down to you before he disappeared.";
            StateManager.inventory[0] = 1;
        }
        public void onItem2()
        {
            itemName.text = names[2];
            itemDescription.text = "A shimmering concotion brewed with care. Drink with caution and let the magic unfold!";
        }
        public void onItem3()
        {
            itemName.text = names[3];
            itemDescription.text = "Hot to the touch and never seems to stop glowing. This strange ore may be the answer to discovering newfound power.";
        }
        public void onItem4()
        {
            itemName.text = names[4];
            itemDescription.text = "Cold to the touch and constantly releases a strange mist. Maybe this ore has some special property.";
        }
        public void onItem5()
        {
            itemName.text = names[5];
            itemDescription.text = "Every time you lay your hand on this ore, a gust of wind blows past you. Is there more to this ore than what meets the eye?";
        }
        public void onItem6()
        {
            itemName.text = names[6];
            itemDescription.text = "A small decrepit piece of an evil creature.";
        }
        public void onItem7()
        {
            itemName.text = names[7];
            itemDescription.text = "An herb that looks as good as it smells, I bet it would help make a delicious stew.";
        }
        public void onItem8()
        {
            itemName.text = names[8];
            itemDescription.text = "A small shard of mineral from an Elemental Golem.";
        }
        public void onItem9()
        {
            itemName.text = names[9];
            itemDescription.text = "A piece of armor that seems to be tainted by some sort of evil temperament.";
        }
        public void onItem10()
        {
            itemName.text = names[10];
            itemDescription.text = "Rewarded from the bounty keeper, this token can be used in the market at a special vendor.";
        }
        public void onItem11()
        {
            itemName.text = names[11];
            itemDescription.text = "This totem gleams with the energy of life. There is a source of magic beckoning you to understand it.";
        }
        
    }
}
