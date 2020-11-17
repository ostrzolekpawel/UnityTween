using UnityEngine.UI;
using UnityTween;

namespace TweenCore.TweenFactory
{
    public class ImageColorTween : TweenFactory
    {
        public override UnityTween.Tween CreateTween(TweenData data)
        {
            if (data.Target.GetComponent<Image>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenImageColor(data.Target.GetComponent<Image>(), data.Color, data.IsAdditive)
                    .SetDelay(data.Delay)
                    .SetDuration(data.Duration);
            if (curveExist) tween.SetEase(data.Curve);
            else tween.SetEase(data.Ease);

            return tween;
        }
    }
}
