using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Unit _prefab;

    private Queue<Unit> _pool = new Queue<Unit>();

    public IEnumerable<Unit> PooledObjects => _pool; 

    public Unit GetObject()
    {
        if (_pool.Count == 0)
        {
            var unit = Instantiate(_prefab);
            unit.transform.parent = _container;

            return unit;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Unit unit)
    {
        _pool.Enqueue(unit);
        unit.gameObject.SetActive(false);
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