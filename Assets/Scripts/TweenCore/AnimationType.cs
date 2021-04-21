using System;

namespace UnityTween
{
    public enum AnimationType
    {
        Position,
        AnchoredPosition,
        EulerRotation,
        Scale,
        QuaternionRotation,
        ImageColor,
        MaterialColor,
        SizeDelta,
        TextColor,
        TextSize
    }

    [Serializable]
    public enum AnimationTypeReflection
    {
        Float,
        Vector2,
        Vector3,
        Quaternion
    }
}
