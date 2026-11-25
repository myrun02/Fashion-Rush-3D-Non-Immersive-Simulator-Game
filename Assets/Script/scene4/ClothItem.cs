using UnityEngine;

public class ClothItem : MonoBehaviour
{
    [Header("Cloth Details")]
    public string itemName = "Basic T-Shirt";
    public int price = 59;
    [TextArea]
    public string description = "A comfortable cotton shirt.";

    // Your VR interaction system (like a Raycast laser or grab) 
    // should be set up to call this specific function when triggered!
    public void OnPlayerClick()
    {
        // Find the hologram manager in the scene and tell it to show this item's info
        HologramManager hologram = FindAnyObjectByType<HologramManager>();
        if (hologram != null)
        {
            hologram.ShowHologram(this);
        }
    }
}