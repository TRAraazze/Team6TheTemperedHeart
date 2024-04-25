using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;
using Unity.VisualScripting;


namespace CGP
{
    using UnityEngine;


    public class SirIgnus : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[10];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[10])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questDialogue[19] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "You are the only one who can save us now!",
                            "Sir Ignus");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        break;
                }
            }
            else if(StateManager.questDialogue[19] == true && StateManager.inventory[9] >= 8)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You made it!", "Sir Ignus");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "I took the liberty of retrieving the final piece of the key for you, given the situation we are in.",
                            "Sir Ignus");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "You should take this back over to the portal, and combined with your other relics, open it and press forward.",
                            "Sir Ignus");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[19] = false;
                        StateManager.questComplete[18] = true;
                        StateManager.inventory[11] = 3;
                        StateManager.hasQuest[10] = false;
                        questMarkNew.SetActive(false);
                        StateManager.inventory[9] -= 8;
                        interactionCount = 0;
                        break;
                }
            }

        }

        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player is inside the collider, enable interaction
                EnableInteraction();
                isInteractable = true;
                infoText.text = "Press E to Speak";
                infoTextCanvas.alpha = 1; //this makes everything transparent
                infoTextCanvas.blocksRaycasts = true; //this prevents the UI element to receive input events
            }
        }

        // Check for player exiting the collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player has exited the collider, disable interaction
                DisableInteraction();
                isInteractable = false;
                infoText.text = null;
                infoTextCanvas.alpha = 0; //this makes everything transparent
                infoTextCanvas.blocksRaycasts = false; //this prevents the UI element to receive input events
                _dialogueBox.EndDialogue();
            }
        }

        // Enable interaction
        void EnableInteraction()
        {
            // Subscribe to the key press event
            if (!Input.GetKeyDown(KeyCode.E))
            {
                Input.GetKeyDown(KeyCode.E);
            }
        }

        // Disable interaction
        void DisableInteraction()
        {
            // Unsubscribe from the key press event
            if (Input.GetKeyDown(KeyCode.E))
            {
                Input.GetKeyUp(KeyCode.E);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check for 'E' key press while player is inside collider
            if (Input.GetKeyDown(KeyCode.E) && isInteractable)
            {
                OnInteraction();
            }
        }
    }
}
