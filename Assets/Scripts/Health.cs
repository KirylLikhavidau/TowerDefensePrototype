using System;
using UnityEngine;

public class Health : Resource
{
    [SerializeField] private HealthZone _healthZone;

    private void OnEnable()
    {
        _healthZone.EnemyMissed += DecreaseHealth;
    }

    private void OnDisable()
    {
        _healthZone.EnemyMissed -= DecreaseHealth;
    }

    private void DecreaseHealth(Enemy enemy)
    {
        _resource = Mathf.Clamp(_resource - enemy.DamageToPlayer, 0, _maxResource);
        InvokeAmountChangedEvent(_resource);
    }
}
