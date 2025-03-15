using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _delay;

    private IEnumerator SpawnEnemies()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        var enemy = _pool.GetObject();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = _spawnPoint.position;
    }

    public void OnEnable()
    {
        StartCoroutine(nameof(SpawnEnemies));
        _pool.ResetPool();
    }

    public void OnDisable()
    {
        StopCoroutine(nameof(SpawnEnemies));
    }
}
