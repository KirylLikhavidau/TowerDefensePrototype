using UnityEngine;
using Pool;
using Game.Resource;

namespace Units.Enemy
{
    public class EnemySpawner : ObjectSpawner 
    {
        [SerializeField] private EnemyRemover _remover;
        [SerializeField] private Mana _mana;

        protected override void Spawn()
        {
            var obj = _pool.GetObject();
            obj.gameObject.SetActive(true);
            obj.transform.position = _spawnPoint.position;
            _remover.SubscribeInstance((EnemyUnit)obj);
            _mana.SubscribeInstance((EnemyUnit)obj);
        }
    }
}
