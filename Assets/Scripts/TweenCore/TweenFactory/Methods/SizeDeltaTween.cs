using UnityEngine;

namespace UnityTween.TweenFactory
{
    public class SizeDeltaTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.GetComponent<RectTransform>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenSizeDelta(data.Target.GetComponent<RectTransform>(), data.Vector.To, data.IsAdditive);
            if (data.Vector.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Vector.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.Curve);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
