using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CGP
{
    public class PlayerDeath : MonoBehaviour
    {
        public GameObject loadingScreen; // Reference to the loading screen object
        public float loadingDelay = 5f; // Delay before reloading the game (optional)

        public void Die()
        {
            // Activate the loading screen
            if (loadingScreen != null)
            {
                loadingScreen.SetActive(true);
            }

            // Call the LoadGame method from the SaveLoadManager to reload the game
            StartCoroutine(ReloadGame());
        }

        private IEnumerator ReloadGame()
        {
            // Optional delay before reloading the game
            yield return new WaitForSeconds(loadingDelay);

            // Load the game using SaveLoadManager
            SaveLoadManager.LoadGame(PlayerPrefs.GetInt("CurrentSaveSlot", 0)); 
        }
    }
}
