using UnityTween;

namespace TweenCore.TweenFactory
{
    public interface ITweenFactory
    {
        UnityTween.Tween CreateTween(TweenData data);
    }

    public abstract class TweenFactory : ITweenFactory
    {
        public abstract UnityTween.Tween CreateTween(TweenData data);
    }
}
