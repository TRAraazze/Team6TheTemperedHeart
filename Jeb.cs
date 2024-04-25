using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class Jeb : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[6];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[6])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questProgress[13] > 0)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You really made it back alive! I'm honestly impressed, with how dangerous things have gotten.", "Jeb");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Now that I look at the relic you have brought back, it looks similiar to a keyhole in the old portal that I saw during a trip to the glaciers.",
                            "Jeb");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "However, it looked as if there was two other slots to unlock whatever those locks were guarding, maybe if you found all 3 of those relics, you could reopen that portal",
                            "Jeb");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "The royal elder mentioned something like that before, I will go back and let him know about the keyholes.",
                            "You");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[16] = false;
                        StateManager.questComplete[13] = true;
                        StateManager.hasQuest[6] = false;
                        questMarkNew.SetActive(false);
                        interactionCount = 0;
                        break;
                }
            }
            else if(StateManager.questDialogue[13] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Hey, you must be the one from <b><color=\"yellow\">Aurum</b><color=\"white\"> that I've heard so much about!", "Jeb");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "I need your help, I was told that you might be able to help me find a <b><color=\"yellow\">Auric Relic</b><color=\"white\"> with special properties.",
                            "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Ah yes! I think that it's located in the caves over by the mountains in front of me. However, most people have agreed to leave it alone as it is guarded by some nasty <b><color=\"yellow\">Metalons</b>.",
                            "You");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "Thank you for the information. Regardless of what danger faces me, I must try for the sake of the kingdom.",
                            "You");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[13] = false;
                        StateManager.questComplete[12] = true;
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
