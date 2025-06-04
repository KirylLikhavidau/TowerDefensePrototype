using System.Collections.Generic;
using UnityEngine;
using States;
using States.Tower;
using Towers.Source;
using Units.Enemy;
using Pool;

namespace Towers
{
    public class ArcherTower : MonoBehaviour, ITower
    {
        [SerializeField] private ArcherTowerViewModel _viewModel;
        [SerializeField] private AttackZone _attackDistance;
        [SerializeField] private ObjectPool _arrowPool;
        [SerializeField] private ArrowSpawner _arrowSpawner;
        [SerializeField] private ArrowMover _arrowMover;

        public float DamageToUnits => _state.DamageToUnits;
        public float UpgradeCost => _state.UpgradeCost;
        public float SellCost => _state.SellCost;

        private TowerState _state;
        private bool _alreadyAttackingEnemy = false;
        private Queue<EnemyUnit> _enemiesToAttack;

        private void Awake()
        {
            _state = new NotConstructedState(gameObject.GetComponent<ArcherTower>(), _attackDistance);
            _enemiesToAttack = new Queue<EnemyUnit>();
        }

        private void Start()
        {
            ChangePrices();
        }

        private void OnEnable()
        {
            _viewModel.TowerUpgrading += Upgrade;
            _viewModel.TowerSelling += Sell;

            if (_attackDistance != null)
            {
                _attackDistance.EnemyEntered += AddToTargetList;
                _attackDistance.EnemyExited += RemoveTargetFromList;   
            }
        }

        private void OnDisable()
        {
            _viewModel.TowerUpgrading -= Upgrade;
            _viewModel.TowerSelling -= Sell;

            if (_attackDistance != null)
            {
                _attackDistance.EnemyEntered -= AddToTargetList;
                _attackDistance.EnemyExited -= RemoveTargetFromList;
            }
        }

        private void AddToTargetList(EnemyUnit target)
        {
            target.Dying += RemoveTargetFromList;

            _enemiesToAttack.Enqueue(target);
            StartAttack();
        }

        private void RemoveTargetFromList()
        {
            if (_enemiesToAttack.TryPeek(out EnemyUnit removingTarget))
            {
                _enemiesToAttack.Dequeue();
                removingTarget.Dying -= RemoveTargetFromList;
            }
        
            StopAttack();

            if (_enemiesToAttack.TryPeek(out EnemyUnit target))
                StartAttack();
        }

        private void StartAttack()
        {
            if (_alreadyAttackingEnemy == false)
                if (_arrowPool.transform.childCount != 0)
                    foreach (var arrow in _arrowPool.PooledObjects)
                    {
                        arrow.gameObject.TryGetComponent(out ArrowMover mover);
                        mover.Target = _enemiesToAttack.Peek();
                    }
                else
                    _arrowMover.Target = _enemiesToAttack.Peek();
                _arrowSpawner.gameObject.SetActive(true);
                _alreadyAttackingEnemy = true;
        }

        private void StopAttack()
        {
            _arrowSpawner?.gameObject.SetActive(false);
            _alreadyAttackingEnemy = false;
        }

        public void SetState(string stateType)
        {
            TowerState newState;
            switch (stateType)
            {
                case TowerStateType.NotConstructed:
                    newState = new NotConstructedState(gameObject.GetComponent<ArcherTower>(), _attackDistance);
                    break;
                case TowerStateType.Builded:
                    newState = new BuildedState(gameObject.GetComponent<ArcherTower>(), _attackDistance);
                    break;
                case TowerStateType.UpgradedBuild:
                    newState = new UpgradedBuildState(gameObject.GetComponent<ArcherTower>(), _attackDistance); 
                    break;
                default:
                    newState = null;
                    Debug.LogError("No create method for type" + stateType);
                    break;
            }

            _state = newState;
        }

        private void Upgrade() { _state.Upgrade(); ChangePrices();  }
        private void Sell() { _state.Sell(); ChangePrices(); }

        private void ChangePrices()
        {
            _viewModel.SellPriceLabel.Value = _state.SellCost;
            _viewModel.UpgradePriceLabel.Value = _state.UpgradeCost;
            _viewModel.MakePriceLabel.Value = _state.UpgradeCost;

        }
    }
}
