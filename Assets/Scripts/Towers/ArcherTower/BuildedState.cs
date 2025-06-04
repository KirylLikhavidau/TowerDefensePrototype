using Towers;

namespace States.Tower
{
    public class BuildedState : TowerState
    {
        public BuildedState(ITower tower, AttackZone visibility)
        {
            EnemyVisibility = visibility;
            Tower = tower;
            DamageToUnits = 3;
            UpgradeCost = 90;
            SellCost = 25;
        }

        public override void Upgrade()
        {
            Tower.SetState(TowerStateType.UpgradedBuild);
        }

        public override void Sell()
        {
            EnemyVisibility.gameObject.SetActive(false);
            Tower.SetState(TowerStateType.NotConstructed);
        }
    }
}
