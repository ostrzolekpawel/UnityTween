﻿using UnityTween;

namespace TweenCore.TweenFactory
{
    public class EulerRotationTween : TweenFactory
    {
        public override UnityTween.UnityTween CreateTween(TweenData data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenRotateEuler(data.Target.transform, data.Vector, data.IsAdditive)
                    .SetDelay(data.Delay)
                    .SetDuration(data.Duration);
            if (curveExist) tween.SetEase(data.Curve);
            else tween.SetEase(data.Ease);

            return tween;
        }
    }
}
