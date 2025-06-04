using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Classes;
using Pool;

namespace Units.Enemy
{
    public class EnemyRemover : ObjectRemover
    {
        [SerializeField] private HealthZone _zone;

        private Queue<EnemyUnit> _enemies = new Queue<EnemyUnit>();

        public void SubscribeInstance(EnemyUnit enemy)
        {
            _enemies.Enqueue(enemy);
            enemy.Died += DelayRemoving;
        }

        private void OnEnable() { _zone.EnemyMissed += RemoveObject; }

        protected override void OnDisable()
        {
            _zone.EnemyMissed -= RemoveObject;
            for (int i = 0; i < _enemies.Count; i++)
                _enemies.Dequeue().Died -= DelayRemoving;
        }

        private void DelayRemoving(EnemyUnit enemy)
        {
            StartCoroutine(nameof(Remove), enemy);
        }

        private IEnumerator Remove(EnemyUnit enemy)
        {
            yield return new WaitForSeconds(enemy.DyingTime);
            RemoveObject(enemy);
            yield break;
        }
    }
}
