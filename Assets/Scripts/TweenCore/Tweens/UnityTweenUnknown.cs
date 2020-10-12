using System;

namespace UnityTween
{
    /// <summary>
    /// this tween can't be created in AnimationBuilder
    /// </summary>
    public class UnityTweenUnknown : UnityTween
    {
        public UnityTweenUnknown(Action<float> evaluateMethod, Action completeMethod)
        {
            _ease = Ease.Linear;
            EaseMethod = EaseFunctions[_ease];

            OnEvaluate += evaluateMethod;
            onComplete += completeMethod;
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
