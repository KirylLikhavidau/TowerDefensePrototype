using System;
using UnityEngine;
using UnityEngine.UI;

public class ArcherTower : Tower
{
    private const float _damageUpgradeMultiplier = 1.5f;
    private const float _costUpgradeMultiplier = 1.8f;

    [SerializeField] private Button _makeButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;

    public event Action TowerUpgraded;
    public event Action TowerSold;

    private void Awake()
    {
        _currentDamageToUnits = _defaultDamageToUnits;
        _currentTowerCost = _defaultTowerCost;
    }

    private void OnEnable()
    {
        _makeButton.onClick.AddListener(Upgrade);
        _upgradeButton.onClick.AddListener(Upgrade);
        _sellButton.onClick.AddListener(Sell);
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
        _currentDamageToUnits *= _damageUpgradeMultiplier;
        _currentTowerCost *= _costUpgradeMultiplier;
        TowerUpgraded?.Invoke();
    }

    protected override void Sell()
    {
        _currentDamageToUnits = _defaultDamageToUnits;
        _currentTowerCost = _defaultTowerCost;
        TowerSold?.Invoke();
    }
}