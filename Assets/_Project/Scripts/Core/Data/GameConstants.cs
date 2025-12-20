public static class GameConstants
{
    public static class Variables
    {
        public const string XMoveDirection = nameof(XMoveDirection);
        public const string Health = nameof(Health);
    }

    public static class Actions
    {
        public const string Jump = nameof(Jump);
        public const string Kill = nameof(Kill);
    }

    public static class SceneNames
    {
        public const string AdditiveScene = nameof(AdditiveScene);
    }

    public static class AnimatorKeys
    {
        public const string Moving = nameof(Moving);
        public const string YVelocity = nameof(YVelocity);
        public const string IsGrounded = nameof(IsGrounded);
        public const string Jump = nameof(Jump);
        public const string Death = nameof(Death);
    }

    public static class ObjectTypes
    {
        public const string Damageable = nameof(Damageable);
        public const string PickUp = nameof(PickUp);
        public const string Coin = nameof(Coin);
    }
}
