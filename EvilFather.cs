using TMPro;

namespace CGP
{
    using DevionGames;
    using UnityEngine;


    public class EvilFather : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void FreezeDeath()
        {
            animator.enabled = false;
        }

        // Function to call when interacting
        void OnInteraction()
        {
            if (questComplete)
            {
                
            }
            //else if (StateManager.questDialogue[1] == true)
            else if (true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("I... I couldn't see it coming. Your skill is much stronger than mine. My power is gone. Drained. Go. Back to Aurum.", "Your Father");
                        interactionCount++;
                        break;

                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        //StateManager.questDialogue[1] = false;

                        break;
                }
            }
            else
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("...I'm sorry son...", "Your Father");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount--;
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