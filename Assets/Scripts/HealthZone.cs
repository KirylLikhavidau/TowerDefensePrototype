using System;
using UnityEngine;

public class HealthZone : MonoBehaviour
{
    public event Action<Enemy> EnemyMissed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyMissed?.Invoke(enemy);
    }
}
