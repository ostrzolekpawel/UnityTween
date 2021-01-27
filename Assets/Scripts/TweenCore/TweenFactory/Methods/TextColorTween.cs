namespace UnityTween.TweenFactory
{
    public class TextColorTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.GetComponent<TMPro.TMP_Text>() == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenTextColor(data.Target.GetComponent<TMPro.TMP_Text>(), data.Color.To, data.IsAdditive);
            if (data.Color.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Color.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.Curve);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
