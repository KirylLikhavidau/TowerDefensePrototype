using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 20;
    [SerializeField] private HealthZone _healthZone;

    public int HealthAmount => _health;

    public event Action<int> HealthChanged;

    private int _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

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
        _health = Mathf.Clamp(_health - enemy.DamageToPlayer, 0, _maxHealth);
        HealthChanged?.Invoke(_health);
    }
}