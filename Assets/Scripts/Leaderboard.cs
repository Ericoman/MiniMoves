using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = MiniGameManager.Instance.gamePoints.ToString();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    public void OnRestartGame()
    {
        MiniGameManager.Instance.RestartGame();
    }
}
