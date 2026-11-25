using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class ShoppingCart : MonoBehaviour
{
    public List<ClothItem> cartItems = new List<ClothItem>();
    public int cartTotalCost = 0;

    [Header("Dynamic Cart UI")]
    public GameObject cartUIPanel; 
    public TextMeshProUGUI totalText; 
    
    [Header("List Spawning")]
    public GameObject itemRowPrefab; 
    public Transform contentContainer; 

    [Header("Audio Settings")]
    public AudioSource cartAudioSource;
    public AudioClip addToCartSound;
    public AudioClip removeFromCartSound;

    private bool isCartOpen = false;

    void Start()
    {
        if (cartUIPanel != null) cartUIPanel.SetActive(false);
    }

    public void AddToCart(ClothItem item)
    {
        cartItems.Add(item);
        cartTotalCost += item.price;
        
        // Play the Add sound!
        if (cartAudioSource != null && addToCartSound != null)
        {
            cartAudioSource.PlayOneShot(addToCartSound);
        }

        Debug.Log(item.itemName + " added to cart! New Total: RM " + cartTotalCost);

        if (isCartOpen) UpdateCartDisplay();

        NotificationManager.Instance.ShowNotification(item.itemName + " added to cart!");
    }

    public void RemoveFromCart(ClothItem item)
    {
        if (cartItems.Contains(item))
        {
            cartItems.Remove(item);
            cartTotalCost -= item.price;
            
            // Play the Remove sound!
            if (cartAudioSource != null && removeFromCartSound != null)
            {
                cartAudioSource.PlayOneShot(removeFromCartSound);
            }

            Debug.Log(item.itemName + " removed! New Total: RM " + cartTotalCost);

            NotificationManager.Instance.ShowNotification(item.itemName + " Item remove from cart!");
            
            UpdateCartDisplay(); 
        }
    }

    public void ToggleCartUI()
    {
        isCartOpen = !isCartOpen; 
        if (cartUIPanel != null) cartUIPanel.SetActive(isCartOpen);

        if (isCartOpen)
        {
            UpdateCartDisplay();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void UpdateCartDisplay()
    {
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (ClothItem item in cartItems)
        {
            GameObject newRow = Instantiate(itemRowPrefab, contentContainer);

            TextMeshProUGUI[] textBoxes = newRow.GetComponentsInChildren<TextMeshProUGUI>();
            textBoxes[0].text = item.itemName;
            textBoxes[1].text = "RM " + item.price;

            Button removeButton = newRow.GetComponentInChildren<Button>();
            
            if (removeButton != null)
            {
                ClothItem itemToRemove = item; 
                removeButton.onClick.AddListener(() => RemoveFromCart(itemToRemove));
            }
        }

        if (totalText != null)
        {
            totalText.text = "Total: RM " + cartTotalCost;
        }
    }

    public void Checkout()
    {
        if (PlayerWallet.totalMoney >= cartTotalCost)
        {
            PlayerWallet.totalMoney -= cartTotalCost;
            cartItems.Clear();
            cartTotalCost = 0;
            
            PlayerWallet walletScript = FindAnyObjectByType<PlayerWallet>();
            if (walletScript != null) walletScript.UpdateMoneyUI(); 

            if (isCartOpen) UpdateCartDisplay(); 
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}