using System.Data.Common;
using CGP;
using DevionGames;
using DevionGames.StatSystem;
using TMPro;


namespace CGP
{
    using UnityEngine;
    
    
    public class Hermit : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        public GameObject questMarkNew;
        public static bool hasQuest = StateManager.hasQuest[4];
        private void Start()
        {
            questMarkNew = gameObject.FindChild("QuestMarkNew", true);
            if (StateManager.hasQuest[4])
            {
                questMarkNew.SetActive(true);
            }
            else questMarkNew.SetActive(false);
        }
        
        // Function to call when interacting
        void OnInteraction()
        {
            if (StateManager.questProgress[5] > 0)
            {
                // Implement your interaction logic here
                if (StateManager.questProgress[6] >= 3)
                {
                    // Implement your interaction logic here
                    if (StateManager.questProgress[7] > 0)
                    {
                        // Implement your interaction logic here
                        if (StateManager.questComplete[10] == true)
                        {
                            // Implement your interaction logic here
                    
                        }
                        else if(StateManager.questDialogue[10] == true)
                        {
                            // Implement your interaction logic here
                            switch (interactionCount)
                            {
                                case (0):
                                    _dialogueBox.ShowDialogue("You’re back! And with the relic too I see.", "The Hermit");
                                    interactionCount++;
                                    break;
                                case (1):
                                    _dialogueBox.ShowDialogue(
                                        "When I made contact with it I felt this rush of energy that I've never felt before. It almost felt like...it was whispering my name with reverberating pulses.",
                                        "You");
                                    interactionCount++;
                                    break;
                                case (2):
                                    _dialogueBox.ShowDialogue(
                                        "These ancient relics, although having been around for centuries, still continue to bewilder even old geezers like myself. I cannot explain these things.",
                                        "The Hermit");
                                    interactionCount++;
                                    break;
                                case (3):
                                    _dialogueBox.ShowDialogue(
                                        "I recommend you head back to Aurum by the wooden cart from where you came from and see the Elder in the castle there. He should know your next steps.",
                                        "The Hermit");
                                    interactionCount++;
                                    break;
                                case (4):
                                    _dialogueBox.ShowDialogue(
                                        "Before you go, there is one more thing you should know. The power that you have harnessed from Pyralis Ore can likely carry over into any elemental ore you find.",
                                        "The Hermit");
                                    interactionCount++;
                                    break;
                                case (5):
                                    _dialogueBox.ShowDialogue(
                                        "When you collect enough from another area, I would estimate around 3 chunks, try to draw power from that element.",
                                        "The Hermit");
                                    interactionCount++;
                                    break;
                                case (6):
                                    _dialogueBox.EndDialogue();
                                    interactionCount = 0;
                                    StateManager.questDialogue[10] = false;
                                    StateManager.questComplete[7] = true;
                                    StateManager.hasQuest[4] = false;
                                    questMarkNew.SetActive(false);
                                    break;
                            }
                        }
                    }
                    else if(StateManager.inventory[3] >= 3 && StateManager.questDialogue[7] == true)
                    {
                        // Implement your interaction logic here
                        switch (interactionCount)
                        {
                            case (0):
                                _dialogueBox.ShowDialogue("You have the ore, yes?", "The Hermit");
                                interactionCount++;
                                break;
                            case (1):
                                _dialogueBox.ShowDialogue(
                                    "Yes, I have 3 with me. Here you go.",
                                    "You");
                                interactionCount++;
                                break;
                            case (2):
                                //_dialogueBox.ShowDialogue(
                                //    "From what I have in my notes, all you need to do to unleash Celestium Temperament is use the ore you just got to forge an elemental effect similiar to the properties of the ore.",
                                //    "The Hermit");
                                _dialogueBox.ShowDialogue(
                                    "Thank you. From what I have in my notes, I need those 3 Pyralis Ore as well as an ancient relic.",
                                    "The Hermit");
                                interactionCount++;
                                
                                //StateManager.editItemCount(3, -3);
                                break;
                            case (3):
                                _dialogueBox.ShowDialogue(
                                    "In total, you need the 2 other ores to harness all of these natural elements. Pyralis is fire, Cryonis is ice and Aeris is wind.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                                /*
                            case (4):
                                _dialogueBox.ShowDialogue(
                                    "Go ahead, try it…",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (5):
                                _dialogueBox.EndDialogue();
                                StateManager.editItemCount(3,-3);
                                interactionCount++;
                                break;
                                
                            case (6):
                                _dialogueBox.ShowDialogue(
                                    "Voila… you are already a natural",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (7):
                                _dialogueBox.ShowDialogue(
                                    "I sure wish I could do that myself... would’ve bailed me out of some tough situations.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (8):
                                _dialogueBox.ShowDialogue(
                                    "Thank you for your help, I’ll make great use of this.",
                                    "You");
                                interactionCount++;
                                break;
                            case (9):
                                _dialogueBox.ShowDialogue(
                                    "Of course! You can likely apply this to other elemental types of ores, but I’m not sure you could find more without going to another biome.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                                */
                            case (4):
                                _dialogueBox.ShowDialogue(
                                    "Now this ancient relic that I speak of is called an Auric totem, it holds the power to unlock mystical portals.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            /*
                            case (11):
                                _dialogueBox.ShowDialogue(
                                    "The name of these relics are auric totems. From what I have read, these totems are spread throughout Aurum, and are tied to the origins of Celestium, but nobody has been able to make out any use for them.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (12):
                                _dialogueBox.ShowDialogue(
                                    "Essentially, they just remain as “shiny totems” that attract travellers. However, you might be the exception.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            */
                            case (5):
                                _dialogueBox.ShowDialogue(
                                    "Go to the cave at the edge of the forest, fight your way inside and find the relic that's hidden away in there.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (6):
                                _dialogueBox.ShowDialogue(
                                    "By the way, since you harness the same power your father did, you might be able to use the essence of that ore to unlock a new level of power. Try it out.",
                                    "The Hermit");
                                interactionCount++;
                                break;
                            case (7):
                                _dialogueBox.ShowDialogue(
                                    "(You can access the skill tree by clicking on the book icon located in the top left corner and choose which power you want to use.)",
                                    "", false);
                                interactionCount++;
                                break;
                            case (8):
                                _dialogueBox.EndDialogue();
                                interactionCount = 0;
                                StateManager.editItemCount(3, -3);
                                StateManager.questDialogue[7] = false;
                                StateManager.questComplete[6] = true;
                                StateManager.isFireUnlocked = true;
                                break;
                        }
                    }
                }
                else if(StateManager.questDialogue[6] == true && StateManager.questProgress[5] >= 1)
                {
                    // Implement your interaction logic here
                    switch (interactionCount)
                    {
                    case (0):
                        _dialogueBox.ShowDialogue("I see that you have made it back and in one piece at that.", "The Hermit");
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.ShowDialogue(
                            "Give me that journal and I will let you know what I find. In the meantime, could you gather 3 Pyralis Ore for me?",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "Sure, but what for?",
                            "You");
                        interactionCount++;
                        break;
                            /*
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "To make use of Celestium Temperament, you must use ores that resonate with elemental effects.",
                            "The Hermit");
                        interactionCount++;
                        break;
                            */
                    case (3):
                        _dialogueBox.ShowDialogue(
                            "Bring me back the ore and I can tell you more. It is a crimson ore that can be found around large rock formations here in the forest.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[6] = false;
                        StateManager.questComplete[5] = true;
                        break;
                }
                }
            }
            else if(StateManager.questDialogue[5] == true)
            {
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue("Who... who are you? Nobody has visited me in years...", "The Hermit");
                        interactionCount++;
                        break;
                    case (1):
                        /*
                        _dialogueBox.ShowDialogue(
                            "I am the son of Aurum's late royal blacksmith. I was told you may have some insight into the power that was passed down my family bloodline.",
                            "You");
                        */
                        _dialogueBox.ShowDialogue(
                          "I am the son of Aurum's late royal blacksmith. I was told you may have some insight into creating a portal to allow travel between us and Titania.",
                          "You");
                        interactionCount++;
                        break;
                    case (2):
                        _dialogueBox.ShowDialogue(
                            "The royal blacksmith? I see... You do have his eyes now that I think about it... I had quite the history with your father.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (3):
                        /*
                        _dialogueBox.ShowDialogue(
                            "Anyways, about the power you speak of...I believe you are talking about Celestium Temperament, correct?",
                            "The Hermit");
                        */
                        _dialogueBox.ShowDialogue(
                            "Anyways, about this portal you speak of...I believe I may be able to help you.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (4):
                        _dialogueBox.ShowDialogue(
                            "I might have some idea as to what need to create one, but in return I'll need your help with something first.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (5):
                        _dialogueBox.ShowDialogue(
                            "I recently ran into some hostile giant spiders and tried to fend them off but it went south and they overpowered me.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (6):
                        _dialogueBox.ShowDialogue(
                            "I happened to be carrying an old journal that has this certain information you seek, but you will likely have to fight your way to it.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (7):
                        _dialogueBox.ShowDialogue(
                            "If you can manage that, I will help you with creating this portal.",
                            "The Hermit");
                        interactionCount++;
                        break;
                    case (8):
                        _dialogueBox.EndDialogue();
                        interactionCount = 0;
                        StateManager.questDialogue[5] = false;
                        StateManager.questComplete[2] = true;
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
