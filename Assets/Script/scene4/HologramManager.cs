using UnityEngine;
using TMPro;

public class HologramManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject uiPanel; 
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI descText;

    [Header("References")]
    // Drag your Shopping Cart manager here in the Inspector!
    public ShoppingCart playerCart; 

    private ClothItem currentlySelectedItem; 
    private bool isHologramOpen = false; 
    
    // Caching the camera improves performance
    private Camera mainCam; 

    void Start()
    {
        uiPanel.SetActive(false);
        mainCam = Camera.main;

        // If you forget to drag the ShoppingCart into the Inspector, 
        // the script will try to find it automatically for you as a backup.
        if (playerCart == null)
        {
            playerCart = FindAnyObjectByType<ShoppingCart>();
        }
    }

    void Update()
    {
        // PC Keyboard Inputs (Great for testing in the Unity Editor)
        if (isHologramOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddCurrentItemToCart();
            }

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseHologram();
            }
        }
    }

    public void ShowHologram(ClothItem item)
    {
        currentlySelectedItem = item;
        nameText.text = item.itemName;
        priceText.text = "RM " + item.price;
        descText.text = item.description;

        // Position it perfectly using the true center
        Collider clothCollider = item.GetComponent<Collider>();
        Vector3 trueCenter = clothCollider != null ? clothCollider.bounds.center : item.transform.position;
        
        // Use the cached camera instead of Camera.main
        Vector3 directionToCamera = (mainCam.transform.position - trueCenter).normalized;
        
        transform.position = trueCenter + new Vector3(0, 0.3f, 0) + (directionToCamera * 0.4f);
        transform.LookAt(mainCam.transform);
        transform.Rotate(0, 180, 0); 

        uiPanel.SetActive(true);
        isHologramOpen = true; 

        // UNLOCK the mouse so they can click the Hologram buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Because these are "public", you can link them directly to mobile UI Buttons!
    public void CloseHologram()
    {
        uiPanel.SetActive(false);
        currentlySelectedItem = null;
        isHologramOpen = false; 

        // LOCK the mouse again to return to walking around
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddCurrentItemToCart()
    {
        if (currentlySelectedItem != null && playerCart != null)
        {
            playerCart.AddToCart(currentlySelectedItem);
            CloseHologram(); 
        }
    }
}