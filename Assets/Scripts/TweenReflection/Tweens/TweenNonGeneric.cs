using System;
using UnityTween;

namespace UnityTweenReflection
{
    public abstract class TweenNonGeneric<V> : Tween<V>
    {
        protected Action<object, object> _setter;
        protected Func<object, object> _getter;
        protected object _componentToAnimate;
        
        public override void Tick(float t)
        {
            if (_isAnimating)
            {
                //if (t < _delay) return; // a co jesli wracasz?
                if (_startFromCurrent)
                {
                    _from = ValueOnBegin.Invoke();
                    _startFromCurrent = false;
                }

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
