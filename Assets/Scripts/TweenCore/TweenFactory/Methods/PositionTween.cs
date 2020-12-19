namespace UnityTween.TweenFactory
{
    public class PositionTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;

            var tween = new UnityTweenPosition(data.Target.transform, data.Vector, data.IsAdditive)
                      .SetDelay(data.Delay)
                      .SetDuration(data.Duration);
            if (curveExist) tween.SetForwardEase(data.Curve);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
