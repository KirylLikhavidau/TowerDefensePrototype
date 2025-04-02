using UnityEngine;

public abstract class ObjectRemover : MonoBehaviour
{
    [SerializeField] protected PoolObject _objectPrefab;
    [SerializeField] private ObjectPool _objectPool;

    protected abstract void OnEnable();
    protected abstract void OnDisable();

    protected void RemoveObject(PoolObject obj)
    {
        _objectPool.PutObject(obj);
    }
}
