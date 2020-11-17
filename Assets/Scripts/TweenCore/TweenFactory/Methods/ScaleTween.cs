using UnityTween;

namespace TweenCore.TweenFactory
{
    public class ScaleTween : TweenFactory
    {
        public override UnityTween.Tween CreateTween(TweenData data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenScale(data.Target.transform, data.Vector, data.IsAdditive)
                    .SetDelay(data.Delay)
                    .SetDuration(data.Duration);
            if (curveExist) tween.SetEase(data.Curve);
            else tween.SetEase(data.Ease);

            return tween;
        }
    }
}
