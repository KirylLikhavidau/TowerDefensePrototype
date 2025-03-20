using System;
using UnityEngine;

public class EnemyRemover : UnitRemover
{
    [SerializeField] private HealthZone _zone;

    protected override void OnEnable()
    {
        _zone.EnemyMissed += RemoveUnit;
        _unitPrefab.Died += RemoveUnit;
    }

    protected override void OnDisable()
    {
        _zone.EnemyMissed -= RemoveUnit;
        _unitPrefab.Died -= RemoveUnit;
    }
}
