using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Units.Tower;
using States;

namespace Towers
{
    public class ArcherTowerView : MonoBehaviour
    {
        [SerializeField] private ArcherTowerViewModel _viewModel;
        [SerializeField] private Animator _unitAnimator;
        [SerializeField] private Animator _archerTowerAnimator;
        [SerializeField] private Button[] _upgradeButtons;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private TextMeshProUGUI _upgradePrice;
        [SerializeField] private TextMeshProUGUI _makePrice;
        [SerializeField] private TextMeshProUGUI _sellPrice;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _wrappingDuration;
        [SerializeField] private ArcherTowerUnit _unit;

        public event Action UpgradeButtonClicked;
        public event Action SellButtonClicked;
        public event Action OpenButtonClicked;
        public event Action CloseButtonClicked;

        private void OnEnable()
        {
            foreach (var button in _upgradeButtons)
                button.onClick.AddListener(() => UpgradeButtonClicked?.Invoke());
            _sellButton.onClick.AddListener(() => SellButtonClicked?.Invoke());
            _closeButton.onClick.AddListener(() => CloseButtonClicked?.Invoke());
            _openButton.onClick.AddListener(() => OpenButtonClicked?.Invoke());

            _viewModel.SellPriceLabel.OnChanged += (value) => { _sellPrice.text = value.ToString(); };
            _viewModel.UpgradePriceLabel.OnChanged += (value) => { _upgradePrice.text = value.ToString(); };
            _viewModel.MakePriceLabel.OnChanged += (value) => { _makePrice.text = value.ToString(); };
            _viewModel.TowerUpgrading += ShowTowerUpgrading;
            _viewModel.TowerSelling += ShowTowerSelling;
            _viewModel.TurningOnUpgradeButtons += TurnOnUpgradeButtons;
            _viewModel.TurningOffUpgradeButtons += TurnOffUpgradeButtons;
            _viewModel.ShowingUIUnwrapping += ShowUnwrapping;
            _viewModel.ShowingUIWrapping += ShowWrapping;
        }

        private void OnDisable()
        {
            foreach (var button in _upgradeButtons)
                button.onClick.RemoveListener(() => UpgradeButtonClicked?.Invoke());
            _sellButton.onClick.RemoveListener(() => SellButtonClicked?.Invoke());
            _closeButton.onClick.RemoveListener(() => CloseButtonClicked?.Invoke());
            _openButton.onClick.RemoveListener(() => OpenButtonClicked?.Invoke());

            _viewModel.SellPriceLabel.OnChanged -= (value) => { _sellPrice.text = value.ToString(); };
            _viewModel.UpgradePriceLabel.OnChanged -= (value) => { _upgradePrice.text = value.ToString(); };
            _viewModel.MakePriceLabel.OnChanged -= (value) => { _makePrice.text = value.ToString(); };
            _viewModel.TowerUpgrading -= ShowTowerUpgrading;
            _viewModel.TowerSelling -= ShowTowerSelling;
            _viewModel.TurningOnUpgradeButtons -= TurnOnUpgradeButtons;
            _viewModel.TurningOffUpgradeButtons -= TurnOffUpgradeButtons;
            _viewModel.ShowingUIUnwrapping += ShowUnwrapping;
            _viewModel.ShowingUIWrapping += ShowWrapping;
        }

        private void ShowTowerUpgrading()
        {
            int level = _archerTowerAnimator.GetInteger(AnimatorStates.TowerAnimator.UpgradeLevel) + 1;
            _archerTowerAnimator.SetInteger(AnimatorStates.TowerAnimator.UpgradeLevel, level);
            if (_unit.gameObject.activeSelf == false)
                _unit.gameObject.SetActive(true);
        }

        private void ShowTowerSelling()
        {
            _archerTowerAnimator.SetInteger(AnimatorStates.TowerAnimator.UpgradeLevel, 0);
            _unit.gameObject.SetActive(false);
        }

        private void TurnOffUpgradeButtons()
        {
            foreach (var button in _upgradeButtons)
                if (button.gameObject.TryGetComponent(out Image image))
                {
                    button.interactable = false;
                    image.color = Color.grey;
                }
        }

        private void TurnOnUpgradeButtons()
        {
            foreach (var button in _upgradeButtons)
                if (button.gameObject.TryGetComponent(out Image image))
                {
                    button.interactable = true;
                    image.color = Color.white;
                }
        }

        private void ShowWrapping()
        {
            if (_canvasGroup.alpha == 1)
            {
                _canvasGroup.transform.DOScale(_canvasGroup.transform.localScale / 2, _wrappingDuration);
                _canvasGroup.DOFade(0, _wrappingDuration);
                _canvasGroup.interactable = false;
            }
        }

        private void ShowUnwrapping()
        {
            if (_canvasGroup.alpha == 0)
            {
                _canvasGroup.transform.DOScale(_canvasGroup.transform.localScale * 2, _wrappingDuration);
                _canvasGroup.DOFade(1, _wrappingDuration);
                _canvasGroup.interactable = true;
            }
        }
    }
}
