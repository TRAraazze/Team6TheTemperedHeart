using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Display the loading screen
        gameObject.SetActive(true);

        // Start loading the target scene asynchronously
        SceneManager.LoadSceneAsync(sceneName);
    }
}
