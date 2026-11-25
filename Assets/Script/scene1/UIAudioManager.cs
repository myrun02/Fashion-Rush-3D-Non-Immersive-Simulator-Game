using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    [Header("Audio Setup")]
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // This function plays the hover sound
    public void PlayHoverSound()
    {
        // PlayOneShot allows multiple sounds to overlap without cutting each other off
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // This function plays the click sound
    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}