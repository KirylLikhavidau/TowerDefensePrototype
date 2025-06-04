using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Classes;

namespace Game.Window
{
    public class LevelSelectWindow : Window
    {
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private Button _homeButton;
        [SerializeField] private LevelButton[] _levelButtons;
        [SerializeField] private Level[] _levelPrefabs;

        public event Action<Level> LevelInitializing;

        private void OnEnable()
        {
            _startWindow.LevelHubChoosed += ShowThisWindow;
            _homeButton.onClick.AddListener(InvokeHomeButtonClickedEvent);

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].Clicked += InitLevel;
            }    
        }

        private void OnDisable()
        {
            _startWindow.LevelHubChoosed -= ShowThisWindow;
            _homeButton.onClick.RemoveListener(InvokeHomeButtonClickedEvent);

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].Clicked -= InitLevel;
            }
        }

        private void InitLevel(int levelIndex)
        {
            HideThisWindow();
            Level spawnedLevel = Instantiate(_levelPrefabs[levelIndex]);
            LevelInitializing.Invoke(spawnedLevel);
        }
    }
}
