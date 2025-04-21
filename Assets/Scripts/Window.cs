using System;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _windowCanvasGroup;

    public event Action HomeButtonClicked;

    private void Awake()
    {
        if (gameObject.TryGetComponent(out Canvas canvas))
            canvas.worldCamera = Camera.main;
    }

    protected void HideThisWindow()
    {
        _windowCanvasGroup.alpha = 0f;
        _windowCanvasGroup.interactable = false;
        _windowCanvasGroup.blocksRaycasts = false;
    }

    protected void ShowThisWindow()
    {
        _windowCanvasGroup.alpha = 1f;
        _windowCanvasGroup.interactable = true;
        _windowCanvasGroup.blocksRaycasts = true;
    }

    protected void InvokeHomeButtonClickedEvent()
    {
        HideThisWindow();
        HomeButtonClicked?.Invoke();
    }
}