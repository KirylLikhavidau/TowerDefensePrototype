using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcherTower : Tower
{
    private readonly float _damageUpgradeMultiplier = 1.5f;
    private readonly float _costUpgradeMultiplier = 1.8f;
    private readonly float _costSellMultiplier = 0.7f;

    [SerializeField] private AttackZone _attackDistance;
    [SerializeField] private ObjectPool _arrowPool;
    [SerializeField] private ArrowSpawner _arrowSpawner;
    [SerializeField] private ArrowMover _arrowMover;
    [SerializeField] private Button _makeButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;

    public event Action TowerUpgraded;
    public event Action TowerSold;
    public event Action<float> ManaDecreasing;
    public event Action<float> ManaIncreasing;

    private bool _alreadyAttackingEnemy = false;
    private Queue<Enemy> _enemiesToAttack;

    private void Awake()
    {
        _enemiesToAttack = new Queue<Enemy>();
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

        if (_attackDistance != null)
        {
            _attackDistance.EnemyEntered += AddToTargetList;
            _attackDistance.EnemyExited += RemoveTargetFromList;   
        }
    }

    private void OnDisable()
    {
        _makeButton?.onClick.RemoveListener(Upgrade);
        _upgradeButton?.onClick.RemoveListener(Upgrade);
        _sellButton?.onClick.RemoveListener(Sell);

        if (_attackDistance != null)
        {
            _attackDistance.EnemyEntered -= AddToTargetList;
            _attackDistance.EnemyExited -= RemoveTargetFromList;
        }
    }

    private void AddToTargetList(Enemy target)
    {
        target.Dying += RemoveTargetFromList;

        _enemiesToAttack.Enqueue(target);
        StartAttack();
    }

    private void RemoveTargetFromList()
    {
        if (_enemiesToAttack.TryPeek(out Enemy removingTarget))
        {
            _enemiesToAttack.Dequeue();
            removingTarget.Dying -= RemoveTargetFromList;
        }
        
        StopAttack();

        if (_enemiesToAttack.TryPeek(out Enemy target))
            StartAttack();
    }

    protected override void StartAttack()
    {
        if (_alreadyAttackingEnemy == false)
        {
            if (_arrowPool.transform.childCount != 0)
                foreach (var arrow in _arrowPool.PooledObjects)
                {
                    arrow.gameObject.TryGetComponent(out ArrowMover mover);
                    mover.Target = _enemiesToAttack.Peek();
                }
            else
                _arrowMover.Target = _enemiesToAttack.Peek();
            _arrowSpawner.gameObject.SetActive(true);
            _alreadyAttackingEnemy = true;
        }
    }

    protected override void StopAttack()
    {
        _arrowSpawner?.gameObject.SetActive(false);
        _alreadyAttackingEnemy = false;
    }

    protected override void Upgrade() 
    {
        if (_attackDistance.gameObject.activeSelf == false)
            _attackDistance?.gameObject.SetActive(true);
        _currentSellPrice = _currentUpgradeCost * _costSellMultiplier;
        ManaDecreasing?.Invoke(_currentUpgradeCost);
        _currentDamageToUnits *= _damageUpgradeMultiplier;
        _currentUpgradeCost *= _costUpgradeMultiplier;
        TowerUpgraded?.Invoke();
    }

    protected override void Sell()
    {
        _attackDistance?.gameObject.SetActive(false);
        ManaIncreasing?.Invoke(_currentSellPrice);
        _currentDamageToUnits = _defaultDamageToUnits;
        _currentUpgradeCost = _defaultUpgradeCost;
        TowerSold?.Invoke();
    }
}
