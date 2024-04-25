using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;
    
    
    public class Townsfolk1 : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[1];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[1])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        
        // Function to call when interacting
        void OnInteraction()
        {
            Debug.Log("on interaction");
            if (StateManager.questProgress[3] > 0)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("My goodness, you found it! I am forever grateful!", "Distressed Citizen");
                        interactionCount++;
                        hasQuest = false;
                        questMarkNew.SetActive(false);
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        StateManager.editKarma(0.05);
                        StateManager.questComplete[3] = true;
                        StateManager.hasQuest[1] = false;
                        questMarkNew.SetActive(false);
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue("I hope you are doing well on this fine day...", "Appeased Citizen");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        interactionCount--;
                        break;
                }
            }
            else if(StateManager.questDialogue[3] == true)
            {
                Debug.Log("if quest dialogue 3 is true");
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("HEY! You there, your face looks familiar but I don't think I've ever seen you before...", "Distressed Citizen");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Nevermind that, would you mind giving me a moment of your time, I quite believe that I have lost a family heirloom somewhere.",
                            "Distressed Citizen");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "It was a solid gold goblet, but I haven’t seen it since I last was buying some goods over in the Shopping Center.",
                            "Distressed Citizen");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "Of course, I will let you know what I find.",
                            "You");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[3] = false;
                        break;
                }
            }
            else
            {
                Debug.Log("something is off here");
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
