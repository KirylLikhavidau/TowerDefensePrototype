using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float _defaultDamageToUnits;
    [SerializeField] protected float _defaultTowerCost;

    protected float _currentTowerCost;
    protected float _currentDamageToUnits;

    protected abstract void PerformAttack();
    protected abstract void Upgrade();
    protected abstract void Sell();
}
