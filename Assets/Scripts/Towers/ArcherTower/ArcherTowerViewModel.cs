using UnityEngine;
using System;
using ProjectClasses;
using Game.Resource;

namespace Towers
{
    public class ArcherTowerViewModel : MonoBehaviour
    {
        [SerializeField] private ArcherTowerView _view;
        [SerializeField] private ArcherTower _tower;
        [SerializeField] private Mana _mana;

        public ReactiveProperty<int> SellPriceLabel = new();
        public ReactiveProperty<int> UpgradePriceLabel = new();
        public ReactiveProperty<int> MakePriceLabel = new();

        public event Action ShowingUIWrapping;
        public event Action ShowingUIUnwrapping;
        public event Action TowerUpgrading;
        public event Action TowerSelling;
        public event Action TurningOffUpgradeButtons;
        public event Action TurningOnUpgradeButtons;
        public event Action<float> ManaDecreasing;
        public event Action<float> ManaIncreasing;


        private void OnEnable()
        {
            _mana.ResourceAmount.OnChanged += (value) => ValidateButtons();
            _view.OpenButtonClicked += () => ShowingUIUnwrapping?.Invoke();
            _view.CloseButtonClicked += () => ShowingUIWrapping?.Invoke();
            _view.UpgradeButtonClicked += () => 
            {
                ShowingUIWrapping?.Invoke();
                ManaDecreasing?.Invoke(_tower.UpgradeCost);
                TowerUpgrading?.Invoke();
            };
            _view.SellButtonClicked += () =>
            {
                ShowingUIWrapping?.Invoke();
                ManaIncreasing?.Invoke(_tower.SellCost);
                TowerSelling?.Invoke();
            };
        }

        private void OnDisable()
        {
            _mana.ResourceAmount.OnChanged -= (value) => ValidateButtons();
            _view.OpenButtonClicked -= () => ShowingUIUnwrapping?.Invoke();
            _view.CloseButtonClicked -= () => ShowingUIWrapping?.Invoke();
            _view.UpgradeButtonClicked -= () =>
            {
                ShowingUIWrapping?.Invoke();
                ManaDecreasing?.Invoke(_tower.UpgradeCost);
                TowerUpgrading?.Invoke();
            };
            _view.SellButtonClicked -= () =>
            {
                ShowingUIWrapping?.Invoke();
                ManaIncreasing?.Invoke(_tower.SellCost);
                TowerSelling?.Invoke();
            };
        }

        private void Start()
        {
            ValidateButtons();
        }

        private void ValidateButtons()
        {
            if (_mana.ResourceAmount.Value < _tower.UpgradeCost)
                TurningOffUpgradeButtons.Invoke();
            else if (_mana.ResourceAmount.Value >= _tower.UpgradeCost)
                TurningOnUpgradeButtons.Invoke();
        }
    }
}