using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_InputField inputfield;
    public UIManager uimanager;
    public Text dialogue;
    public string[] level1, level2;
    

    public void Start()
    {
        uimanager.uselessCamera.enabled = true;
        
        if (Random.Range(0, 2) == 0)
        {
            dialogue.text = level1[Random.Range(0, level1.Length)];
        }
        else
        {
            dialogue.text = level2[Random.Range(0, level1.Length)];
        }

    }
    void Update()
    {
        text.text = MiniGameManager.Instance.gamePoints.ToString();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
        if (bShow)
        {
            uimanager.music.Stop();
            uimanager.leaderboardMusic.Play();
            
        }

        
        
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
        MiniGameManager.Instance.ResetGamePoints();;
    }

    public void OnRestartGame()
    {
        MiniGameManager.Instance.RestartGame();
    }
}
