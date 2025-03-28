using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ArcherTowerUIView : MonoBehaviour
{
    [SerializeField] private ArcherTower _tower;
    [SerializeField] private Button[] _upgradeButtons;
    [SerializeField] private Button[] _wrappingButtons;
    [SerializeField] private Button[] _unwrappingButtons;
    [SerializeField] private TextMeshProUGUI _upgradePriceText;
    [SerializeField] private TextMeshProUGUI _makePriceText;
    [SerializeField] private TextMeshProUGUI _sellPriceText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _wrappingDuration;

    private void OnEnable()
    {
        foreach (var button in _wrappingButtons)
            button.onClick.AddListener(Wrap);

        foreach (var button in _unwrappingButtons)
            button.onClick.AddListener(Unwrap);

        _tower.TowerSold += ShowNewPrices;
        _tower.TowerUpgraded += ShowNewPrices;
    }

    private void OnDisable()
    {
        foreach (var button in _wrappingButtons)
            button.onClick.RemoveListener(Wrap);

        foreach (var button in _unwrappingButtons)
            button.onClick.RemoveListener(Unwrap);

        _tower.TowerSold -= ShowNewPrices;
        _tower.TowerUpgraded -= ShowNewPrices;
    }

    private void Start()
    {
        ShowNewPrices();
    }

    private void ShowNewPrices()
    {
        _sellPriceText.text = Mathf.Round(_tower.CurrentSellPrice).ToString();
        _upgradePriceText.text = Mathf.Round(_tower.CurrentUpgradeCost).ToString();
        _makePriceText.text = Mathf.Round(_tower.CurrentUpgradeCost).ToString();
    }

    public void TurnOffUpgradeButtons()
    {
        foreach (var button in _upgradeButtons)
        {
            if (button.gameObject.TryGetComponent(out Image image))
            {
                button.interactable = false;
                image.color = Color.grey;
            }
        }
    }

    public void TurnOnUpgradeButtons()
    {
        foreach (var button in _upgradeButtons)
        {
            if (button.gameObject.TryGetComponent(out Image image))
            {
                button.interactable = true;
                image.color = Color.white;
            }
        }
    }

    private void Wrap()
    {
        if (_canvasGroup.alpha == 1)
        {
            _canvasGroup.transform.DOScale(_canvasGroup.transform.localScale / 2, _wrappingDuration);
            _canvasGroup.DOFade(0, _wrappingDuration);
            _canvasGroup.interactable = false;
        }
    }

    private void Unwrap()
    {
        if (_canvasGroup.alpha == 0)
        {
            _canvasGroup.transform.DOScale(_canvasGroup.transform.localScale * 2, _wrappingDuration);
            _canvasGroup.DOFade(1, _wrappingDuration);
            _canvasGroup.interactable = true;
        }
    }
}
