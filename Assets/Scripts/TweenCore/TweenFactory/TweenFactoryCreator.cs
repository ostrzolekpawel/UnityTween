using System.Collections.Generic;

namespace UnityTween.TweenFactory
{
    public static class TweenFactoryCreator
    {
        private static readonly Dictionary<AnimationType, ITweenFactory> _tweens =
            new Dictionary<AnimationType, ITweenFactory>()
            {
                [AnimationType.Position] = new PositionTween(),
                [AnimationType.AnchoredPosition] = new AnchoredPositionTween(),
                [AnimationType.EulerRotation] = new EulerRotationTween(),
                [AnimationType.Scale] = new ScaleTween(),
                [AnimationType.QuaternionRotation] = new QuaternionRotationTween(),
                [AnimationType.ImageColor] = new ImageColorTween(),
                [AnimationType.MaterialColor] = new MaterialColorTween(),
                [AnimationType.SizeDelta] = new SizeDeltaTween(),
                [AnimationType.TextColor] = new TextColorTween(),
                [AnimationType.TextSize] = new TextSizeTween()
            };


        public static Tween CreateTween(AnimationType type, TweenOptions data)
        {
            return _tweens[type].CreateTween(data);
        }
    }
}
