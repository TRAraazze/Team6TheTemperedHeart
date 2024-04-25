using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CGP
{
    public static class SaveLoadManager
    {

        public static void SaveGame(int fileNumber)
        {
            string filePath = Application.persistentDataPath + "/SaveGameFile" + fileNumber + ".dat";
            //Debug.Log(filePath);
            //Debug.Log("Insave game");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            // Serialize game data
            GameData data = new GameData();
            data.questDialogue = StateManager.questDialogue;
            data.questComplete = StateManager.questComplete;
            data.questProgress = StateManager.questProgress;
            data.hasQuest = StateManager.hasQuest;
            data.isFireUnlocked = StateManager.isFireUnlocked;
            data.isAirUnlocked = StateManager.isAirUnlocked;
            data.isIceUnlocked = StateManager.isIceUnlocked;
            data.currency = StateManager.currency;
            data.karma = StateManager.karma;
            data.inventory = StateManager.inventory;
            //data.spawn = StateManager.spawn;
            data.spawn = PlayerPrefs.GetInt("spawnNum");

            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            Debug.Log(sceneName);

            // Store the scene name in the game data
            data.sceneName = sceneName;

            formatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public static void LoadGame(int fileNumber)
        {
            string filePath = Application.persistentDataPath + "/SaveGameFile" + fileNumber + ".dat";
            if (File.Exists(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                GameData data = formatter.Deserialize(fileStream) as GameData;
                fileStream.Close();

                // Deserialize and apply game data
                StateManager.questDialogue = data.questDialogue;
                StateManager.questComplete = data.questComplete;
                StateManager.questProgress = data.questProgress;
                StateManager.hasQuest = data.hasQuest;
                StateManager.currency = data.currency;
                StateManager.isFireUnlocked = data.isFireUnlocked;
                StateManager.isAirUnlocked = data.isAirUnlocked;
                StateManager.isIceUnlocked = data.isIceUnlocked;
                StateManager.karma = data.karma;
                StateManager.inventory = data.inventory;
                StateManager.spawn = data.spawn;

                PlayerPrefs.SetInt("spawnNum", StateManager.spawn);
                Debug.Log("spawn num was set to from file: " + StateManager.spawn);

                UnityEngine.SceneManagement.SceneManager.LoadScene(data.sceneName);
            }
            else
            {
                Debug.LogError("No saved game found at file " + fileNumber);
            }
        }
    }
}