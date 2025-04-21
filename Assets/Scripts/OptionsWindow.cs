using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow : Window
{
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private Button _homeButton;

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(InvokeHomeButtonClickedEvent);
        _startWindow.OptionsChoosed += ShowThisWindow;
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(InvokeHomeButtonClickedEvent);
        _startWindow.OptionsChoosed -= ShowThisWindow;
    }
}