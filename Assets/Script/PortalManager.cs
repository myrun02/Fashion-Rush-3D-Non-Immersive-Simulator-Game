using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PortalManager : MonoBehaviour
{
    [Header("Teleport Settings")]
    public string sceneToLoad = "Cloth_Store";
    public bool isUnlocked = false;

    [Header("Money Settings")]
    public int currentMoney = 0;
    public int moneyRequired = 100;

    [Header("Visuals")]
    public MeshRenderer portalPlane;
    public Material lockedMaterial;
    public Material unlockedMaterial;

    [Header("Loading UI")]
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI progressText;
    
    [Tooltip("The minimum amount of seconds the loading screen will stay on screen.")]
    public float minimumLoadTime = 3f;

    [Header("Audio Settings")]
    public AudioSource portalAudioSource;
    public AudioClip teleportSound;

    void Start()
    {
        if (portalPlane != null)
        {
            if (isUnlocked)
            {
                portalPlane.material = unlockedMaterial;
            }
            else
            {
                portalPlane.material = lockedMaterial;
            }
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        Debug.Log("Money: " + currentMoney + " / " + moneyRequired);

        if (currentMoney >= moneyRequired && !isUnlocked)
        {
            UnlockPortal();
        }
    }

    void UnlockPortal()
    {
        isUnlocked = true;
        if (portalPlane != null)
        {
            portalPlane.material = unlockedMaterial;
        }
        Debug.Log("Portal Unlocked! Step through.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUnlocked && other.CompareTag("Player"))
        {
            // Play the teleport sound right before we start loading!
            if (portalAudioSource != null && teleportSound != null)
            {
                portalAudioSource.PlayOneShot(teleportSound);
            }

            StartCoroutine(LoadSceneAsync());
        }
    }

    IEnumerator LoadSceneAsync()
    {
        if (loadingScreen != null) loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; 

        float elapsedTime = 0f;

        while (elapsedTime < minimumLoadTime || operation.progress < 0.9f)
        {
            elapsedTime += Time.deltaTime;

            float timeProgress = elapsedTime / minimumLoadTime;
            float loadProgress = operation.progress / 0.9f;

            float displayProgress = Mathf.Min(timeProgress, loadProgress);

            if (loadingBar != null)
            {
                loadingBar.value = displayProgress;
            }
            if (progressText != null)
            {
                progressText.text = Mathf.RoundToInt(displayProgress * 100f) + "%";
            }

            if (elapsedTime >= minimumLoadTime && operation.progress >= 0.9f)
            {
                if (loadingBar != null) loadingBar.value = 1f;
                if (progressText != null) progressText.text = "100%";
                
                yield return new WaitForSeconds(0.2f); 
                
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}