using Towers;

namespace States.Tower
{
    public class NotConstructedState : TowerState
    {
        public NotConstructedState(ITower tower, AttackZone visibility)
        {
            EnemyVisibility = visibility;
            Tower = tower;
            DamageToUnits = 0;
            UpgradeCost = 50;
            SellCost = 0;
        }

        public override void Upgrade()
        {
            EnemyVisibility.gameObject.SetActive(true);
            Tower.SetState(TowerStateType.Builded);
        }

        public override void Sell()
        {
            EnemyVisibility.gameObject.SetActive(false);
            Tower.SetState(TowerStateType.NotConstructed);
        }
    }
}
