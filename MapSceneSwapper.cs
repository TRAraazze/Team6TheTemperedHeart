using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class MapSceneSwapper : MonoBehaviour
    {
        public int spawnNum;
        public int sceneNum;
        GameObject playerObject;
        public GameObject mapCanvas; // Reference to the map canvas object
        public Image fadeImage; // Reference to the image used for fading
        public float fadeDuration = 1f; // Duration of the fade effect

        private void Awake()
        {
            // Find the player object
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Show the map when the player enters the collider
                ShowMap();
            }
        }

        // Show the map canvas
        private void ShowMap()
        {
            // Activate the map canvas
            mapCanvas.SetActive(true);

            // Optionally, you can pause the game or lock player controls while the map is open
            //Time.timeScale = 0f; // Pause the game
            // playerObject.GetComponent<PlayerMovement>().enabled = false; // Disable player movement

            // You can also add logic to allow the player to interact with the map (e.g., select destinations)
        }

        // Hide the map canvas
        private void HideMap()
        {
            // Deactivate the map canvas
            mapCanvas.SetActive(false);

            // Optionally, resume the game or unlock player controls
            //Time.timeScale = 1f; // Resume the game
            // playerObject.GetComponent<PlayerMovement>().enabled = true; // Enable player movement
        }

        // Check for player exiting the collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Hide the map when the player exits the collider
                HideMap();
            }
        }
    }
}
