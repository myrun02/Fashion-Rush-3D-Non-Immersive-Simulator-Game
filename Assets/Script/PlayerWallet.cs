using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PlayerWallet : MonoBehaviour
{
    public static int totalMoney = 0;
    public TextMeshProUGUI moneyDisplay; // Drag your Text object here

    // 1. Added Start() to load the money when the player enters a scene
    void Start()
    {
        UpdateMoneyUI(); // Update the screen immediately upon loading in
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        
        // 2. Save the new total immediately so it's ready for the scene change
        PlayerPrefs.SetInt("SavedMoney", totalMoney);
        PlayerPrefs.Save();

        UpdateMoneyUI();

        // Send the money data to the Portal to see if we unlock the door
        PortalManager portal = FindAnyObjectByType<PortalManager>();
        if (portal != null)
        {
            // Note: We use the exact amount picked up so the portal can add it to its own counter
            portal.AddMoney(amount); 
        }
    }

    // 3. Moved the UI update to a separate helper method to keep things clean
    // since we now need to call it from both Start() and AddMoney()
    public void UpdateMoneyUI()
    {
        // Update the floating VR screen
        if (moneyDisplay != null)
        {
            moneyDisplay.text = "RM " + totalMoney.ToString();
        }
    }
}