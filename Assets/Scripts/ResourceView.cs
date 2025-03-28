using TMPro;
using UnityEngine;

public abstract class ResourceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private Resource _resourceObject;

    private void Start()
    {
        _resourceText.text = _resourceObject.ResourceAmount.ToString();
    }

    private void OnEnable()
    {
        _resourceObject.ResourceAmountChanged += ChangeResourceText;
    }

    private void OnDisable()
    {
        _resourceObject.ResourceAmountChanged -= ChangeResourceText;
    }

    private void ChangeResourceText(int currentResourceAmount)
    {
        _resourceText.text = currentResourceAmount.ToString();
    }
}