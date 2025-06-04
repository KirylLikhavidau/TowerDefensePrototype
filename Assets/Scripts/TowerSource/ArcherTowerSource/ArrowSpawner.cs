using UnityEngine;
using Pool;

namespace Towers.Source
{
    public class ArrowSpawner : ObjectSpawner 
    {
        [SerializeField] private ArrowRemover _remover;
        [SerializeField] private ArcherTower _tower;

        protected override void Spawn()
        {
            var obj = _pool.GetObject();
            obj.gameObject.SetActive(true);
            obj.transform.position = _spawnPoint.position;
            Arrow arrow = (Arrow)obj;
            arrow.Tower = _tower;
            _remover.SubscribeInstance(arrow);
        }
    }
}
