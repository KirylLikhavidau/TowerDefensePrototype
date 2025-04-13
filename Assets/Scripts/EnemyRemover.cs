using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRemover : ObjectRemover
{
    [SerializeField] private HealthZone _zone;

    private Queue<Enemy> _enemies = new Queue<Enemy>();

    public void SubscribeInstance(Enemy enemy)
    {
        _enemies.Enqueue(enemy);
        enemy.Died += DelayRemoving;
    }

    private void OnEnable() { _zone.EnemyMissed += RemoveObject; }

    protected override void OnDisable()
    {
        _zone.EnemyMissed -= RemoveObject;
        for (int i = 0; i < _enemies.Count; i++)
            _enemies.Dequeue().Died -= DelayRemoving;
    }

    private void DelayRemoving(Unit enemy)
    {
        StartCoroutine(nameof(Remove), enemy);
    }

    private IEnumerator Remove(Enemy enemy)
    {
        yield return new WaitForSeconds(enemy.DyingTime);
        RemoveObject(enemy);
        yield break;
    }
}
