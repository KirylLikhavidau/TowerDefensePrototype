using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Button[] _wrappingButtons;
    [SerializeField] private Button[] _unwrappingButtons;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _wrappingDuration;

    private void OnEnable()
    {
        foreach (var button in _wrappingButtons)
        {
            button.onClick.AddListener(Wrap);
        }

        foreach (var button in _unwrappingButtons)
        {
            button.onClick.AddListener(Unwrap);
        }
    }

    private void OnDisable()
    {
        foreach (var button in _wrappingButtons)
        {
            button.onClick.RemoveListener(Wrap);
        }

        foreach (var button in _unwrappingButtons)
        {
            button.onClick.RemoveListener(Unwrap);
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

public interface ITowerUpgrader
{

}

public interface ITowerSeller
{

}