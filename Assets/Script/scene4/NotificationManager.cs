using System.Collections;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    // This makes it a "Singleton" so any script can use it!
    public static NotificationManager Instance;

    [Header("UI References")]
    public CanvasGroup notificationPanel; 
    public TextMeshProUGUI notificationText;

    [Header("Settings")]
    public float displayTime = 2f; // How long it stays on screen
    public float fadeSpeed = 5f;   // How fast it fades in/out

    private Coroutine currentCoroutine;

    private void Awake()
    {
        // Set up the Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Make sure it is completely invisible when the game starts
        if (notificationPanel != null)
        {
            notificationPanel.alpha = 0f;
        }
    }

    // THIS is the function other scripts will call!
    public void ShowNotification(string message)
    {
        notificationText.text = message;

        // If a message is already showing, stop it so we can show the new one immediately
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        // Start the fade animation
        currentCoroutine = StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        // 1. Fade IN
        while (notificationPanel.alpha < 1f)
        {
            notificationPanel.alpha += Time.deltaTime * fadeSpeed;
            yield return null; // Wait a frame
        }

        // 2. Wait for a few seconds so the player can read it
        yield return new WaitForSeconds(displayTime);

        // 3. Fade OUT
        while (notificationPanel.alpha > 0f)
        {
            notificationPanel.alpha -= Time.deltaTime * fadeSpeed;
            yield return null; // Wait a frame
        }
    }
}