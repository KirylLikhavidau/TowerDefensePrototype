using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private int _buttonIndex;
        [SerializeField] private Button _button;

        public event Action<int> Clicked;

        private void OnEnable() 
        {
            _button.onClick.AddListener(InvokeClickEvent); 
        }

        private void OnDisable() 
        {
            _button.onClick.RemoveListener(InvokeClickEvent); 
        }

        private void InvokeClickEvent()
        {
            Clicked?.Invoke(_buttonIndex);
        }
    }
}
