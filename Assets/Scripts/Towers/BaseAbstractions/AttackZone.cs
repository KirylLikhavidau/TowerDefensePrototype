using System;
using UnityEngine;
using Units.Enemy;

namespace Towers
{
    public class AttackZone : MonoBehaviour
    {
        public event Action<EnemyUnit> EnemyEntered;
        public event Action EnemyExited;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyUnit enemy))
                if(enemy.IsDead == false)
                    EnemyEntered?.Invoke(enemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyUnit enemy))
                if (enemy.IsDead == false)
                    EnemyExited?.Invoke();
        }
    }
}