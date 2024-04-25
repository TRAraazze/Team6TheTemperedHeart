using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CGP;

public class StartGame : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen object

    public int spawnNum;

    public void playGame()
    {
        StartCoroutine(LoadNextSceneWithLoadingScreen());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadNextSceneWithLoadingScreen()
    {

         // Update the quest dialogue state
        for (int i = 0; i < StateManager.questDialogue.Length; i++)
        {
            Debug.Log("Set each quest dialogue to true");
            StateManager.questDialogue[i] = true;
        }
        for (int i = 0; i < StateManager.hasQuest.Length; i++)
        {
            StateManager.hasQuest[i] = true;
        }
        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Load the next scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(3);

        // Wait until the scene is fully loaded
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // Deactivate the loading screen
        loadingScreen.SetActive(false);

       
    }
}
