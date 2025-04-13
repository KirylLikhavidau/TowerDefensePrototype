using System;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    [SerializeField] protected int _resource;
    [SerializeField] protected int _maxResource;

    public int ResourceAmount => _resource;

    public event Action ResourceAmountChanged;

    protected void InvokeAmountChangedEvent()
    {
        ResourceAmountChanged?.Invoke();
    }
}
