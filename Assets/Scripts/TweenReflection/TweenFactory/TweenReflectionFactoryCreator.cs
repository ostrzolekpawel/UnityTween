using System.Collections.Generic;

namespace UnityTween.TweenFactory
{
    public static class TweenReflectionFactoryCreator
    {
        private static readonly Dictionary<AnimationTypeReflection, ITweenReflectionFactory> _tweens =
            new Dictionary<AnimationTypeReflection, ITweenReflectionFactory>()
            {
                //[AnimationTypeReflection.Float] = 
            };


        public static Tween CreateTween(AnimationTypeReflection type, TweenOptionsReflection data)
        {
            return _tweens[type].CreateTween(data);
        }
    }
}
