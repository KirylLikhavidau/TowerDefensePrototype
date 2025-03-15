using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private float _health;

    public bool IsDead => _health <= 0;

    private void Update()
    {
        if (IsDead)
            _pool.PutObject(gameObject.GetComponent<Enemy>());
    }
}
