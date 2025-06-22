using System;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemy;
using Towers;

namespace Game.Resource
{
    public class Mana : Resource<int>
    {
        [SerializeField] private List<ArcherTowerViewModel> _towerViewModels;

        private Queue<EnemyUnit> _spawnedEnemies = new Queue<EnemyUnit>();

        private void OnEnable()
        {
            foreach (var viewModel in _towerViewModels)
            {
                viewModel.ManaDecreasing += DecreaseMana;
                viewModel.ManaIncreasing += IncreaseMana;
            }
        }

        private void OnDisable()
        {
            foreach (var viewModel in _towerViewModels)
            {
                viewModel.ManaDecreasing -= DecreaseMana;
                viewModel.ManaIncreasing -= IncreaseMana;
            }

            for (int i = 0; i < _spawnedEnemies.Count; i++)
                _spawnedEnemies.Dequeue().Died -= (enemy) => IncreaseMana(enemy.IncomeFromDeath);
        }

        public void SubscribeInstance(EnemyUnit enemy)
        {
            _spawnedEnemies.Enqueue(enemy);
            //Увеличивает ману на два, почему?
            enemy.Died += (enemy) => IncreaseMana(enemy.IncomeFromDeath);
        }

        private void DecreaseMana(float decreaseAmount)
        {
            ResourceAmount.Value = Mathf.Clamp(ResourceAmount.Value - Convert.ToInt32(Mathf.Round(decreaseAmount)), 0, _maxResource);
        }

        private void IncreaseMana(float increaseAmount)
        {
            ResourceAmount.Value = Mathf.Clamp(ResourceAmount.Value + Convert.ToInt32(Mathf.Round(increaseAmount)), 0, _maxResource);
        }
    }
}