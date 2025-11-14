using UnityEngine;
using UnityEngine.EventSystems;
public static class Names
{
    public static class Variable
    {
        public const string XMoveDirection = nameof(XMoveDirection);
    }

    public static class Actions
    {
        public const string Jump = nameof(Jump);
    }

    public static class SceneName
    {
        public const string AdditiveScene = nameof(AdditiveScene);
    }

    public static class Animator
    {
        public const string Moving = nameof(Moving);
        public const string YVelocity = nameof(YVelocity);
        public const string IsGrounded = nameof(IsGrounded);
        public const string Jump = nameof(Jump);
    }
}
