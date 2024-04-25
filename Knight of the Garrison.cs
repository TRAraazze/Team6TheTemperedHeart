using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class KnightOfTheGarrison : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[9];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[9])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questComplete[18])
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "You are the only one who can save us now!",
                            "Knight of the Garrison");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        break;
                }
            }
            else if(StateManager.questDialogue[18] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Are you the magic blacksmith from <b><color=\"yellow\">Aurum</b><color=\"white\">? We've been waiting for you!", "Knight of the Garrison");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "It seems the enemy has found out about your plan to end their reign, and have sent reinforcements to protect the portal.",
                            "Knight of the Garrison");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Fight your way to the garrison up ahead... There is a knight there that has retrieved the last piece you need to open the portal.",
                            "Knight of the Garrison");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[18] = false;
                        StateManager.questComplete[17] = true;
                        StateManager.hasQuest[9] = false;
                        questMarkNew.SetActive(false);
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
