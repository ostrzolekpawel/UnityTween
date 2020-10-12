using System;
using UnityEngine;

namespace UnityTween
{
    [Serializable]
    public class TweenData
    {
        public GameObject Target;
        public AnimationType Type;
        public float Delay;
        public float Duration;
        public bool IsAdditive;
        public Vector3 Vector;
        public Quaternion Quaternion;
        public Color Color;
        public float Number;
        public Ease Ease;
        public AnimationCurve Curve;
    }
}
