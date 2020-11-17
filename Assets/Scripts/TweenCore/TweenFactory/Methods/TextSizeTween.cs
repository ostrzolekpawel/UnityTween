using UnityTween;

namespace TweenCore.TweenFactory
{
    public class TextSizeTween : TweenFactory
    {
        public override UnityTween.Tween CreateTween(TweenData data)
        {
            if (data.Target.GetComponent<TMPro.TMP_Text>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenTextSize(data.Target.GetComponent<TMPro.TMP_Text>(), data.Number, data.IsAdditive)
                    .SetDelay(data.Delay)
                    .SetDuration(data.Duration);
            if (curveExist) tween.SetEase(data.Curve);
            else tween.SetEase(data.Ease);

            return tween;
        }
    }
}
