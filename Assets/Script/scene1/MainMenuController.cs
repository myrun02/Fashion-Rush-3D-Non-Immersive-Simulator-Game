using UnityEngine;
using UnityEngine.SceneManagement; // Needed to switch scenes


public class MainMenuController : MonoBehaviour
{

    public GameObject settingsPanel;
    // This function will be linked to the Start Button
    public void PlayGame()
    {
        // Replace "SPAWNAREA" with the exact name of your spawn scene if it is different
        SceneManager.LoadScene("SPAWNAREA"); 
    }

    // This function will be linked to the Settings Button
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Debug.Log("Opening Settings Menu...");
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Turns the panel off
    }

    // This function will be linked to the Quit Button
    public void QuitGame()
    {
        Debug.Log("Quitting Game!"); // This prints in the Unity Editor so you know it works
        Application.Quit(); // This actually closes the game when you build and play it
    }
}