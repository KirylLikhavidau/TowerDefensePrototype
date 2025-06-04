using UnityEngine;
using Units.Enemy;
using Game.Classes;

namespace Game.Resource
{
    public class Health : Resource<int>
    {
        [SerializeField] private HealthZone _healthZone;

        private void OnEnable()
        {
            _healthZone.EnemyMissed += DecreaseHealth;
        }

        private void OnDisable()
        {
            _healthZone.EnemyMissed -= DecreaseHealth;
        }

        private void DecreaseHealth(EnemyUnit enemy)
        {
            ResourceAmount.Value = Mathf.Clamp(ResourceAmount.Value - enemy.DamageToPlayer, 0, _maxResource);
        }
    }
}
