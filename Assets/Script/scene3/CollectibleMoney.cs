using UnityEngine;

public class CollectibleMoney : MonoBehaviour
{
    [Tooltip("Set this to 5, 10, or 50 in the Inspector!")]
    public int noteValue;

    [Header("Audio")]
    public AudioClip collectSound; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the money is the Player
        if (other.CompareTag("Player"))
        {
            // 1. Play the collection sound exactly where the money is
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // 2. Look for the PlayerWallet script on the player
            PlayerWallet wallet = other.GetComponentInParent<PlayerWallet>();
            
            if (wallet != null)
            {
                wallet.AddMoney(noteValue); // Add the money to your UI/Wallet
                Destroy(gameObject);        // Make the note disappear
            }
            else
            {
                Debug.LogWarning("Could not find the PlayerWallet on the Player!");
            }
        }
    }
}