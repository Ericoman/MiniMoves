using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public MinigameData[] minigameData;
    public GameObject miniGameInstance;
    public Camera uselessCamera;

    void Start()
    {
       
    }

    public void LoadMinijuego()
    {
        if (minigameData != null && minigameData[Random.Range(0,minigameData.Length)].MiniGamePrefab != null)
        {
            miniGameInstance = Instantiate(minigameData[0].MiniGamePrefab);
            Debug.Log("Minijuego cargado: " + minigameData[0].MinigameID);
        }
        else
        {
            Debug.LogWarning("Datos del minijuego incompletos");
        }
    }
    public void PlaySelectedMiniGame(MinigameData selectedgame)
    {
        if (selectedgame != null && selectedgame.MiniGamePrefab != null)
        {
            miniGameInstance = Instantiate(selectedgame.MiniGamePrefab);
            Debug.Log("Minijuego cargado: " + selectedgame.MinigameID);
        }
        else
        {
            Debug.LogWarning("Datos del minijuego incompletos");
        }
    }

}
