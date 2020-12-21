namespace UnityTween.TweenFactory
{
    public class TextSizeTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.GetComponent<TMPro.TMP_Text>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenTextSize(data.Target.GetComponent<TMPro.TMP_Text>(), data.Float.To, data.IsAdditive);
            if (data.Float.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Float.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.Curve);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
