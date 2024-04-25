using UnityEngine;

public class KeySound : MonoBehaviour
{
    public AudioClip sound; // The sound to be played when the E key is pressed
    public AudioClip sound2;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("UniversalCharacter").GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Play the sound if it is assigned
            if (sound != null)
            {
                //AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
                audioSource.PlayOneShot(sound);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Play the sound if it is assigned
            if (sound != null)
            {
                //AudioSource.PlayClipAtPoint(sound2, Camera.main.transform.position);
                audioSource.PlayOneShot(sound2);
            }
        }
    }
}
