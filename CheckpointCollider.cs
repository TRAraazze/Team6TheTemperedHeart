using UnityEngine;
using TMPro;

namespace CGP
{
    public class CheckpointCollider : MonoBehaviour
    {
        private bool isNearCheckpoint = false;
        private bool isSaving = false;
        private int currentSaveSlot; // Default save slot
        public TextMeshProUGUI infoText;
        public CanvasGroup infoTextCanvas;
        public int checkpointNum;

        void Start()
        {
            currentSaveSlot = PlayerPrefs.GetInt("CurrentSaveSlot", 0);
        }

        // Update is called once per frame
        void Update()
        {
            // Check for 'Tab' key press while player is near the checkpoint
            if (Input.GetKeyDown(KeyCode.Tab) && isNearCheckpoint && !isSaving)
            {
                SaveGame();
            }
        }

        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player is inside the collider, enable interaction
                isNearCheckpoint = true;
                infoText.text = "Press 'Tab' to Save Game";
                infoTextCanvas.alpha = 1; // Show the info text
            }
        }

        // Check for player exiting the collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Player has exited the collider, disable interaction
                isNearCheckpoint = false;
                infoText.text = ""; // Clear the info text
                infoTextCanvas.alpha = 0; // Hide the info text
            }
        }

        // Save the game
        void SaveGame()
        {
            isSaving = true;
            PlayerPrefs.SetInt("spawnNum", checkpointNum);
            SaveLoadManager.SaveGame(currentSaveSlot); // Save to the current save slot
            //Debug.Log("checkpoint num: " + checkpointNum);
            
            //Debug.Log("player prefs spawn num: " + PlayerPrefs.GetInt("spawnNum"));
            infoText.text = "Game Saved";
            infoTextCanvas.alpha = 1; // Show the info text
            Invoke("HideSaveMessage", 5f); // Hide the save message after 5 seconds
        }

        // Hide the save message
        void HideSaveMessage()
        {
            infoText.text = ""; // Clear the info text
            infoTextCanvas.alpha = 0; // Hide the info text
            isSaving = false;
        }
    }
}
