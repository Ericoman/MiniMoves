using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image titleImage, background;
    public GameObject menu, selectionMenu, freeModeMenu, selectSkinmenu;
    public MiniGameManager minigameManager;
    public MinigameData[] minigamedata;
    private int indexActual = 0;
    public Button prevgame, nextgame;
    //public Text freeModeGame;
    public Camera uselessCamera;

    public float fadeInTime = 0.1f;
    public float fadeOutTime = 0.1f;
    public AudioSource click, back;
    void Start()
    {
        // Opcional: iniciar con fade in
        StartCoroutine(FadeIn());
        ActualizarTexto();
        minigameManager = MiniGameManager.Instance;
        //prevgame.onClick.AddListener(() => CambiarMinijuego(-1));
        //nextgame.onClick.AddListener(() => CambiarMinijuego(1));
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

    public IEnumerator FadeIn()
    {
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
        menu.SetActive(true);
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
        back.Play();
        selectionMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void BackToSelectionMenu()
    {
        back.Play();
        freeModeMenu.SetActive(false);
        selectSkinmenu.SetActive(false);
        
        selectionMenu.SetActive(true);
    }
    public void PlaySelectedMode()
    {

        click.Play();
        freeModeMenu.SetActive(false);
        //minigameManager.GetComponent<MiniGameManager>().PlaySelectedMiniGame(getSelectedMinigame());
    }
}
