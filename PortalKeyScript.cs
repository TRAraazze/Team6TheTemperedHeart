using System.Data.Common;
using CGP;
using TMPro;


namespace CGP
{
    using UnityEngine;
    
    
    public class PortalKeyScript : MonoBehaviour
    {
        private bool isInteractable = false;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;
        public CanvasGroup infoTextCanvas;
        public GameObject portalEffect;
        public GameObject stone1, stone2, stone3;
        public GameObject portalSwap;

        public int questNum;
        // Function to call when interacting
        void OnInteraction()
        {
            // Implement your interaction logic here
            if (StateManager.inventory[11] > 2)
            {
                portalEffect.SetActive(true);
                stone1.SetActive(true);
                stone2.SetActive(true);
                stone3.SetActive(true);
                portalSwap.SetActive(true);
                StateManager.questComplete[18] = true;
                StateManager.questDialogue[19] = false;

            }
        }

        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (StateManager.inventory[11] > 2)
            {
                if (other.CompareTag("Player"))
                {
                    // Player is inside the collider, enable interaction
                    EnableInteraction();
                    isInteractable = true;
                    infoText.text = "Press E to Open Portal";
                    infoTextCanvas.alpha = 1; //this makes everything transparent
                    infoTextCanvas.blocksRaycasts = true; //this prevents the UI element to receive input events
                }
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

        void Start()
        {
            infoTextCanvas.alpha = 0; //this makes everything transparent
            infoTextCanvas.blocksRaycasts = false; //this prevents the UI element to receive input events
        }
    }
}
