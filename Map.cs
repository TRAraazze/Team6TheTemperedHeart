using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using CGP;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen object
    public int spawnNum;

    public void SwitchToFarmScene()
    {
        StartCoroutine(LoadSceneWithLoadingScreen(4));
    }

    public void SwitchToSnowScene()
    {
        StartCoroutine(LoadSceneWithLoadingScreen(7));
    }

    public void SwitchToAurumScene()
    {
        StartCoroutine(LoadSceneWithLoadingScreen(1));
    }

    public void SwitchToForrestScene()
    {
        StartCoroutine(LoadSceneWithLoadingScreen(4));
    }

    public void SwitchToMountainScene()
    {
        StartCoroutine(LoadSceneWithLoadingScreen(10));
    }

    IEnumerator LoadSceneWithLoadingScreen(int sceneNum)
    {
        PlayerPrefs.SetInt("spawnNum", spawnNum);
        StateManager.spawn = spawnNum;

        // Activate the loading screen
        loadingScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNum);

        // Wait until the scene is fully loaded
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // Deactivate the loading screen
        loadingScreen.SetActive(false);
    }
}
