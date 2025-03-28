using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class Mana : Resource
{
    [SerializeField] private List<ArcherTower> _towerPrefabs;
    [SerializeField] private Enemy _enemyPrefab;

    public int Amount => _resource;

    private void OnEnable()
    {
        foreach (var tower in _towerPrefabs)
        {
            tower.ManaDecreasing += DecreaseMana;
            tower.ManaIncreasing += IncreaseMana;
        }
        _enemyPrefab.Died += IncreaseMana;
    }

    private void OnDisable()
    {
        foreach (var tower in _towerPrefabs)
        {
            tower.ManaDecreasing -= DecreaseMana;
            tower.ManaIncreasing -= IncreaseMana;
        }
        _enemyPrefab.Died -= IncreaseMana;
    }

    private void DecreaseMana(float decreaseAmount)
    {
        _resource = Mathf.Clamp(_resource - Convert.ToInt32(Mathf.Round(decreaseAmount)), 0, _maxResource);
        InvokeAmountChangedEvent(_resource);
    }

    private void IncreaseMana(float increaseAmount)
    {
        _resource = Mathf.Clamp(_resource + Convert.ToInt32(Mathf.Round(increaseAmount)), 0, _maxResource);
        InvokeAmountChangedEvent(_resource);
    }

    private void IncreaseMana(Unit unit)
    {
        int income;
        if (unit.TryGetComponent(out Enemy enemy))
            income = enemy.IncomeFromDeath;
        else
            income = 0;

        _resource = Mathf.Clamp(_resource + income, 0, _maxResource);
        InvokeAmountChangedEvent(_resource);
    }
}