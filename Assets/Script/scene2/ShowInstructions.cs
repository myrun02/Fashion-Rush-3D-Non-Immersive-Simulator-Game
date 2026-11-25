using UnityEngine;

public class ShowInstructions : MonoBehaviour
{
    [Tooltip("Drag your World Space Canvas here in the Inspector")]
    public GameObject instructionCanvas;

    private void Start()
    {
        // Make sure the text is hidden when the game first starts
        if (instructionCanvas != null)
        {
            instructionCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object stepping on the pedestal is the Player
        if (other.CompareTag("Player"))
        {
            if (instructionCanvas != null)
            {
                instructionCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the text when the player walks away
        if (other.CompareTag("Player"))
        {
            if (instructionCanvas != null)
            {
                instructionCanvas.SetActive(false);
            }
        }
    }
}