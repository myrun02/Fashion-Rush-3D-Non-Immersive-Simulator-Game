using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckoutSystem : MonoBehaviour
{
    [Header("System Links")]
    public ShoppingCart cartManager; 

    [Header("UI Elements")]
    public GameObject checkoutPanel;    
    public TextMeshProUGUI cartText;    
    public TextMeshProUGUI moneyText;   
    public Button payButton;            

    [Header("Audio Settings")]
    public AudioSource checkoutAudioSource;
    public AudioClip paymentSuccessSound;

    private bool isMenuOpen = false;

    void Start()
    {
        if (checkoutPanel != null) checkoutPanel.SetActive(false);
    }

    void Update()
    {
        if (isMenuOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCheckoutMenu();
        }

        if (isMenuOpen && Input.GetKeyDown(KeyCode.Return))
        {
            if (payButton.interactable) 
            {
                ProcessPayment();
            }
        }
    }

    public void OpenCheckoutMenu()
    {
        isMenuOpen = true;
        checkoutPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateUI();
    }

    public void CloseCheckoutMenu()
    {
        isMenuOpen = false;
        checkoutPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UpdateUI()
    {
        if (cartManager == null)
        {
            Debug.LogError("ShoppingCart is not linked to CheckoutSystem!");
            return;
        }

        if (cartManager.cartItems.Count == 0)
        {
            cartText.text = "Your Cart:\n\n- Cart is empty.";
        }
        else
        {
            string itemsList = "Your Cart:\n\n";
            foreach (ClothItem item in cartManager.cartItems)
            {
                itemsList += $"- {item.itemName} (RM {item.price})\n";
            }
            cartText.text = itemsList;
        }

        int currentMoney = PlayerWallet.totalMoney;
        int totalCost = cartManager.cartTotalCost;

        moneyText.text = $"Total: RM {totalCost} \nYour Money: RM {currentMoney}";

        if (currentMoney >= totalCost && totalCost > 0)
        {
            moneyText.color = Color.white;
            payButton.interactable = true; 
        }
        else if (totalCost == 0)
        {
            moneyText.color = Color.white;
            moneyText.text += "\n<color=yellow>Cart is empty!</color>";
            payButton.interactable = false; 
        }
        else
        {
            moneyText.color = Color.red;
            moneyText.text += "\n<color=red>Not enough money!</color>";
            payButton.interactable = false; 

            if (currentMoney == 0) 
            {
                NotificationManager.Instance.ShowNotification("You have 0 RM!");
            }
        }
    }

    public void ProcessPayment()
    {
        // Play the success sound!
        if (checkoutAudioSource != null && paymentSuccessSound != null)
        {
            checkoutAudioSource.PlayOneShot(paymentSuccessSound);
        }

        cartManager.Checkout();
        NotificationManager.Instance.ShowNotification("Purchase Successful!");
        CloseCheckoutMenu();
    }
}