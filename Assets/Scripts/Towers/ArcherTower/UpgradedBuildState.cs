using Towers;

namespace States.Tower
{
    public class UpgradedBuildState : TowerState
    {
        public UpgradedBuildState(ITower tower, AttackZone visibility)
        {
            EnemyVisibility = visibility;
            Tower = tower;
            DamageToUnits = 5;
            UpgradeCost = 130;
            SellCost = 45;
        }

        public override void Upgrade() { }

        public override void Sell()
        {
            EnemyVisibility.gameObject.SetActive(false);
            Tower.SetState(TowerStateType.NotConstructed);
        }
    }
}