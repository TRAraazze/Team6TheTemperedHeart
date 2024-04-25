using UnityEngine;
using UnityEngine.UI;

public class MouseXInversion : MonoBehaviour
{
    public Toggle inversionToggle;

    private bool isInverted = false;

    void Start()
    {
        // Set the initial state of inversion based on the toggle
        isInverted = inversionToggle.isOn;

        // Add a listener to respond to changes in the toggle value
        inversionToggle.onValueChanged.AddListener(ToggleInversion);
    }

    void Update()
    {
        // Get the horizontal mouse input
        float mouseX = Input.GetAxis("Mouse X");

        // Invert the input if necessary
        if (isInverted)
        {
            mouseX *= -1f;
        }

        // Use the inverted or non-inverted input for your movement logic
        // Example: transform.Rotate(Vector3.up, mouseX * sensitivity);
    }

    void ToggleInversion(bool isOn)
    {
        // Update the inversion state when the toggle is switched
        isInverted = isOn;
    }
}
