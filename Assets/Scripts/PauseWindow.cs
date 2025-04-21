using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueGameButton;
    [SerializeField] private Button _homeButton;

    public event Action PauseButtonClicked;
    public event Action PlayButtonClicked;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(InvokeHomeButtonClickedEvent);   
        _continueGameButton.onClick.AddListener(InvokePlayAction);
        _pauseButton.onClick.AddListener(InvokePauseAction);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(InvokeHomeButtonClickedEvent);
        _continueGameButton.onClick.RemoveListener(InvokePlayAction);
        _pauseButton.onClick.RemoveListener(InvokePauseAction);
    }

    private void InvokePauseAction()
    {
        PauseButtonClicked?.Invoke();
        ShowThisWindow();
    }

    private void InvokePlayAction()
    {
        PlayButtonClicked?.Invoke();
        HideThisWindow();
    }
}
