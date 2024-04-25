using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;
    public AudioSource playerVolume;

    private void Start()
    {
        // Initialize volume from PlayerPrefs
        InitializeVolume();
    }

    // Method to initialize volume control
    private void InitializeVolume()
    {
        // Load volume from PlayerPrefs
        float volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        
        // Set volume slider and audio source
        volumeSlider.value = volume;
        audioSource.volume = volume;
        playerVolume.volume = volume;

        // Add listener for volume slider change event
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float volume)
    {
        // Update audio source volume
        audioSource.volume = volume;
        playerVolume.volume = volume;
        // Save volume to PlayerPrefs
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        
        Debug.Log("Volume updated: " + volume);
    }
}
