using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private LevelSelectWindow _levelsWindow;

    public event Action LevelClosed;

    private Level _currentLevel;

    private void OnEnable()
    {
        _levelsWindow.LevelInitializing += SubscrideToLevelInstance;
    }

    private void OnDisable()
    {
        _levelsWindow.LevelInitializing -= SubscrideToLevelInstance;
    }

    private void SubscrideToLevelInstance(Level level)
    {
        _currentLevel = level;

        _currentLevel.PauseWindow.PauseButtonClicked += PauseLevel;
        _currentLevel.PauseWindow.PlayButtonClicked += PlayLevel;
        _currentLevel.Closing += InvokeLevelClosingEvent;
    }

    private void InvokeLevelClosingEvent()
    {
        _currentLevel.PauseWindow.PauseButtonClicked -= PauseLevel;
        _currentLevel.PauseWindow.PlayButtonClicked -= PlayLevel;
        _currentLevel.Closing -= InvokeLevelClosingEvent;
        LevelClosed?.Invoke();
        Time.timeScale = 1f;
    }

    private void PauseLevel()
    {
        Time.timeScale = 0f;
    }

    private void PlayLevel()
    {
        Time.timeScale = 1f;
    }
}
