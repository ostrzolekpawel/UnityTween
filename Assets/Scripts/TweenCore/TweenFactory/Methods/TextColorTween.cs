using UnityTween;

namespace TweenCore.TweenFactory
{
    public class TextColorTween : TweenFactory
    {
        public override UnityTween.UnityTween CreateTween(TweenData data)
        {
            if (data.Target.GetComponent<TMPro.TMP_Text>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenTextColor(data.Target.GetComponent<TMPro.TMP_Text>(), data.Color, data.IsAdditive)
                    .SetDelay(data.Delay)
                    .SetDuration(data.Duration);
            if (curveExist) tween.SetEase(data.Curve);
            else tween.SetEase(data.Ease);

            return tween;
        }
    }
}
