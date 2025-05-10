using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeModeManager : MonoBehaviour
{
    public MinigameData[] minigameData; // Assign from inspector or through code
    private GameObject miniGameInstance;
    public GameObject menuFreeMode;
    public GameObject menuCamera;
    public float minigameDuration = 30f;

    public Leaderboard resultsScreen;
    public void PlayBallonMiniGame()
    {
        menuCamera.SetActive(false);
        menuFreeMode.SetActive(false);
        PlaySelectedMiniGame(minigameData[0]);
    }

    public void PlayFlagsMinigame()
    {
        menuCamera.SetActive(false);
        menuFreeMode.SetActive(false);
        PlaySelectedMiniGame(minigameData[1]);
    }

    public void PlayBoxingMinigame()
    {
        menuCamera.SetActive(false);
        menuFreeMode.SetActive(false);
        PlaySelectedMiniGame(minigameData[2]);
    }

    public void PlaySimonMinigame()
    {
        menuCamera.SetActive(false);
        menuFreeMode.SetActive(false);
        PlaySelectedMiniGame(minigameData[3]);
    }
    
    public void PlaySelectedMiniGame(MinigameData selectedgame)
    {
        if (selectedgame != null && selectedgame.MiniGamePrefab != null)
        {
            // Destroy any existing minigame instance
            if (miniGameInstance != null)
            {
                Destroy(miniGameInstance);
                Debug.Log("Previous minigame instance destroyed.");
            }

            // Instantiate the selected minigame
            miniGameInstance = Instantiate(selectedgame.MiniGamePrefab);
            Debug.Log("Minijuego cargado: " + selectedgame.MinigameID);

            // Start a 30-second timer to destroy this instance and load the next one
            StartCoroutine(MinigameTimer(minigameDuration)); // Pass a duration of 30 seconds
        }
        else
        {
            Debug.LogWarning("Datos del minijuego incompletos");
        }
    }
    
    IEnumerator MinigameTimer(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Destroy the current minigame instance
        if (miniGameInstance != null)
        {
            Destroy(miniGameInstance);
            miniGameInstance = null;
            Debug.Log("Minigame instance destroyed after " + duration + " seconds.");
        }
        
        resultsScreen.Show(true);
    }
    
}
