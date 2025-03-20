using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class UnitRemover : MonoBehaviour
{
    [SerializeField] protected Unit _unitPrefab;
    [SerializeField] private ObjectPool _unitPool;

    protected abstract void OnEnable();
    protected abstract void OnDisable();

    protected void RemoveUnit(Unit unit)
    {
        _unitPool.PutObject(unit);
    }
}
