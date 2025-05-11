using System;
using UnityEngine;

public class ShowablePanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        Show(false);
    }

    public void Show(bool bShow)
    {
        if (canvasGroup == null)
        {
            gameObject.SetActive(bShow);
            return;
        }
        
        canvasGroup.alpha = bShow ? 1 : 0;
        canvasGroup.interactable = bShow;
        canvasGroup.blocksRaycasts = bShow;
    }
}
