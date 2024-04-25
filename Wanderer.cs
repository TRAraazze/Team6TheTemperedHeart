using System.Collections.Generic;
using System.Data.Common;
using CGP;
using TMPro;
using UnityEngine.AI;


namespace CGP
{
    using UnityEngine;
    
    
    public class Wanderer : MonoBehaviour
    {
        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private bool questComplete = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        private NavMeshAgent _agent;
        private int waypoint = 0;
        public List<Transform> waypoints;
        private Animator _animator;
        private bool _atEnd = false;
        private bool _moving = true;
        private string[] dialogueOpts = { "I hope you are doing well on this fine day...",
                                          "You must be the blacksmith's son...",
                                          "I've heard things about you, is it true you can save us?",
                                          "Your contributions are so important to our city...",
                                          "I thank you for what you are doing for our city...",
                                          "Hello there.",
                                          "Good day.",
                                          "Nice weather we're having today."};

        // Function to call when interacting
        void OnInteraction()
        {
            
           
                // Implement your interaction logic here
                switch (interactionCount)
                {
                    case (0):
                        _dialogueBox.ShowDialogue(dialogueOpts[Random.Range(0,6)], "Citizen");
                        _animator.SetBool("isWalking", false);
                        _agent.isStopped = true;
                        interactionCount++;
                        break;
                    case (1):
                        _dialogueBox.EndDialogue();
                        interactionCount--;
                        _animator.SetBool("isWalking", true);
                        _agent.isStopped = false;
                        break;
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
            interactionCount = 0;
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
            _animator.SetBool("isWalking", true);
            _agent.isStopped = false;
            interactionCount = 0;
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

            if ((Vector3.Distance(transform.position, waypoints[waypoint].position) <= 2f))
            {
                waypoint = Random.Range(0, 6);
                _agent.SetDestination(waypoints[waypoint].position);
            }
        }

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _animator.SetBool("isWalking", true);
            waypoint = Random.Range(0, 6);
            _agent.SetDestination(waypoints[waypoint].position);
        }
        
    }
}
