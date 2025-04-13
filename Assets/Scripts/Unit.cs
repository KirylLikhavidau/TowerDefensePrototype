using System;
using UnityEngine;

public abstract class Unit : PoolObject
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damageToUnits;
    [SerializeField] private float _dyingTime;

    public bool IsDead { get; protected set; }
    public float DamageToUnits => _damageToUnits;
    public float DyingTime => _dyingTime;

    public event Action<Unit> Died;
    public event Action Dying;

    private float _defaultHealth;

    private void Awake()
    {
        _defaultHealth = _health;
    }

    private void OnEnable()
    {
        IsDead = false;
        _health = _defaultHealth;
    }

    protected abstract void TakeDamage(float damage);

    protected void Die()
    {
        Dying?.Invoke();
        Died?.Invoke(gameObject.GetComponent<Unit>());
    }
}
