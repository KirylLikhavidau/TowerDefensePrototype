using System;
using UnityEngine;

public class Level : MonoBehaviour 
{
    [SerializeField] private PauseWindow _pauseWindow;

    public PauseWindow PauseWindow => _pauseWindow;

    public event Action Closing;

    private void OnEnable()
    {
        _pauseWindow.HomeButtonClicked += EscapeFromLevel;
    }

    private void OnDisable()
    {
        _pauseWindow.HomeButtonClicked -= EscapeFromLevel;
    }

    private void EscapeFromLevel()
    {
        Closing?.Invoke();
        Destroy(gameObject);
    }
}