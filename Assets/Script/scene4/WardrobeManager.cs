using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    [Header("Wardrobe Inventory")]
    public GameObject[] availableSuits;

    [Header("Audio Settings")]
    public AudioSource mannequinAudioSource;
    public AudioClip clothChangeSound;

    private int currentSuitIndex = -1;

    public void EquipSuit(int suitIndex)
    {
        for (int i = 0; i < availableSuits.Length; i++)
        {
            availableSuits[i].SetActive(false);
        }

        if (suitIndex >= 0 && suitIndex < availableSuits.Length)
        {
            availableSuits[suitIndex].SetActive(true);
        }
    }

    public void CycleNextSuit()
    {
        if (availableSuits.Length == 0) return; 

        // Play the cloth change sound!
        if (mannequinAudioSource != null && clothChangeSound != null)
        {
            mannequinAudioSource.PlayOneShot(clothChangeSound);
        }

        currentSuitIndex++; 

        if (currentSuitIndex >= availableSuits.Length)
        {
            currentSuitIndex = 0;
        }

        EquipSuit(currentSuitIndex);
    }

    public void RemoveAllSuits()
    {
        for (int i = 0; i < availableSuits.Length; i++)
        {
            availableSuits[i].SetActive(false);
        }
        currentSuitIndex = -1; 
    }
}