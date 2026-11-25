using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // This creates a static reference to track if a music player already exists
    private static BackgroundMusic instance;

    void Awake()
    {
        // Check if there is already a music player in the game
        if (instance == null)
        {
            // If not, make this the official music player
            instance = this;
            
            // Tell Unity NEVER to destroy this object when loading a new scene
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // If a music player already exists, destroy this duplicate immediately!
            // (This prevents two songs from playing over each other when you go back to the Main Menu)
            Destroy(gameObject); 
        }
    }
}