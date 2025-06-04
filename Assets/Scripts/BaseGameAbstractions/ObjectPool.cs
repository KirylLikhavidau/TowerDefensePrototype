using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private PoolObject _prefab;

        private Queue<PoolObject> _pool = new Queue<PoolObject>();

        public IEnumerable<PoolObject> PooledObjects => _pool; 

        public PoolObject GetObject()
        {
            if (_pool.Count == 0)
            {
                var obj = Instantiate(_prefab);
                obj.transform.parent = _container;

                return obj;
            }

            return _pool.Dequeue();
        }

        public void PutObject(PoolObject obj)
        {
            _pool.Enqueue(obj);
            obj.gameObject.SetActive(false);
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
}