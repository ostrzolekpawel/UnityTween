using UnityEngine;

namespace UnityTween.TweenFactory
{
    public class MaterialColorTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.GetComponent<Renderer>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenMaterialColor(data.Target.GetComponent<Renderer>(), data.Color.To, data.IsAdditive);
            if (data.Color.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Color.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.Curve);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
