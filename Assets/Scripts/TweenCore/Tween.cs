﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTween
{

    public abstract class Tween
    {
        [SerializeField] protected float _duration;
        [SerializeField] protected float _delay;
        protected bool _isAnimating = false;
        protected int _isForward = 1; // 1 - forward, -1 - rewind
        protected bool _startFromCurrent = false;
        protected Ease _ease = Ease.Linear;
        protected AnimationCurve _curve = null;

        protected Action onUpdate = null;
        protected Action onComplete = null;
        protected Action onPause = null;
        protected Action onPlay = null;
        protected Action onRewind = null;

        protected Func<float, bool> EndCondition;
        protected Func<float, float> CurrentEaseMethod = null;
        protected Func<float, float> EaseForwardMethod = null;
        protected Func<float, float> EaseRewindMethod = null;

        //public abstract Tween SetFrom(object from);
        //public abstract Tween SetTo(object to);

        public virtual Tween OnUpdate(Action a)
        {
            onUpdate += a;
            return this;
        }
        public virtual Tween OnComplete(Action a)
        {
            onComplete += a;
            return this;
        }
        public virtual Tween OnPause(Action a)
        {
            onPause += a;
            return this;
        }
        public virtual Tween OnPlay(Action a)
        {
            onPlay += a;
            return this;
        }

        public abstract void Tick(float timeStep);

        public virtual Tween SetDuration(float duration)
        {
            _duration = duration;
            return this;
        }
        public virtual Tween SetDelay(float delay)
        {
            _delay = delay;
            return this;
        }
        public float GetDuration() => _duration;
        public float GetDelay() => _delay;

        public virtual Tween SetForwardEase(Ease ease)
        {
            _ease = ease;
            EaseForwardMethod = EaseFunctions[_ease];
            return this;
        }
        public virtual Tween SetForwardEase(AnimationCurve curve)
        {
            _ease = Ease.Custom;
            _curve = curve;
            EaseForwardMethod = EvaluateAnimationCurve;
            return this;
        }

        public virtual Tween SetRewindEase(Ease ease)
        {
            _ease = ease;
            EaseRewindMethod = EaseFunctions[_ease];
            return this;
        }
        public virtual Tween SetRewindEase(AnimationCurve curve)
        {
            _ease = Ease.Custom;
            _curve = curve;
            EaseRewindMethod = EvaluateAnimationCurve;
            return this;
        }

        public virtual Tween Stop()
        {
            _isAnimating = false;
            onPause?.Invoke();
            return this;
        }
        public virtual Tween Play()
        {
            _isAnimating = true;
            onPlay?.Invoke();
            return this;
        }
        public virtual Tween Forward()
        {
            _isForward = 1;
            CurrentEaseMethod = EaseForwardMethod;
            EndCondition = (x) => ((x) >= 1.0f);
            return this;
        }
        public virtual Tween Rewind()
        {
            _isForward = -1;
            CurrentEaseMethod = EaseRewindMethod;
            EndCondition = (x) => ((x) < 0.0f);
            return this;
        }

        public virtual Tween StartFromCurrent(bool b)
        {
            _startFromCurrent = b;
            return this;
        }

        protected float EvaluateAnimationCurve(float t)
        {
            if (_curve != null && _curve.length >= 2)
                return _curve.Evaluate(t);
            return 0.0f;
        }

        protected Action<float> OnEvaluate;
        public void EvaluateValue(float t) =>
            OnEvaluate?.Invoke(t);

        public static readonly Dictionary<Ease, Func<float, float>> EaseFunctions = new Dictionary<Ease, Func<float, float>>()
        {
            [Ease.Linear] = MathfEx.Linear,
            [Ease.InBack] = MathfEx.EaseInBack,
            [Ease.InBounce] = MathfEx.EaseInBounce,
            [Ease.InCirc] = MathfEx.EaseInCirc,
            [Ease.InCubic] = MathfEx.EaseInCubic,
            [Ease.InElastic] = MathfEx.EaseInElastic,
            [Ease.InExpo] = MathfEx.EaseInExpo,
            [Ease.InOutBack] = MathfEx.EaseInOutBack,
            [Ease.InOutBounce] = MathfEx.EaseInOutBounce,
            [Ease.InOutCirc] = MathfEx.EaseInOutCirc,
            [Ease.InOutCubic] = MathfEx.EaseInOutCubic,
            [Ease.InOutElastic] = MathfEx.EaseInOutElastic,
            [Ease.InOutExpo] = MathfEx.EaseInOutExpo,
            [Ease.InOutQuad] = MathfEx.EaseInOutQuad,
            [Ease.InOutQuart] = MathfEx.EaseInOutQuart,
            [Ease.InOutQuint] = MathfEx.EaseInOutQuint,
            [Ease.InOutSine] = MathfEx.EaseInOutSine,
            [Ease.InQuad] = MathfEx.EaseInQuad,
            [Ease.InQuart] = MathfEx.EaseInQuart,
            [Ease.InQuint] = MathfEx.EaseInQuint,
            [Ease.InSine] = MathfEx.EaseInSine,
            [Ease.OutBack] = MathfEx.EaseOutBack,
            [Ease.OutBounce] = MathfEx.EaseOutBounce,
            [Ease.OutCirc] = MathfEx.EaseOutCirc,
            [Ease.OutCubic] = MathfEx.EaseOutCubic,
            [Ease.OutElastic] = MathfEx.EaseOutElastic,
            [Ease.OutExpo] = MathfEx.EaseOutExpo,
            [Ease.OutQuad] = MathfEx.EaseOutQuad,
            [Ease.OutQuart] = MathfEx.EaseOutQuart,
            [Ease.OutQuint] = MathfEx.EaseOutQuint,
            [Ease.OutSine] = MathfEx.EaseOutSine
        };
    }

    public abstract class Tween<V> : Tween
    {
        protected V _from;
        protected V _to;

        protected Action<V> OnEvaluateComplete;
        protected Func<V> ValueOnBegin;

        public virtual Tween<V> SetFrom(V from)
        {
            _from = from;
            return this;
        }

        public virtual Tween<V> SetTo(V to)
        {
            _to = to;
            return this;
        }

        // use inverse lerp to get time 
        public virtual float GetTime(V v)
        {
            return 0.0f;
        }

        //public abstract V EvaluateValue(float t);
    }

    public abstract class Tween<T, V> : Tween<V> //where T : UnityEngine.Object
    {
        protected T _componentToAnimate; // wstapnie niech bedzie tylko component

        public Tween()
        {
            _ease = Ease.Linear;
            EaseRewindMethod = EaseFunctions[_ease];
            EaseForwardMethod = EaseFunctions[_ease];
            CurrentEaseMethod = EaseForwardMethod;// EaseForwardMethod = EaseFunctions[_ease];
        }

        public virtual Tween<T, V> SetComponent(T t)
        {
            _componentToAnimate = t;
            return this;
        }

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
