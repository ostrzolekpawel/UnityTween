namespace UnityTween.TweenFactory
{
    public class PositionTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.CurveForward.length >= 2;

            var tween = new UnityTweenPosition(data.Target.transform, data.Vector.To, data.IsAdditive);
            if (data.Vector.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Vector.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.CurveForward);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
