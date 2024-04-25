using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityControl : MonoBehaviour
{
    public Dropdown sensitivityDropdown;

    // Define the available sensitivity options
    private float[] sensitivityOptions = { 0.1f, 0.5f, 0.75f, 1f, 1.5f, 2f, 3f, 5f };

    // Define the default sensitivity index (100%)
    private int defaultSensitivityIndex = 3; // Assuming 1.0f (100%) sensitivity is at index 3

    // Start is called before the first frame update
    void Start()
    {
        //sensitivityDropdown = GameObject.Find("Dropdown (Legacy)").GetComponent<Dropdown>();
        // Set the dropdown value to the default sensitivity index
        //sensitivityDropdown.value = defaultSensitivityIndex;

        // Load sensitivity from PlayerPrefs
        LoadSensitivity();

        // Add listener for dropdown value changes
        sensitivityDropdown.onValueChanged.AddListener(OnSensitivityChanged);
    }

    // Method to load sensitivity from PlayerPrefs
    void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            SetMouseSensitivity(sensitivity);
            Debug.Log("Mouse sensitivity loaded: " + sensitivity);
        }
        else
        {
            // If sensitivity is not set in PlayerPrefs, set default sensitivity
            SetMouseSensitivity(sensitivityOptions[defaultSensitivityIndex]);
            Debug.Log("Default mouse sensitivity loaded");
        }
    }

    // Method to set mouse sensitivity based on dropdown selection
    void SetMouseSensitivity(float sensitivity)
    {
        // Save the sensitivity value in PlayerPrefs
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();

        Debug.Log("Mouse sensitivity updated: " + sensitivity);

        // Find the index of the loaded sensitivity value in the sensitivityOptions array
        int index = System.Array.IndexOf(sensitivityOptions, sensitivity);
        // Set the dropdown value to the index of the loaded sensitivity
        sensitivityDropdown.value = index;
    }

    // Callback method for dropdown value changes
    void OnSensitivityChanged(int index)
    {
        // Set mouse sensitivity based on the selected dropdown value
        float sensitivity = sensitivityOptions[index];
        SetMouseSensitivity(sensitivity);
    }
}
