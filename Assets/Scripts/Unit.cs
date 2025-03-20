using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float _health;

    public event Action<Unit> Died;

    protected abstract void TakeDamage(float damage);

    protected void Die()
    {
        Died?.Invoke(gameObject.GetComponent<Unit>());
    }
}
