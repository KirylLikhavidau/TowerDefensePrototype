using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField, Range(0, 100)] private float _sensetivityToDamage;
    [SerializeField, Range(1, 20)] private int _damageToPlayer;
    [SerializeField, Range(0, 100)] private int _incomeFromDeath;

    public int IncomeFromDeath => _incomeFromDeath;
    public int DamageToPlayer => _damageToPlayer;

    protected override void TakeDamage(float damage)
    {
        _health -= damage / 100 * _sensetivityToDamage ;

        if (_health <= 0)
            Die();
    }
}
