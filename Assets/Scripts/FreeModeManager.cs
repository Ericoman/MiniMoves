using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FreeModeManager : MonoBehaviour
{
    public MinigameData[] minigameData; // Assign from inspector or through code
    private GameObject miniGameInstance;

    public GameObject menuCamera;
    public float minigameDuration = 30f;

    public Canvas gameCanvas;
    
    public UIManager uiManager;
    public Leaderboard resultsScreen;

    public static FreeModeManager _instance;
    public static FreeModeManager Instance => _instance;
    
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
    }

    private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            Reinitialize();
        }
    }

    private void Reinitialize()
    {
        if (gameCanvas == null)
        {
            gameCanvas = GameObject.FindWithTag("GameCanvas").GetComponent<Canvas>();
        }
        if (resultsScreen == null)
        {
            resultsScreen = gameCanvas.GetComponentInChildren<Leaderboard>(true);
        }

        if (uiManager == null)
        {
            uiManager = gameCanvas.GetComponent<UIManager>();
        }

        if (menuCamera == null)
        {
            menuCamera = GameObject.FindWithTag("SceneCamera");
        }
    }

    public void PlaySelectedMiniGame(int index)
    {
        StartCoroutine(MiniGameManager.Instance.FadeOut(0.1f));
        menuCamera.SetActive(false);
        uiManager.ShowFreeModeMenu(false);
        gameCanvas.GetComponentInChildren<UIManager>().HideBackground();
        PlaySelectedMiniGame(minigameData[index]);
    }
    public void PlayBallonMiniGame()
    {
        menuCamera.SetActive(false);
        uiManager.ShowFreeModeMenu(false);
        gameCanvas.GetComponentInChildren<UIManager>().HideBackground();
        PlaySelectedMiniGame(minigameData[0]);
    }

    public void PlayFlagsMinigame()
    {
        menuCamera.SetActive(false);
        uiManager.ShowFreeModeMenu(false);
        gameCanvas.GetComponentInChildren<UIManager>().HideBackground();
        PlaySelectedMiniGame(minigameData[1]);
    }

    public void PlayBoxingMinigame()
    {
        menuCamera.SetActive(false);
        uiManager.ShowFreeModeMenu(false);
        gameCanvas.GetComponentInChildren<UIManager>().HideBackground();
        PlaySelectedMiniGame(minigameData[2]);
    }

    public void PlaySimonMinigame()
    {
        menuCamera.SetActive(false);
        uiManager.ShowFreeModeMenu(false);
        gameCanvas.GetComponentInChildren<UIManager>().HideBackground();
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

            if (selectedgame.MinigameDuration > 0f)
            {
                StartCoroutine(MinigameTimer(selectedgame.MinigameDuration));
            }
            else
            {
                // Start a 30-second timer to destroy this instance and load the next one
                StartCoroutine(MinigameTimer(minigameDuration)); // Pass a duration of 30 seconds
            }
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
        
        menuCamera.SetActive(true);
        resultsScreen.Show(true);
    }
    
}
