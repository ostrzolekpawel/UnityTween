using System;
using UnityEngine;

namespace UnityTween
{
    [Serializable]
    public class TweenOptions // tween options
    {
        public GameObject Target;
        public AnimationType Type;
        public float Delay;
        public float Duration;
        public bool IsAdditive;

        public Ease Ease;
        public AnimationCurve Curve;

        public AnimationData Animation;

        public VectorTweenData Vector = new VectorTweenData();
        public QuaternionTweenData Quaternion = new QuaternionTweenData();
        public ColorTweenData Color = new ColorTweenData();
        public FloatTweenData Float = new FloatTweenData();
    }

    [Serializable]
    public class AnimationOptions
    {
        public Ease Ease;
        public AnimationCurve Curve;
    }

    [Serializable]
    public class AnimationData
    {
        public AnimationOptions AnimationForward;
        public bool RewindIsDifferent;
        public AnimationOptions AnimationRewind;
    }

    [Serializable]
    public class TweenData<T>
    {
        public bool FromIsDifferentThanCurrent;
        public T From;
        public T To;
    }

    [Serializable]
    public class VectorTweenData : TweenData<Vector3> { }

    [Serializable]
    public class QuaternionTweenData : TweenData<Quaternion> { }

    [Serializable]
    public class ColorTweenData : TweenData<Color> { }

    [Serializable]
    public class FloatTweenData : TweenData<float> { }
}
