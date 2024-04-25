using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace CGP
{
    public class SceneSwapper : MonoBehaviour
    {
        public int spawnNum;
        public int sceneNum;
        GameObject playerObject;
        public GameObject loadingScreen; // Reference to the loading screen object
        public Image fadeImage; // Reference to the image used for fading
        public float fadeDuration = 1f; // Duration of the fade effect

        public DialogueBox _dialogueBox;
        private bool isInteractable = false;
        private int interactionCount;
        public CanvasGroup infoTextCanvas;
        public TextMeshProUGUI infoText;
        public GameObject infoPanel;

        private Collider col;

        private void Awake()
        {
            // for some reason without this awake function spawning doesn't work
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        void OnInteraction()
        {
            DisableInteraction();
            isInteractable = false;
            infoText.text = null;
            infoTextCanvas.alpha = 0; //this makes everything transparent
            infoTextCanvas.blocksRaycasts = false; //this prevents the UI element to receive input events

            Destroy(col.gameObject);

            PlayerPrefs.SetInt("spawnNum", spawnNum);
            StateManager.spawn = spawnNum;
            //Debug.Log(spawnNum);

            // Start the scene transition coroutine
            StartCoroutine(SceneTransition(sceneNum));
        }

        // Check for player entering the collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                col = other;
                // Player is inside the collider, enable interaction
                EnableInteraction();
                isInteractable = true;
                infoText.text = "Press E to travel";
                infoTextCanvas.alpha = 1; //this makes everything transparent
                infoTextCanvas.blocksRaycasts = true; //this prevents the UI element to receive input events
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

        // Coroutine for scene transition
        IEnumerator SceneTransition(int sceneNum)
        {
            // Activate the loading screen
            loadingScreen.SetActive(true);

            // Fade out
            yield return StartCoroutine(FadeOut(fadeDuration));

            Debug.Log(spawnNum);

            // Start loading the scene asynchronously
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNum);

            // Wait until the scene is fully loaded
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            // Wait for a specified duration (loadingTime) before deactivating the loading screen
            yield return new WaitForSeconds(10f);

            // Deactivate the loading screen
            loadingScreen.SetActive(false);

            // Fade in
            yield return StartCoroutine(FadeIn(fadeDuration));
        }

        // Coroutine for fading out
        IEnumerator FadeOut(float duration)
        {
            Debug.Log("Fading out");
            float timer = 0f;
            while (timer < duration)
            {
                fadeImage.color = Color.Lerp(Color.clear, Color.black, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            fadeImage.color = Color.black;
        }

        // Coroutine for fading in
        IEnumerator FadeIn(float duration)
        {
            Debug.Log("Fading in");
            float timer = 0f;
            while (timer < duration)
            {
                fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / duration);
                timer += Time.deltaTime;
                yield return null;
            }
            fadeImage.color = Color.clear;
        }
    }
}
