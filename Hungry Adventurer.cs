using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class HungryAdventurer : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[8];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[8])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.inventory[7] >= 5 && StateManager.questComplete[14] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "Mmm... they smell just as good as I thought they would... take this gold as a reward, you can put it to better use.",
                            "Hungry Adventurer");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        StateManager.editItemCount(7, -5);
                        StateManager.editCurrency(20);
                        StateManager.questComplete[14] = true;
                        StateManager.hasQuest[8] = false;
                        questMarkNew.SetActive(false);
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
                        _dialogueBox.ShowDialogue("You there! Would you mind helpin' me out?", "Hungry Adventurer");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "I've just arrived here after a long day of hiking and I am super hungry, but I've heard things about a delicious herb in this area that could make for a great stew.",
                            "Hungry Adventurer");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "If you could bring me back 5 of those there herbs, I would be over the moon!",
                            "Hungry Adventurer");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        StateManager.questDialogue[14] = false;
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
