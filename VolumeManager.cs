using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    private const string VolumeKey = "Volume";

    private void Awake()
    {
        // Load the volume setting from PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        ApplyVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        // Save the volume setting to PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
        
        // Apply the volume setting
        ApplyVolume(volume);
    }

    private void ApplyVolume(float volume)
    {
        // Apply the volume setting to the AudioListener or other audio sources
        AudioListener.volume = volume;
    }
}
