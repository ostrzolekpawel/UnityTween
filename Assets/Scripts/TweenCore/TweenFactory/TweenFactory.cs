namespace UnityTween.TweenFactory
{
    public interface ITweenFactory
    {
        Tween CreateTween(TweenOptions data);
    }

    public abstract class TweenFactory : ITweenFactory
    {
        public abstract Tween CreateTween(TweenOptions data);
    }
}
