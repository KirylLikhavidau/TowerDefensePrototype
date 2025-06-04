using TMPro;
using UnityEngine;

namespace Game.Resource
{
    public abstract class ResourceView<T> : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resourceText;
        [SerializeField] private Resource<T> _resource;

        private void Start()
        {
            _resourceText.text = _resource.ResourceAmount.Value.ToString();
        }

        private void OnEnable()
        {
            _resource.ResourceAmount.OnChanged += (value) => ChangeResourceText(value);
        }

        private void OnDisable()
        {
            _resource.ResourceAmount.OnChanged -= (value) => ChangeResourceText(value);
        }

        private void ChangeResourceText(T amount)
        {
            _resourceText.text = amount.ToString();
        }
    }
}
