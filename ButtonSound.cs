using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound; // The sound to be played when the button is hovered over
    public AudioClip clickSound; // The sound to be played when the button is clicked

    private Button button; // Reference to the button component

    private void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if the hover sound is assigned
        if (hoverSound != null)
        {
            // Play the hover sound
            AudioSource.PlayClipAtPoint(hoverSound, Camera.main.transform.position);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the click sound is assigned
        if (clickSound != null)
        {
            // Play the click sound
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
    }
}
