namespace States
{
    public static class AnimatorStates
    {
        public static class EnemyAnimator
        {
            public const string Dead = nameof(Dead);
            public const string RightMotion = nameof(RightMotion);
            public const string LeftMotion = nameof(LeftMotion);
            public const string DownMotion = nameof(DownMotion);
            public const string UpMotion = nameof(UpMotion);
        }

        public static class TowerAnimator
        {
            public const string UpgradeLevel = nameof(UpgradeLevel);
        }
    }
}
