namespace UnityTween.TweenFactory
{
    public class QuaternionRotationTween : TweenFactory
    {
        public override Tween CreateTween(TweenOptions data)
        {
            if (data.Target.transform == null) return null;
            bool curveExist = (data.Ease == Ease.Custom) && data.CurveForward.length >= 2;

            var tween = new UnityTweenRotateQuaternion(data.Target.transform, data.Quaternion.To, data.IsAdditive);
            if (data.Quaternion.FromIsDifferentThanCurrent)
                tween.SetFrom(data.Quaternion.From);

            tween.SetDelay(data.Delay).SetDuration(data.Duration);

            if (curveExist) tween.SetForwardEase(data.CurveForward);
            else tween.SetForwardEase(data.Ease);

            return tween;
        }
    }
}
