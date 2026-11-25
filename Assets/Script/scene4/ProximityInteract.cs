using UnityEngine;

public class ProximityInteract : MonoBehaviour
{
    private ClothItem nearbyCloth = null;

    [Header("UI Settings")]
    [Tooltip("Drag your Text_InteractPrompt object here")]
    public GameObject interactPromptUI; 

    void Start()
    {
        // Ensure the text is hidden as soon as the game starts
        if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        // If we are near a shirt AND the player presses the 'F' key
        if (nearbyCloth != null && Input.GetKeyDown(KeyCode.F))
        {
            nearbyCloth.OnPlayerClick();
            
            // Optional: Hide the "Press F" text while the hologram is open
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ClothItem shirt = other.GetComponent<ClothItem>();
        if (shirt != null)
        {
            nearbyCloth = shirt;
            
            // Show the "Press F" text when we get close
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ClothItem shirt = other.GetComponent<ClothItem>();
        if (shirt != null && shirt == nearbyCloth)
        {
            nearbyCloth = null; // Forget the shirt
            
            // Hide the text when we walk away
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(false);
            }
        }
    }
}