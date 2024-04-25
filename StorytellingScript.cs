using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorytellingScript : MonoBehaviour
{
    public TMP_Text storyText; // Reference to the TextMeshPro Text component for displaying story text
    public Image storyImage; // Reference to the UI Image component for displaying story images
    public AudioSource clickSound; // Reference to the AudioSource component for playing click sound
    public Button startGameButton; // Reference to the UI Button component for starting the game

    public Sprite[] storyImages; // Array of story images to display
    private string[] storyTexts = {
                                    "In the peaceful city of Aurum, life flowed tranquilly under the watchful gaze of the royal blacksmith family.",
                                    "The father blacksmith was renowned for his craftsmanship. He wielded the power to infuse strength and vitality into others and their weapons.",
                                    "He had a heart for helping others, but his eagerness sometimes led to unwanted assistance.",
                                    "The people of Aurum realized how corrupt he had become and tried to reason with the royal elders to put an end to this.",
                                    "Then out of nowhere, the Titania army suddenly attacked Aurum, seeking to plunder Aurum's resources.",
                                    "In the midst of this chaos, the blacksmith disappeared, leaving his child and his wife defenseless.",
                                    "The Titania pillagers tried to bring harm to the child, wanting to make sure that the blacksmith's bloodline was ended.",
                                    "But the child's mom sacrificed herself to save her son, sealing him away in a place safe from the attack.",
                                    "However, this was not without its consequences, as the child fell into a coma for 10 years.",
                                    "During this time, the royal blacksmith was still gone, his whereabouts unknown. The child, now a young adult, awakens.",
                                    "His heart heavy with grief and resolve burning within. His journey to reclaim Aurum's honor begins.",
                                    "The path ahead is fraught with danger, but your heart remains steadfast. The tempered heart shall rise again, stronger than ever.",
                                    }; // Array of story texts to display

    public float typingSpeed = 0.1f; // Speed at which the text is typed (words per second)

    private int currentIndex = 0; // Index to track the current story page
    private bool isTyping = false; // Flag to indicate if text is currently being typed
    private string currentText = ""; // Current text being displayed
    private Coroutine typingCoroutine; // Reference to the coroutine handling text typing

    void Start()
    {
        // Initialize the story text and image on startup
        UpdateStoryPage(currentIndex);
    }

    void Update()
    {
        // Check for mouse click input
        if (Input.GetMouseButtonDown(0))
        {
            // Play click sound
            if (clickSound != null)
            {
                clickSound.Play();
            }

            // If text is still typing, complete the typing instantly
            if (isTyping)
            {
                CompleteTyping();
            }
            else
            {
                // Increment the current index to move to the next story page
                currentIndex++;

                // Check if the current index is within the bounds of the arrays
                if (currentIndex < storyImages.Length && currentIndex < storyTexts.Length)
                {
                    // Update the story page with the new index
                    UpdateStoryPage(currentIndex);
                }
                else
                {
                    // If the end of the story is reached, enable the start game button
                    startGameButton.gameObject.SetActive(true);
                }
            }
        }
    }

    void UpdateStoryPage(int index)
    {
        // Reset current text and start typing new text
        currentText = "";
        isTyping = true;
        typingCoroutine = StartCoroutine(TypeText(storyTexts[index]));
        
        // Update the story image
        storyImage.sprite = storyImages[index];
    }

    IEnumerator TypeText(string text)
    {
        // Type the text one character at a time
        foreach (char c in text)
        {
            currentText += c;
            storyText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Typing complete
        isTyping = false;
    }

    void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        storyText.text = storyTexts[currentIndex];
        isTyping = false;
    }

    public void StartGame()
    {
        // Change the scene to start the game
        SceneManager.LoadScene(1);
    }
}
