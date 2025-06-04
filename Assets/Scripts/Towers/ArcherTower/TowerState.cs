using Towers;

namespace States.Tower
{
    public abstract class TowerState
    {
        public int DamageToUnits { get; protected set; }
        public int UpgradeCost { get; protected set; }
        public int SellCost { get; protected set; }

        protected ITower Tower;
        protected AttackZone EnemyVisibility;

        public abstract void Upgrade();
        public abstract void Sell();
    }
}
