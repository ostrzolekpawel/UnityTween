using System;

namespace UnityTween
{
    /// <summary>
    /// this tween can't be created in AnimationBuilder
    /// </summary>
    public class UnityTweenUnknown : Tween
    {
        public UnityTweenUnknown(Action<float> evaluateMethod, Action completeMethod) // todo add forward and rewind ease methods
        {
            _ease = Ease.Linear;
            CurrentEaseMethod = EaseForwardMethod = EaseFunctions[_ease];

            OnEvaluate += evaluateMethod;
            onComplete += completeMethod;
        }

        public override Tween SetFrom(object from)
        {
            return this;
        }

        public override Tween SetTo(object to)
        {
            return this;
        }

        public override void Tick(float t)
        {
            if (_isAnimating)
            {
                float x = (t - _delay) / _duration;
                onUpdate?.Invoke();
                if (x > 0.0f && x <= 1.0f)
                    OnEvaluate?.Invoke(x);

                if (EndCondition != null && EndCondition.Invoke(x))
                {
                    _isAnimating = false;
                    OnEvaluate?.Invoke(_isForward > 0 ? 1 : 0);
                    onComplete?.Invoke();
                }
            }
        }
    }
}
