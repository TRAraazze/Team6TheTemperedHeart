using TMPro;

namespace CGP
{
    using UnityEngine;
    
    
    public class BountyKeeper : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questProgress[9] > 0)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You really did it huh? Well... I guess I owe you your reward.", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue("This here mystic token is collected by magic users, so maybe you could trade it to the mage over there for some imbuements to your strength.", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questComplete[9] = true;
                        StateManager.editItemCount(10,1);
                        StateManager.questProgress[9] = -1000;
                        break;
                }
            }
            else if(StateManager.questComplete[7] == true && StateManager.questDialogue[9] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You lookin' for some more work, I might have something up your alley", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue("There is a wild Fire Elemental that has taken over a cavern out in the forest.", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue("If you can eliminate it, I'll give you a special reward.", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue("Go out to the forest and look for castle doors on the side of a cliff, that's where it'll be.", "Mysterious Figure");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[9] = false;
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