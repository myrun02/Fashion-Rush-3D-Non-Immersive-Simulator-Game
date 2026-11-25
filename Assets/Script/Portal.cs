using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Teleport Settings")]
    public string sceneToLoad = "Cloth_Store";
    public bool isUnlocked = false;

    [Header("Money Settings")]
    public int currentMoney = 0;
    public int moneyRequired = 100;

    [Header("Visuals")]
    public MeshRenderer portalPlane; // The blue plane mesh
    public Material lockedMaterial;
    public Material unlockedMaterial; // The glowing store picture

    void Start()
    {
        // Ensure the door starts looking locked
        if (portalPlane != null)
        {
            portalPlane.material = lockedMaterial;
        }
    }

    // Your coin-collection script will need to call this function!
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("Money: " + currentMoney + " / " + moneyRequired);

        // Check if we hit the target and the door isn't already unlocked
        if (currentMoney >= moneyRequired && !isUnlocked)
        {
            UnlockPortal();
        }
    }

    void UnlockPortal()
    {
        isUnlocked = true;

        // Swap the material to the glowing picture
        if (portalPlane != null)
        {
            portalPlane.material = unlockedMaterial;
        }

        Debug.Log("Portal Unlocked! Step through.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only teleport if the door is unlocked AND it's the Player
        if (isUnlocked && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}