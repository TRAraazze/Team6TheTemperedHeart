using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CGP
{

    public class Skill : MonoBehaviour
    {
        public int id; // Unique ID of the skill

        public TMP_Text title; // Text UI element for the title
        public TMP_Text description; // Text UI element for the description
        public SkillTree skilltree; // Reference to the skill tree (not used in this script)

        // Define categories based on the ranges specified: 0-2, 3-5, 6-8
        // This function returns the category index for a given skill ID


        public int GetCategoryIndex(int id)
        {
            if (id >= 0 && id <= 2) return 0;
            if (id >= 3 && id <= 5) return 1;
            if (id >= 6 && id <= 8) return 2;
            // Return -1 if ID doesn't match any category (error handling)
            return -1;
        }

        public bool isLocked(int id)
        {
            int categoryIndex = GetCategoryIndex(id);
            if (categoryIndex == 0 && !StateManager.isFireUnlocked)
            {
                return true; 
            }
            if (categoryIndex == 1 && !StateManager.isAirUnlocked)
            {
                return true;
            }
            if (categoryIndex == 2 && !StateManager.isIceUnlocked)
            {
                return true; 
            }
            return false; 


        }

        public void UpdateUI()
        {
            // Update the UI elements for the skill based on the current status
            title.text = $"{SkillTree.skillNames[id]}";
            title.fontSize = 12;
            title.color = Color.black;
            description.text = $"{SkillTree.skillDescriptions[id]}";
            description.color = Color.black;

            // Set the button color based on skill status
            GetComponent<Image>().color = SkillTree.skillStatus[id] ? Color.white : Color.gray;


            if (isLocked(id))
            {
                GetComponent<Image>().color = new Color32(0x33, 0x33, 0x33, 0xFF);

            }


        }

        public void OnClick()
        {
            // Get the category index of the clicked skill
            int categoryIndex = GetCategoryIndex(id);

            // Check if the category is valid
            if (categoryIndex != -1)
            {
                // Iterate through all skills in the same category
                for (int i = categoryIndex * 3; i < (categoryIndex + 1) * 3; i++)
                {
                    if (i != id)
                    {
                        // Unselect the skill if it's not the clicked one
                        SkillTree.skillStatus[i] = false;
                    }
                }

                // Toggle the clicked skill's status
                SkillTree.skillStatus[id] = !SkillTree.skillStatus[id];
            }

            // Update the UI after changing the skill status
            UpdateUI();
        }

        public bool isCategoryIndexUnlocked(int categoryIndex)
        {
            if (categoryIndex == 0 && StateManager.isFireUnlocked)
            {
                return true;
            }

            if (categoryIndex == 1 && StateManager.isAirUnlocked)
            {
                return true;
            }

            if (categoryIndex == 2 && StateManager.isIceUnlocked)
            {
                return true;
            }

            return false;
        }
    }

}
