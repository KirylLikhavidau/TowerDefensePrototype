using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArcherTower : Tower
{
    private const float _damageUpgradeMultiplier = 1.5f;
    private const float _costUpgradeMultiplier = 1.8f;
    private const float _costSellMultiplier = 0.7f;

    [SerializeField] private Button _makeButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;

    public event Action TowerUpgraded;
    public event Action TowerSold;
    public event Action<float> ManaDecreasing;
    public event Action<float> ManaIncreasing;

    private void Awake()
    {
        _currentDamageToUnits = _defaultDamageToUnits;
        _currentUpgradeCost = _defaultUpgradeCost;
        _currentSellPrice = _currentUpgradeCost * _costSellMultiplier;
    }

    private void OnEnable()
    {
        if (_makeButton != null & _sellButton != null & _upgradeButton != null)
        {
            _makeButton.onClick.AddListener(Upgrade);
            _upgradeButton.onClick.AddListener(Upgrade);
            _sellButton.onClick.AddListener(Sell);
        }
    }

    private void OnDisable()
    {
        _makeButton?.onClick.RemoveListener(Upgrade);
        _upgradeButton?.onClick.RemoveListener(Upgrade);
        _sellButton?.onClick.RemoveListener(Sell);
    }

    protected override void PerformAttack() { }
    protected override void Upgrade() 
    {
        _currentSellPrice = _currentUpgradeCost * _costSellMultiplier;
        ManaDecreasing?.Invoke(_currentUpgradeCost);
        _currentDamageToUnits *= _damageUpgradeMultiplier;
        _currentUpgradeCost *= _costUpgradeMultiplier;
        TowerUpgraded?.Invoke();
    }

    protected override void Sell()
    {
        ManaIncreasing?.Invoke(_currentSellPrice);
        _currentDamageToUnits = _defaultDamageToUnits;
        _currentUpgradeCost = _defaultUpgradeCost;
        TowerSold?.Invoke();
    }
}