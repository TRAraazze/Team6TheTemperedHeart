using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class MapManager : MonoBehaviour
    {
        public GameObject[] questButtons;
        public GameObject[] questButtons2; // Reference to the quest buttons in the map UI

        private void Start()
        {
            if(StateManager.questDialogue[2] == false)
            {
                questButtons[0].SetActive(true);
            }
            if(StateManager.questDialogue[2] == false)
            {
                questButtons2[0].SetActive(true);
            }
            if (StateManager.questDialogue[12] == false)
            {
                questButtons[1].SetActive(true);
            }
            if (StateManager.questDialogue[12] == false)
            {
                questButtons2[1].SetActive(true);
            }
            if (StateManager.questDialogue[17] == false)
            {
                questButtons[2].SetActive(true);
            }
            if (StateManager.questDialogue[17] == false)
            {
                questButtons2[2].SetActive(true);
            }
        }
    }
}
