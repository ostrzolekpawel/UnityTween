namespace UnityTween.TweenFactory
{
    public class ScaleTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.CurveForward.length >= 2;

            var tween = new UnityTweenScale(data.Target.transform, data.Vector.To, data.IsAdditive);
            if (data.Vector.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Vector.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.CurveForward);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
