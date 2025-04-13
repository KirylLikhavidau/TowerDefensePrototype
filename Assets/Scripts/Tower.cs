using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float _defaultDamageToUnits;
    [SerializeField] protected float _defaultUpgradeCost;

    protected float _currentUpgradeCost;
    protected float _currentDamageToUnits;
    protected float _currentSellPrice;

    public float CurrentDamageToUnits => _currentDamageToUnits;
    public float CurrentUpgradeCost => _currentUpgradeCost;
    public float CurrentSellPrice => _currentSellPrice;

    protected abstract void StartAttack();
    protected abstract void StopAttack();
    protected abstract void Upgrade();
    protected abstract void Sell();
}
