using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public AudioSource audioSource;

    void Update()
    {
        // Check if the player is on the ground AND moving
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            // If the player is moving but the sound IS NOT playing, turn it on!
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If the player stops moving and the sound IS playing, instantly turn it off!
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}