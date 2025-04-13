using System;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public event Action<Enemy> EnemyEntered;
    public event Action EnemyExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            EnemyEntered?.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            EnemyExited?.Invoke();
    }
}