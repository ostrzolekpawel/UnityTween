using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTween
{
    public enum Ease
    {
        Custom,
        Linear,
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InElastic,
        OutElastic,
        InOutElastic,
        InBack,
        OutBack,
        InOutBack,
        InBounce,
        OutBounce,
        InOutBounce
    }

    public abstract class UnityTween
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
        protected Func<float, float> EaseMethod = null;

        public virtual UnityTween OnUpdate(Action a)
        {
            onUpdate += a;
            return this;
        }
        public virtual UnityTween OnComplete(Action a)
        {
            onComplete += a;
            return this;
        }
        public virtual UnityTween OnPause(Action a)
        {
            onPause += a;
            return this;
        }
        public virtual UnityTween OnPlay(Action a)
        {
            onPlay += a;
            return this;
        }

        public abstract void Tick(float timeStep);

        public virtual UnityTween SetDuration(float duration)
        {
            _duration = duration;
            return this;
        }
        public virtual UnityTween SetDelay(float delay)
        {
            _delay = delay;
            return this;
        }
        public float GetDuration() => _duration;
        public float GetDelay() => _delay;

        public virtual UnityTween SetEase(Ease ease)
        {
            _ease = ease;
            EaseMethod = EaseFunctions[_ease];
            return this;
        }
        public virtual UnityTween SetEase(AnimationCurve curve)
        {
            _ease = Ease.Custom;
            _curve = curve;
            EaseMethod = EvaluateAnimationCurve;
            return this;
        }
        public virtual UnityTween Stop()
        {
            _isAnimating = false;
            onPause?.Invoke();
            return this;
        }
        public virtual UnityTween Play()
        {
            _isAnimating = true;
            onPlay?.Invoke();
            return this;
        }
        public virtual UnityTween Forward()
        {
            _isForward = 1;
            EndCondition = (x) => ((x) >= 1.0f);
            return this;
        }
        public virtual UnityTween Reverse()
        {
            _isForward = -1;
            EndCondition = (x) => ((x) < 0.0f);
            return this;
        }

        public virtual UnityTween StartFromCurrent(bool b)
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

        // raczej nie musza byc publiczne
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
    
    public abstract class UnityTween<V> : UnityTween
    {
        protected V _from;
        protected V _to;

        protected Action<float> OnEvaluate;
        protected Action<V> OnEvaluateComplete;
        protected Func<V> ValueOnBegin;

        public virtual UnityTween<V> SetFrom(V from)
        {
            _from = from;
            return this;
        }

        public virtual UnityTween<V> SetTo(V to)
        {
            _to = to;
            return this;
        }
    }

    public abstract class UnityTween<T, V> : UnityTween<V> where T : UnityEngine.Object
    {
        protected T _componentToAnimate; // wstapnie niech bedzie tylko component

        public UnityTween()
        {
            _ease = Ease.Linear;
            EaseMethod = EaseFunctions[_ease];
        }

        public virtual UnityTween<T, V> SetComponent(T t)
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

    public class UnityTweenTranslate : UnityTween<Transform, Vector3>
    {
        public UnityTweenTranslate(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.position;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.position = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.position = x;
            };

            ValueOnBegin += () => _componentToAnimate.position;
        }
    }

    public class UnityTweenScale : UnityTween<Transform, Vector3>
    {
        public UnityTweenScale(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.localScale;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.localScale = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.localScale = x;
            };
            ValueOnBegin += () => _componentToAnimate.localScale;
        }
    }

    public class UnityTweenRotateEuler : UnityTween<Transform, Vector3>
    {
        public UnityTweenRotateEuler(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.eulerAngles;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.eulerAngles = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.eulerAngles = x;
            };
            ValueOnBegin += () => _componentToAnimate.eulerAngles;
        }
    }

    public class UnityTweenRotateQuaternion : UnityTween<Transform, Quaternion>
    {
        public UnityTweenRotateQuaternion(Transform transform, Quaternion endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.rotation;
            _to = isAdditive ? _from * endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.rotation = Quaternion.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.rotation = x;
            };
            ValueOnBegin += () => _componentToAnimate.rotation;
        }
    }

    public class UnityTweenImageColor : UnityTween<Image, Color>
    {
        public UnityTweenImageColor(Image image, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = image;
            _from = image.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.color = Color.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.color;
        }
    }

    public class UnityTweenMaterialColor : UnityTween<Renderer, Color>
    {
        public UnityTweenMaterialColor(Renderer renderer, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = renderer;
            _from = _componentToAnimate.material.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.material.color = Color.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.material.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.material.color;
        }
    }

    public class UnityTweenRectTranslate : UnityTween<RectTransform, Vector3>
    {
        public UnityTweenRectTranslate(RectTransform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.anchoredPosition;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.anchoredPosition = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.anchoredPosition = x;
            };

            ValueOnBegin += () => _componentToAnimate.anchoredPosition;
        }
    }
}
