using UnityEngine;

namespace Pool
{
    public abstract class ObjectRemover : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;

        protected abstract void OnDisable();

        protected void RemoveObject(PoolObject obj)
        {
            _objectPool.PutObject(obj);
        }
    }
}
