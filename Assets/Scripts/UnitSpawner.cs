using UnityEngine;
using System.Collections;

public abstract class UnitSpawner : MonoBehaviour
{
    [SerializeField] protected ObjectPool _pool;
    [SerializeField] protected Transform _spawnPoint;
    [SerializeField] private float _delay;

    private IEnumerator SpawnUnits()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    protected abstract void Spawn();

    private void OnEnable()
    {
        StartCoroutine(nameof(SpawnUnits));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(SpawnUnits));
    }
}
