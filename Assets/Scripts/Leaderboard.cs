using TMPro;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI text;
    public InputField inputfield;
    public UIManager uimanager;
    

    public void Start()
    {
        uimanager.uselessCamera.enabled = true;
        uimanager.music.Stop();
        uimanager.leaderboardMusic.Play();

    }
    void Update()
    {
        text.text = MiniGameManager.Instance.gamePoints.ToString();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
        uimanager.background.gameObject.SetActive(true);
        uimanager.uselessCamera.enabled = true;
        
        //
    }

    public void showLeaderboards()
    {
        uimanager.music.Stop();
        if (!uimanager.leaderboardMusic.isPlaying)
        {
            uimanager.leaderboardMusic.Play();
        }
        
        uimanager.AddNewScore(inputfield.text, MiniGameManager.Instance.gamePoints);
    }

    public void OnRestartGame()
    {
        MiniGameManager.Instance.RestartGame();
    }
}
