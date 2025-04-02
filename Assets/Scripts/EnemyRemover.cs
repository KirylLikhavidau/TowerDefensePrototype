using System;
using UnityEngine;

public class EnemyRemover : ObjectRemover
{
    [SerializeField] private HealthZone _zone;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = (Enemy)_objectPrefab;
    }

    protected override void OnEnable()
    {
        _zone.EnemyMissed += RemoveObject;
        _enemy.Died += RemoveObject;
    }

    protected override void OnDisable()
    {
        _zone.EnemyMissed -= RemoveObject;
        _enemy.Died -= RemoveObject;
    }
}
