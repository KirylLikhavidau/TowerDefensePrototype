using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Health _healthObject;

    private void Start()
    {
        _healthText.text = _healthObject.HealthAmount.ToString();
    }

    private void OnEnable()
    {
        _healthObject.HealthChanged += ChangeHealthText;
    }

    private void OnDisable()
    {
        _healthObject.HealthChanged -= ChangeHealthText;
    }

    private void ChangeHealthText(int currentHealth)
    {
        _healthText.text = currentHealth.ToString();
    }
}