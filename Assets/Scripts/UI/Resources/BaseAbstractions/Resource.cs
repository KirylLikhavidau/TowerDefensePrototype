using UnityEngine;
using ProjectClasses;

namespace Game.Resource
{
    public abstract class Resource<T> : MonoBehaviour
    {
        [SerializeField] private T  _resource;
        [SerializeField] protected T _maxResource;

        public ReactiveProperty<T> ResourceAmount = new();

        protected void Awake()
        {
            ResourceAmount.Value = _resource;
        }

    }
}
