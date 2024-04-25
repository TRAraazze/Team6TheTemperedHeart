using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class YoungBoy : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[7];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[7])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        // Function to call when interacting
        void OnInteraction()
        {
            // Implement your interaction logic here
            if (StateManager.questProgress[11] > 0)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(
                            "Thank you so much for saving my chest of valuables, I am forever in your debt!", "Young Boy");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questComplete[11] = true;
                        StateManager.hasQuest[7] = false;
                        questMarkNew.SetActive(false);
                        StateManager.editKarma(0.5);
                        break;
                }
            }
            else if (StateManager.questDialogue[11] == true)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Now that you're here at the mountains, there is someone you should talk to. His name is <b><color=\"yellow\">Jeb</b><color=\"white\"> and he lives over by the big tree ahead of us.", "Young Boy");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "He might know where you can find that strange artifact you were talking about, the <b><color=\"yellow\">Auric Totem</b><color=\"white\">.",
                            "Young Boy");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "I'll go check it out right now.",
                            "You");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "But wait, before you go, I have a request.",
                            "Young Boy");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.ShowDialogue(
                            "Some bandits came and forcibly stole my family's chest of valuables because I was too weak to stop them. Can you go and retrieve it for me?",
                            "Young Boy");
                        interactionCount++;
                        break;
                    case (5):
                        _dialogueBox.ShowDialogue(
                            "They're inhabiting a house to the left of the mountains. Good luck!",
                            "Young Boy");
                        interactionCount++;
                        break;
                    case (6):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[11] = false;
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
