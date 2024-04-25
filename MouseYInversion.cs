using UnityEngine;
using UnityEngine.UI;

public class MouseYInversion : MonoBehaviour
{
    public Toggle yInversionToggle;

    private bool isInvertedY = false;

    void Start()
    {
        // Set the initial state of Y-axis inversion based on the toggle
        isInvertedY = yInversionToggle.isOn;

        // Add a listener to respond to changes in the toggle value
        yInversionToggle.onValueChanged.AddListener(ToggleYInversion);
    }

    void Update()
    {
        // Get the mouse input
        float mouseY = Input.GetAxis("Mouse Y");

        // Invert the Y-axis input if necessary
        if (isInvertedY)
        {
            mouseY *= -1f;
        }

        // Use the inverted or non-inverted input for your movement logic
        // Example: transform.Rotate(Vector3.right, mouseY * sensitivity);
    }

    void ToggleYInversion(bool isOn)
    {
        // Update the Y-axis inversion state when the toggle is switched
        isInvertedY = isOn;
    }
}
