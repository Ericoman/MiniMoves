using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniGameManager : MonoBehaviour
{
    public MinigameData[] minigameData;
    public GameObject miniGameInstance;
    public Canvas gameCanvas;
    
    
    public float minigameDuration = 30f;
    private static MiniGameManager _instance;
    public static MiniGameManager Instance => _instance;
    // Keep track of the played minigames
    private List<MinigameData> usedMinigames = new List<MinigameData>();

    public Leaderboard resultsScreen;
    public int gamePoints;
    public AudioSource tutorialSound;

    public GameObject menuCamera;
    public CanvasGroup fadeScreen;
    
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
        
        resultsScreen.Show(false);
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
        
        if (menuCamera == null)
        {
            menuCamera = GameObject.FindWithTag("SceneCamera");
        }
    }

    public void LoadMinijuego()
    {
        StartCoroutine(TutorialPanelLogic());
    }
    public void PlaySelectedMiniGame(MinigameData selectedgame)
    {
        StartCoroutine(FadeOut(1.5f));
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

    IEnumerator TutorialPanelLogic()
    {
        // Select a random minigame
        if (minigameData != null && minigameData.Length > 0)
        {
            MinigameData selectedGame = GetRandomUnusedMiniGame(); // Method to select unused minigame

            if (selectedGame != null)
            {
                // Instantiate the tutorial panel
                if (selectedGame.TutorialPanel != null)
                {
                    
                    menuCamera.SetActive(true);
                    tutorialSound.Play();
                    GameObject tutorialPanelInstance = Instantiate(selectedGame.TutorialPanel);

                    // Ensure the tutorial panel is part of the Canvas hierarchy
                    if (gameCanvas != null)
                    {
                        tutorialPanelInstance.transform.SetParent(gameCanvas.transform, false); // Attach to the Canvas
                        // Reset the RectTransform for correct scaling
                        RectTransform rectTransform = tutorialPanelInstance.GetComponent<RectTransform>();
                        if (rectTransform != null)
                        {
                            rectTransform.anchoredPosition = Vector2.zero; // Center it
                            rectTransform.localScale = Vector3.one; // Reset scale
                            rectTransform.sizeDelta = Vector2.zero; // Optionally adjust size
                        }
                    }
                    else
                    {
                        Debug.LogError("Game Canvas is not assigned.");
                    }
                    Debug.Log("Tutorial panel displayed for: " + selectedGame.MinigameID);

                    // Wait for 5 seconds before proceeding
                    yield return new WaitForSeconds(5);

                    // Destroy the tutorial panel after the timer
                    Destroy(tutorialPanelInstance);
                }
                else
                {
                    Debug.LogWarning("Tutorial panel is missing for: " + selectedGame.MinigameID);
                }
                
                menuCamera.SetActive(false);
                // Instantiate the minigame and start the 30-second timer
                PlaySelectedMiniGame(selectedGame);
                
            }
            else
            {
                Debug.LogWarning("No unused minigames available to select.");
                menuCamera.SetActive(true);
                resultsScreen.Show(true);
                gameCanvas.GetComponent<UIManager>().leaderboardMusic.Play();
                gameCanvas.GetComponent<UIManager>().music.Stop();
            }
        }
        else
        {
            GetRandomUnusedMiniGame();
            Debug.LogWarning("No minigame data available");
        }
    }

    IEnumerator MinigameTimer(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);
        
        StartCoroutine(FadeIn(1.5f));
        yield return new WaitForSeconds(1.5f);
        // Destroy the current minigame instance
        if (miniGameInstance != null)
        {
            Destroy(miniGameInstance);
            miniGameInstance = null;
            Debug.Log("Minigame instance destroyed after " + duration + " seconds.");
        }
        
        // Load the next random minigame
        StartCoroutine(TutorialPanelLogic());
    }

    private MinigameData GetRandomUnusedMiniGame()
    {
        // Get the list of unused minigames
        List<MinigameData> unusedMinigames = new List<MinigameData>();

        foreach (var game in minigameData)
        {
            if (!usedMinigames.Contains(game))
            {
                unusedMinigames.Add(game);
            }
        }

        if (unusedMinigames.Count > 0)
        {
            // Randomly select one of the unused minigames
            MinigameData selectedGame = unusedMinigames[Random.Range(0, unusedMinigames.Count)];
            usedMinigames.Add(selectedGame); // Mark it as used
            return selectedGame;
        }

        // If no unused minigames are available, reset the used list and return null
        usedMinigames.Clear();
        Debug.Log("All minigames have been played. Resetting the used minigame list.");
        return null;
    }

    public void AddGamePoints(int pointsToAdd)
    {
        gamePoints += pointsToAdd;
    }

    public void RemoveGamePoints(int pointsToRemove)
    {
        gamePoints-= pointsToRemove;
    }
    
    public void RestartGame()
    {
        usedMinigames.Clear();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fadeScreen.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
    }
    
    public IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            fadeScreen.alpha = Mathf.Clamp01(1 - elapsedTime / duration);
            yield return null;
        }
    }
}
