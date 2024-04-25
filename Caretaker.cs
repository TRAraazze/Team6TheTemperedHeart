using TMPro;

namespace CGP
{
    using DevionGames;
    using UnityEngine;
    
    
    public class Caretaker : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;


        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[0];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[0])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }

        // Function to call when interacting
        void OnInteraction()
        {
            if (questComplete)
            {
                
            }
            else if(StateManager.questDialogue[1] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Oho! You're finally awake!", "Thorim");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Finally? How long have I been out for? My head feels all foggy...",
                            "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Its been almost <b><color=\"yellow\">10 years</b><color=\"white\"> now! Much has happened...we are in dire need of your help.",
                            "Thorim");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "Can you remember anything about any special power that your father possessed when he was the royal blacksmith?",
                            "Thorim");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.ShowDialogue(
                            "Perhaps faintly, I always knew our family had a special affinity for smithing, but not much more than that.",
                            "You");
                        interactionCount++;
                        break;
                    case (5):
                        _dialogueBox.ShowDialogue(
                            "Over the past years, a notorious group from our enemy continent Titania has learned to abuse this mystical smithing power, Celestium Temperament.",
                            "Thorim");
                        interactionCount++;
                        break;
                    case (6):
                        _dialogueBox.ShowDialogue(
                            "However, it shouldn't be possible, since your Father was the last one who could harness this power, yet he vanished right before you feel into a coma.",
                            "Thorim");
                        interactionCount++;
                        break;
                    case (7):
                        _dialogueBox.ShowDialogue(
                            "Since this ability transfers through your bloodline, we of Aurum have been hoping that you may somehow use this power to counter Titania's evil.",
                            "Thorim");
                        interactionCount++;
                        break;
                    case (8):
                        _dialogueBox.ShowDialogue(
                            "This is all still so much to take in...where do I even start?",
                            "You");
                        interactionCount++;
                        break;
                    case (9):
                        _dialogueBox.ShowDialogue(
                            "Go, talk to the Elders in the castle. They will know what you must do.",
                            "Thorim");
                        interactionCount++;
                        break;
                    
                    case (10):
                        _dialogueBox.ShowDialogue(
                            "*Thorim's eyes light up suddenly, as if being possessed by some otherwordly being*",
                            "", true);
                        interactionCount++;
                        break;
                    
                    case (11):
                        _dialogueBox.ShowDialogue(
                        "(Press 'Alt' to toggle your mouse and check out the controls via the controls button at the top of the screen.)",
                        "", true);
                        interactionCount++;
                        break;
                    case (12):
                        _dialogueBox.ShowDialogue(
                        "(Use anvils strung about the map to save your game, they act as checkpoints.)",
                        "", true);
                        interactionCount++;
                        break;
                    case (13):
                        _dialogueBox.ShowDialogue(
                        "(When fighting enemies, remember to use your dodge roll by tapping 'Shift', you will gain a small amount of invincibility during it.)",
                        "", true);
                        interactionCount++;
                        break;
                    case (14):
                        _dialogueBox.ShowDialogue(
                        "(After leaving Aurum, return here by visiting the wooden carts in each area.)",
                        "", true);
                        interactionCount++;
                        break;
                    case (15):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[1] = false;
                        questMarkNew.SetActive(false);
                        StateManager.hasQuest[0] = false;
                        break;
                }
            }
            else
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("How goes it? I know you will do great things for our city...", "Thorim");
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