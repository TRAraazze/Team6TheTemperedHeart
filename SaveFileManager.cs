using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace CGP
{
    public class SaveFileManager : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Button[] saveFileButtons;
        public StartGame startGame;

        private int currentSaveSlot;
        private bool[] saveFilesInitialized = new bool[3];
        private string[] saveFileNames = new string[3];

        private void Start()
        {
            currentSaveSlot = PlayerPrefs.GetInt("CurrentSaveSlot", 0);
            LoadSaveFileNames();
            UpdateSaveFileButtonsText();
        }

        public void OnNewFileButtonClick(int saveFileIndex)
        {
            if (!saveFilesInitialized[saveFileIndex])
            {
                OnFileNameEntered(saveFileIndex);
            }
            else
            {
                // Show loading screen
                loadingScreen.SetActive(true);
                // Load the existing save file
                StartCoroutine(LoadSaveFile(saveFileIndex));
            }
        }

        private void OnFileNameEntered(int saveFileIndex)
        {
            // Save the game data with the entered file name
            saveFilesInitialized[saveFileIndex] = true;
            UpdateSaveFileButtonsText();

            // Show loading screen
            loadingScreen.SetActive(true);
            // Load new game
            StartCoroutine(LoadNewGame(saveFileIndex));
        }

        private IEnumerator LoadSaveFile(int saveFileIndex)
        {
            string fileName = saveFileNames[saveFileIndex];

            // Load the game data with the selected file name
            yield return new WaitForSeconds(5f); // Simulate loading delay
            SaveLoadManager.LoadGame(saveFileIndex);

        }

        private IEnumerator LoadNewGame(int saveFileIndex)
        {
            Debug.Log("New game created");
            // Load the initial game scene or perform other necessary actions
            startGame.playGame();
            
            currentSaveSlot = saveFileIndex;
            PlayerPrefs.SetInt("CurrentSaveSlot", currentSaveSlot);
            PlayerPrefs.SetInt("spawnNum", 0);

            yield return new WaitForSeconds(5f); // Simulate loading delay
            // Hide loading screen after loading is complete
            loadingScreen.SetActive(false);
        }

        private void LoadSaveFileNames()
        {
            for (int i = 0; i < 3; i++)
            {
                string filePath = Application.persistentDataPath + "/SaveGameFile" + i + ".dat";

                if (System.IO.File.Exists(filePath))
                {
                    saveFileNames[i] = "Save File " + (i + 1);
                    saveFilesInitialized[i] = true;
                }
                else
                {
                    saveFileNames[i] = "New File";
                }
            }
        }

        private void UpdateSaveFileButtonsText()
        {
            for (int i = 0; i < 3; i++)
            {
                saveFileButtons[i].GetComponentInChildren<Text>().text = saveFileNames[i];
            }
        }
    }
}
