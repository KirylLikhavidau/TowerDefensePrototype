using UnityEngine;
using DG.Tweening;

public class ArcherTowerView : MonoBehaviour
{
    [SerializeField] private ArcherTowerUnit _unit;
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private ArcherTower _archerTower;
    [SerializeField] private Animator _archerTowerAnimator;

    private void OnEnable()
    {
        _archerTower.TowerUpgraded += ShowTowerUpgrading;
        _archerTower.TowerSold += ShowTowerSelling;
    }

    private void OnDisable()
    {
        _archerTower.TowerUpgraded -= ShowTowerUpgrading;
        _archerTower.TowerSold -= ShowTowerSelling;
    }

    private void ShowTowerUpgrading()
    {
        int level = _archerTowerAnimator.GetInteger(AnimatorConstants.TowerAnimator.States.UpgradeLevel) + 1;
        _archerTowerAnimator.SetInteger(AnimatorConstants.TowerAnimator.States.UpgradeLevel, level);
        if (_unit.gameObject.activeSelf == false)
            _unit.gameObject.SetActive(true);
    }

    private void ShowTowerSelling()
    {
        _archerTowerAnimator.SetInteger(AnimatorConstants.TowerAnimator.States.UpgradeLevel, 0);
        _unit.gameObject.SetActive(false);
    }
}
