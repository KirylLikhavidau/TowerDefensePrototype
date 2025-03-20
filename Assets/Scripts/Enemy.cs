using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Enemy : Unit 
{
    [SerializeField, Range(0, 100)] private float _sensetivityToDamage;

    protected override void TakeDamage(float damage)
    {
        _health -= damage / 100 * _sensetivityToDamage ;

        if (_health <= 0)
            Die();
    }
}
