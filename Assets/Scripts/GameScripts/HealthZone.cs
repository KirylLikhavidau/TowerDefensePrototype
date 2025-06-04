using System;
using UnityEngine;
using Units.Enemy;

namespace Game.Classes
{
    public class HealthZone : MonoBehaviour
    {
        public event Action<EnemyUnit> EnemyMissed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyUnit enemy))
                EnemyMissed?.Invoke(enemy);
        }
    }
}
