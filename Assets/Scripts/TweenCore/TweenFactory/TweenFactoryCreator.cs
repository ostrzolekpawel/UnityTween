using System.Collections.Generic;
using UnityTween;

namespace TweenCore.TweenFactory
{
    public static class TweenFactoryCreator
    {
        private static readonly Dictionary<AnimationType, TweenFactory> _tweens =
            new Dictionary<AnimationType, TweenFactory>()
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


        public static UnityTween.Tween CreateTween(AnimationType type, TweenData data)
        {
            return _tweens[type].CreateTween(data);
        }
    }
}
