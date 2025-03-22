using UnityEditor.Search;
using UnityEngine;

public class ArcherTowerView : MonoBehaviour
{
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
    }

    private void ShowTowerSelling()
    {
        _archerTowerAnimator.SetInteger(AnimatorConstants.TowerAnimator.States.UpgradeLevel, 0);
    }
}