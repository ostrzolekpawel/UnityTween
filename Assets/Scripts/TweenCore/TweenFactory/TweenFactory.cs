using UnityTween;

namespace TweenCore.TweenFactory
{
    public interface ITweenFactory
    {
        UnityTween.UnityTween CreateTween(TweenData data);
    }

    public abstract class TweenFactory : ITweenFactory
    {
        public abstract UnityTween.UnityTween CreateTween(TweenData data);
    }
}
