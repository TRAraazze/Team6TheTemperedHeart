using System.Data.Common;
using CGP;
using DevionGames;
using TMPro;


namespace CGP
{
    using UnityEngine;


    public class Elder : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        
        public GameObject questMarkNew;
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if ((StateManager.questDialogue[16] == false && StateManager.questComplete[16] == false) || (StateManager.questDialogue[10] == false && StateManager.questComplete[10] == false) || (StateManager.questDialogue[1] == false && StateManager.questDialogue[2] == true))
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }

        // Function to call when interacting
        void OnInteraction()
        {
            if (FinalBossManager.defeated && StateManager.questComplete[21] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You did it, you've saved the kingdom!", "Royal Elder");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "I can't put into words my gratitude, as I am sure many others can't either. Coming out of a coma and fighting back against evil is not something everyone has to experience.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "You will be knighted by our royal court, and forever be known as the blacksmith with the Tempered Heart",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "Now go forth, and find what other adventures await you!",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.editCurrency(200);
                        StateManager.questComplete[21] = true;
                        questMarkNew.SetActive(false);
                        break;
                }
            }
            else if (StateManager.questDialogue[16] == false && StateManager.questComplete[16] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Back again I see, did you learn anything from the man in the mountains?", "Royal Elder");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "He seemed to have some passing knowledge about an old portal in snow. Apparently we could use the auric relics to possibly reactivate it.",
                            "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "That might just be our best option. Go out to the glaciers and meet with the knights of the garrison there. They can lead you forward.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[17] = false;
                        StateManager.questComplete[16] = true;
                        questMarkNew.SetActive(false);
                        break;
                }
            }
            else if (StateManager.questDialogue[10] == false && StateManager.questComplete[10] == false)
            {
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Welcome back! By the looks of it, you have already been learning how use your Celestium Temperament.", "Royal Elder");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "To create a portal, I need the 3 elemental relics. Do you know where I can find the other two?",
                            "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Well, while you were gone, word seems to have spread to the different parts of Aurum.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "I received a letter from a man in the nearby mountains saying he has been seeing terrifying infested creatures around his property.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.ShowDialogue(
                            "These infected monsters could be a sign of consuming this elemental aura. I advise you to go check them out.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (5):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[12] = false;
                        StateManager.questComplete[10] = true;
                        questMarkNew.SetActive(false);
                        break;
                }
            }
            else if (StateManager.questDialogue[1] == false && StateManager.questDialogue[2] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("You’re awake! I had heard murmurs from people passing through the district, but to see you in person...", "Royal Elder");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "A man named Thorim said you could point me in a direction to start learning about this temperament power inherited from my father.",
                            "You");
                        interactionCount++;
                        break;
                    /*
                case (2):
                    _dialogueBox.ShowDialogue(
                        "Ah yes, of course... Head over to the forest just outside the gates. There lives a hermit who has dedicated his life to studying strange phenomena.",
                        "Royal Elder");
                    interactionCount++;
                    break;
                */
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Ah yes, Celestium Temperament. This is a unique power that few in the kingdom know about. I am one of them.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "However, I myself do not know the secrets of this power and how to unlock it... although I think I know a man who might.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.ShowDialogue(
                            "Head over to the forest just outside the city's gates. There lives a hermit who has dedicated his life to studying strange phenomena.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                        /*
                    case (5):
                        _dialogueBox.ShowDialogue(
                            "Why do I have to do this?",
                            "You");
                        interactionCount++;
                        break;
                    case (6):
                        _dialogueBox.ShowDialogue(
                            "Sadly, it is not quite as simple as that. Titania is much stronger than us since your father has gone...But now you are here.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                    case (7):
                        _dialogueBox.ShowDialogue(
                            "Now go, to the hermit in the forest. I will be waiting here.",
                            "Royal Elder");
                        interactionCount++;
                        break;
                        */
                    case (5):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[2] = false;
                        StateManager.questComplete[1] = true;
                        questMarkNew.SetActive(false);
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
