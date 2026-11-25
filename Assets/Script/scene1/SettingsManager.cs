using UnityEngine;
using UnityEngine.UI; // Required to talk to Sliders

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    private void Start()
    {
        // When the menu loads, check if the player has saved settings. 
        // If not, default to 1 for volume (100%) and 2 for sensitivity.
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
    }

    // This gets called every time the volume slider is moved
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Instantly changes the global game volume
        PlayerPrefs.SetFloat("MasterVolume", volume); // Saves it
    }

    // This gets called every time the sensitivity slider is moved
    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity); // Saves it for your player script to read later
    }
}