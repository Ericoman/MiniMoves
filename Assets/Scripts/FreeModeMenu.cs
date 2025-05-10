using UnityEngine;

public class FreeModeMenu : MonoBehaviour
{
    public void InitMinigame(int index)
    {
        FreeModeManager.Instance.PlaySelectedMiniGame(index);
    }
}
