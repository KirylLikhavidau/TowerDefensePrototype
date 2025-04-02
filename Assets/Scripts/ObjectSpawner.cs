using UnityEngine;
using System.Collections;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] protected ObjectPool _pool;
    [SerializeField] protected Transform _spawnPoint;
    [SerializeField] private float _delay;

    private IEnumerator SpawnObjects()
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
        var obj = _pool.GetObject();
        obj.gameObject.SetActive(true);
        obj.transform.position = _spawnPoint.position;
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(SpawnObjects));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(SpawnObjects));
    }
}
