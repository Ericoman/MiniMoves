using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    public Image titleImage, background;
    public GameObject menu, selectionMenu, freeModeMenu, selectSkinmenu, leaderboards, scoreboard;
    public MiniGameManager minigameManager;
    public MinigameData[] minigamedata;
    private int indexActual = 0;
    public Text scoreText;
    public AudioSource music, logoMusic, leaderboardMusic;
    //public Text freeModeGame;
    public Camera uselessCamera;
    public bool logobool = true;
    public float fadeInTime = 0.1f;
    public float fadeOutTime = 0.1f;
    public AudioSource click, back;


    private const string SCORE_KEY = "score_table";
    void Start()
    {
        // Opcional: iniciar con fade in

        StartCoroutine(FadeIn());
        ActualizarTexto();
        minigameManager = MiniGameManager.Instance;
        //prevgame.onClick.AddListener(() => CambiarMinijuego(-1));
        //nextgame.onClick.AddListener(() => CambiarMinijuego(1));
    }
    public void Update()
    {
        
    }


    public void CambiarMinijuego(int direccion)
    {
        indexActual += direccion;
        Debug.Log("BOPTON");
        if (indexActual < 0) indexActual = minigamedata.Length - 1;
        if (indexActual >= minigamedata.Length) indexActual = 0;

        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (minigamedata.Length > 0)
        {
            //freeModeGame.text = minigamedata[indexActual].MinigameID;
        }
        else
        {
            //freeModeGame.text = "No hay minijuegos";
        }
    }
    public MinigameData getSelectedMinigame()
    {
        return minigamedata[indexActual];
    }

    public void ShowFreeModeMenu(bool bShow)
    {
        freeModeMenu.SetActive(bShow);
    }
    public void HideBackground()
    {
         background.gameObject.SetActive(false);
    }
    
    
    public IEnumerator FadeIn()
    {
        logoMusic.Play();
        
        float elapsed = 0f;
        Color color = titleImage.color;
        color.a = 0f;
        titleImage.color = color;

        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / 2);
            titleImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(fadeInTime);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = titleImage.color;
        color.a = 1f;
       titleImage.color = color;

        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / 2);
            titleImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(fadeOutTime);
        logobool = false;
        menu.SetActive(true);
        music.Play();
    }

    public void PlayButton()
    {
        click.Play();
        
        menu.SetActive(false);
        selectionMenu.SetActive(true);
    }
    public void QuitButton()
    {
        back.Play();
#if UNITY_EDITOR
        // Esto detiene el juego en el editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Esto funciona en una build real
        Application.Quit();
#endif
    }

    public void ChallengeButton()
    {
        music.Stop();
        click.Play();
        selectionMenu.SetActive(false);
        background.enabled = false;
        uselessCamera.gameObject.SetActive(false);
        minigameManager.LoadMinijuego();
    }
    public void FreeModeButton()
    {
        click.Play();
        selectionMenu.SetActive(false);
        freeModeMenu.SetActive(true);

    }
    public void SelectSkinButton()
    {
        click.Play();
        selectionMenu.SetActive(false);
        selectSkinmenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        scoreboard.SetActive(true);
        back.Play();
        selectionMenu.SetActive(false);
        background.enabled = true;
        menu.SetActive(true);
    }
    public void BackToSelectionMenu()
    {
        scoreboard.SetActive(false);
        back.Play();
        freeModeMenu.SetActive(false);
        selectSkinmenu.SetActive(false);
        leaderboards.SetActive(false);
        
        selectionMenu.SetActive(true);
    }
    public void PlaySelectedMode()
    {

        click.Play();
        freeModeMenu.SetActive(false);
        //minigameManager.GetComponent<MiniGameManager>().PlaySelectedMiniGame(getSelectedMinigame());
    }
    public void showLeaderboards()
    {
        music.Stop();
        leaderboardMusic.Play();
        selectionMenu.SetActive(false);
        scoreboard.SetActive(true);
        showScores();
    }
    public void StopMusic()
    {
        music.Stop();
    }

    public void showScores()
    {
        ScoreList scoreList = LoadScores();
        string display = "RANKING\n";

        for (int i = 0; i < scoreList.scores.Count; i++)
        {
            ScoreEntry entry = scoreList.scores[i];
            display += $"{i + 1}. {entry.playerName} - {entry.score}\n";
        }

        scoreText.text = display;
    }


    public void AddNewScore(string name, int score)
    {
        leaderboards.SetActive(false);
        scoreboard.SetActive(true);
        // Cargar tabla actual
        ScoreList scoreList = LoadScores();
        if (name == "")
        {
            name = "Default";
        }

        // Agregar nueva entrada
        ScoreEntry newEntry = new ScoreEntry { playerName = name, score = score };
        scoreList.scores.Add(newEntry);

        // Ordenar de mayor a menor
        scoreList.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Limitar a 10 entradas
        if (scoreList.scores.Count > 10)
            scoreList.scores.RemoveAt(scoreList.scores.Count - 1);

        // Guardar de nuevo
        string json = JsonUtility.ToJson(scoreList);
        PlayerPrefs.SetString(SCORE_KEY, json);
        PlayerPrefs.Save();

        showScores();
    }

    public ScoreList LoadScores()
    {
        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            string json = PlayerPrefs.GetString(SCORE_KEY);
            return JsonUtility.FromJson<ScoreList>(json);
        }
        else
        {
            return new ScoreList();
        }
    }


}

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}


