using TMPro;

namespace CGP
{
    using UnityEngine;
    
    
    public class PotionShop : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        private int cost;
        
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.currency > cost)
            {
                StateManager.editCurrency(-5);
                StateManager.editItemCount(2, 1);
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
                if (StateManager.inventory[2] < 3)
                {
                    infoText.text = "Press E to Buy Potion (" + cost + " Gold Each)";
                }
                else
                {
                    infoText.text = "You have max Potions";
                }
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

            cost = 5;
        }
    }
}