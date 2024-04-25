using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class Shepherd : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest1 = StateManager.hasQuest[2];
        public static bool hasQuest2 = StateManager.hasQuest[3];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[2] || (StateManager.hasQuest[5] == false && StateManager.questComplete[8] == false))
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questProgress[8] > 0 && StateManager.questComplete[8] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "You aren't the man that usually brings me my grain, but alas, I don't exactly have any livestock to tend to anymore...",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Regardless, I owe you payment for the delivery, so here you go, and thank you!",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.EndDialogue();
                        StateManager.questComplete[8] = true;
                        StateManager.editCurrency(20);
                        StateManager.hasQuest[2] = false;
                        interactionCount = 0;
                        break;
                }
            }
            if (StateManager.questProgress[4] >= 5 && StateManager.questComplete[4] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "Is it done?",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Yes, I finished off 5 of them.",
                            "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Thank you, what you have done on this day is bring about justice. Here is payment for your deed.",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        StateManager.questComplete[4] = true;
                        StateManager.editCurrency(10);
                        hasQuest1 = false;
                        interactionCount = 0;
                        break;
                }
            }
            else if(StateManager.questDialogue[4] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You look like you can use a weapon... could you do something for me?", "Shepherd");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "When I was grazing my sheep through the countryside yesterday, and then a horde of spiders came up and killed every last one of them.",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "I am sick and tired of those evil creatures. Kill 5 of them for me to bring justice to those foul things.",
                            "Shepherd");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[4] = false;
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
