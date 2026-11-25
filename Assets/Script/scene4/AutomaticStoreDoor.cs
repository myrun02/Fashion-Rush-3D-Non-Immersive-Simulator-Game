using UnityEngine;

public class AutomaticStoreDoor : MonoBehaviour
{
    public Transform leftHinge;
    public Transform rightHinge;
    
    public float openAngle = 90f; 
    public float openSpeed = 5f;  
    
    private bool isPlayerNear = false;
    private Quaternion leftClosedRotation;
    private Quaternion rightClosedRotation;
    
    // We add a multiplier to flip the swing direction (1 or -1)
    private float directionMultiplier = 1f;

    void Start()
    {
        leftClosedRotation = leftHinge.localRotation;
        rightClosedRotation = rightHinge.localRotation;
    }

    void Update()
    {
        // Multiply the angle by our direction multiplier
        float currentAngle = openAngle * directionMultiplier;
        
        Quaternion leftTarget = isPlayerNear ? leftClosedRotation * Quaternion.Euler(0, currentAngle, 0) : leftClosedRotation;
        Quaternion rightTarget = isPlayerNear ? rightClosedRotation * Quaternion.Euler(0, -currentAngle, 0) : rightClosedRotation;
        
        leftHinge.localRotation = Quaternion.Lerp(leftHinge.localRotation, leftTarget, Time.deltaTime * openSpeed);
        rightHinge.localRotation = Quaternion.Lerp(rightHinge.localRotation, rightTarget, Time.deltaTime * openSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            
            // Draw a line from the center of the doorway to the player
            Vector3 directionToPlayer = other.transform.position - transform.position;
            
            // Check if that line is pointing in the same direction as the sensor's "forward" direction
            float side = Vector3.Dot(transform.forward, directionToPlayer);
            
            // Flip the swing direction depending on which side they are on
            if (side > 0) 
            {
                directionMultiplier = 1f; // Swing outward
            } 
            else 
            {
                directionMultiplier = -1f; // Swing inward
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}