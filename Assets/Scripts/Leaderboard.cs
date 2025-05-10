using TMPro;
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
    }
    void Update()
    {
        text.text = MiniGameManager.Instance.gamePoints.ToString();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
        uimanager.uselessCamera.enabled = true;
    }

    public void showLeaderboards()
    {
        uimanager.AddNewScore(inputfield.text, MiniGameManager.Instance.gamePoints);
    }

    public void OnRestartGame()
    {
        MiniGameManager.Instance.RestartGame();
    }
}
