using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmartInteract : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f; 
    public GameObject floatingPromptUI; 
    public Transform playerCamera; 

    [Header("Crosshair Settings")]
    public Image crosshairImage; 
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f); 
    public Color hoverColor = Color.red; 

    [Header("Global Menus")]
    public ShoppingCart cartManager; 
    public GameObject pauseMenuUI; 

    [Header("Audio Settings")]
    public AudioSource interactAudioSource;
    public AudioClip clothRustleSound; // Sound for shirts and mannequins
    public AudioClip cashRegisterSound; // Sound for the checkout counter

    private ClothItem currentTarget = null;
    private bool isMenuOpen = false;

    void Start()
    {
        if (floatingPromptUI != null) floatingPromptUI.SetActive(false);
        if (crosshairImage != null) crosshairImage.color = normalColor;
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false); 
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Press 'C' to toggle the Cart
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (cartManager != null)
            {
                cartManager.ToggleCartUI();
            }
        }

        // 2. Press 'Escape' to toggle the Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked || isMenuOpen)
            {
                ToggleMenu();
            }
        }

        // --- RAYCAST INTERACTION ---
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            ClothItem shirt = hit.collider.GetComponent<ClothItem>();
            CheckoutSystem register = hit.collider.GetComponent<CheckoutSystem>(); 
            WardrobeManager mannequin = hit.collider.GetComponent<WardrobeManager>();

            // SCENARIO A: Player is looking at clothes
            if (shirt != null)
            {
                currentTarget = shirt;
                if (crosshairImage != null) crosshairImage.color = hoverColor;

                floatingPromptUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Play cloth sound!
                    if (interactAudioSource != null && clothRustleSound != null)
                        interactAudioSource.PlayOneShot(clothRustleSound);

                    shirt.OnPlayerClick();
                    floatingPromptUI.SetActive(false); 
                }
            }
            // SCENARIO B: Player is looking at the cash register
            else if (register != null) 
            {
                currentTarget = null; 
                if (crosshairImage != null) crosshairImage.color = hoverColor;

                floatingPromptUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Play register sound!
                    if (interactAudioSource != null && cashRegisterSound != null)
                        interactAudioSource.PlayOneShot(cashRegisterSound);

                    register.OpenCheckoutMenu(); 
                    floatingPromptUI.SetActive(false); 
                }
            }
            // SCENARIO C: Player is looking at a Mannequin
            else if (mannequin != null)
            {
                currentTarget = null; 
                if (crosshairImage != null) crosshairImage.color = hoverColor;

                floatingPromptUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Play cloth sound!
                    if (interactAudioSource != null && clothRustleSound != null)
                        interactAudioSource.PlayOneShot(clothRustleSound);

                    mannequin.CycleNextSuit(); 
                }
            }
            else
            {
                ClearTarget(); 
            }
        }
        else 
        {
            ClearTarget(); 
        }
    }

    void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
        }
        
        if (floatingPromptUI != null) floatingPromptUI.SetActive(false);
        if (crosshairImage != null) crosshairImage.color = normalColor;
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isMenuOpen);
        }

        if (isMenuOpen)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // ==========================================
    // MENU BUTTON FUNCTIONS
    // ==========================================

    public void ResumeGame()
    {
        if (isMenuOpen)
        {
            ToggleMenu(); 
        }
    }

    public void ReturnToEnvironment()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Environment1"); 
    }

    public void ReturnToEnvironment2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FashionRush"); 
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SPAWNAREA");
    }
}