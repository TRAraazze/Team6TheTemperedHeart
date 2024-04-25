using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    public Toggle toggle;
    public string toggleKey = "ToggleState";

    private void Start()
    {
        // Initialize toggle state from PlayerPrefs
        InitializeToggle();
    }

    // Method to initialize toggle state
    private void InitializeToggle()
    {
        // Load toggle state from PlayerPrefs
        bool toggleState = PlayerPrefs.GetInt(toggleKey, 0) == 1;

        // Set toggle state
        toggle.isOn = toggleState;

        // Add listener for toggle change event
        toggle.onValueChanged.AddListener(UpdateToggle);
    }

    private void UpdateToggle(bool toggleState)
    {
        // Save toggle state to PlayerPrefs
        PlayerPrefs.SetInt(toggleKey, toggleState ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Toggle state updated: " + toggleState);
    }
}
