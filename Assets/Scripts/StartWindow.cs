using System;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    [SerializeField] private OptionsWindow _options;
    [SerializeField] private LevelSelectWindow _levelsWindow;
    [SerializeField] private Button _levelHubButton;
    [SerializeField] private Button _optionsButton;

    public event Action LevelHubChoosed;
    public event Action OptionsChoosed;

    private Level _currentLevel;

    private void OnEnable()
    {
        _options.HomeButtonClicked += ShowThisWindow;
        _levelsWindow.HomeButtonClicked += ShowThisWindow;
        _levelsWindow.LevelInitializing += SubscrideToLevelInstance;
        _levelHubButton.onClick.AddListener(InvokeLevelHub);
        _optionsButton.onClick.AddListener(InvokeOptionsHub);
    }

    private void OnDisable()
    {
        _options.HomeButtonClicked -= ShowThisWindow;
        _levelsWindow.HomeButtonClicked -= ShowThisWindow;
        _levelsWindow.LevelInitializing -= SubscrideToLevelInstance;
        _levelHubButton.onClick.RemoveListener(InvokeLevelHub);
        _optionsButton.onClick.RemoveListener(InvokeOptionsHub);
    }

    private void SubscrideToLevelInstance(Level level)
    {
        _currentLevel = level;

        _currentLevel.PauseWindow.HomeButtonClicked += ShowThisWindow;
        _currentLevel.Closing += UnSubscrideToLevelInstance;
    }

    private void UnSubscrideToLevelInstance()
    {
        _currentLevel.PauseWindow.HomeButtonClicked -= ShowThisWindow;
        _currentLevel.Closing -= UnSubscrideToLevelInstance;
    }

    private void InvokeLevelHub()
    {
        HideThisWindow();
        LevelHubChoosed?.Invoke();
    }

    private void InvokeOptionsHub()
    {
        HideThisWindow();
        OptionsChoosed?.Invoke();
    }
}