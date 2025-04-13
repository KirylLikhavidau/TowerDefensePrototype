using UnityEngine;

public class ArcherTowerUIValidator : MonoBehaviour
{
    [SerializeField] private ArcherTower[] _towers;
    [SerializeField] private Mana _mana;

    private void OnEnable()
    {
        foreach (var tower in _towers)
        {
            tower.TowerSold += ValidateButtons;
            tower.TowerUpgraded += ValidateButtons;
        }    
    }

    private void OnDisable()
    {
        foreach (var tower in _towers)
        {
            tower.TowerSold += ValidateButtons;
            tower.TowerUpgraded += ValidateButtons;
        }
    }

    private void ValidateButtons()
    {
        foreach (var tower in _towers)
        {
            if (tower.gameObject.TryGetComponent(out ArcherTowerUIView towerUIView))
            {
                if (_mana.Amount < tower.CurrentUpgradeCost)
                {
                    towerUIView.TurnOffUpgradeButtons();
                }
                else if (_mana.Amount >= tower.CurrentUpgradeCost)
                {
                    towerUIView.TurnOnUpgradeButtons();
                }
            }
        }
    }
}
