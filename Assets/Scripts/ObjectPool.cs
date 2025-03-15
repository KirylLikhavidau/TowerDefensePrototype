using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _prefab;

    private Queue<Enemy> _pool = new Queue<Enemy>();

    public IEnumerable<Enemy> PooledObjects => _pool;

    public Enemy GetObject()
    {
        if (_pool.Count == 0)
        {
            var enemy = Instantiate(_prefab);
            enemy.transform.parent = _container;

            return enemy;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Enemy enemy)
    {
        _pool.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    public void ResetPool()
    {
        for (int i = _container.childCount - 1; i >= 0; i--)
        {
            Destroy(_container.GetChild(i).gameObject);
        }

        _pool.Clear();
    }
}