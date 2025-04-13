using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class Mana : Resource
{
    [SerializeField] private List<ArcherTower> _towerPrefabs;

    public int Amount => _resource;

    private Queue<Enemy> _spawnedEnemies = new Queue<Enemy>();

    private void OnEnable()
    {
        foreach (var tower in _towerPrefabs)
        {
            tower.ManaDecreasing += DecreaseMana;
            tower.ManaIncreasing += IncreaseMana;
        }
    }

    private void OnDisable()
    {
        foreach (var tower in _towerPrefabs)
        {
            tower.ManaDecreasing -= DecreaseMana;
            tower.ManaIncreasing -= IncreaseMana;
        }

        for (int i = 0; i < _spawnedEnemies.Count; i++)
            _spawnedEnemies.Dequeue().Died -= IncreaseMana;
    }

    public void SubscribeInstance(Enemy enemy)
    {
        _spawnedEnemies.Enqueue(enemy);
        //Увеличивает ману на два, почему?
        enemy.Died += IncreaseMana;
    }

    private void DecreaseMana(float decreaseAmount)
    {
        _resource = Mathf.Clamp(_resource - Convert.ToInt32(Mathf.Round(decreaseAmount)), 0, _maxResource);
        InvokeAmountChangedEvent();
    }

    private void IncreaseMana(float increaseAmount)
    {
        _resource = Mathf.Clamp(_resource + Convert.ToInt32(Mathf.Round(increaseAmount)), 0, _maxResource);
        InvokeAmountChangedEvent();
    }

    private void IncreaseMana(Unit unit)
    {
        int income;
        if (unit.TryGetComponent(out Enemy enemy))
            income = enemy.IncomeFromDeath;
        else
            income = 0;

        _resource = Mathf.Clamp(_resource + income, 0, _maxResource);
        InvokeAmountChangedEvent();
    }
}