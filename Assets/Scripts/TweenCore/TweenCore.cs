using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTween
{
    // moze tutaj powienien byc glowny kontroler czy play czy rewind?
    [Serializable]
    public class TweenCore
    {
        [SerializeField] [HideInInspector] private WrapMode _wrapMode = WrapMode.Default;
        private List<Tween> _tweens = new List<Tween>();
        private bool _isAnimating = false;
        private int _isForward = 1;
        private float _timer = 0.0f;
        private float _length = 0.0f;
        protected Action onUpdate = null;
        protected Action onComplete = null;
        protected Action onPause = null;
        protected Action onPlay = null;
        protected Action onRewind = null;
        public virtual TweenCore OnUpdate(Action a)
        {
            onUpdate += a;
            return this;
        }
        public virtual TweenCore OnComplete(Action a)
        {
            onComplete += a;
            return this;
        }
        public virtual TweenCore OnPause(Action a)
        {
            onPause += a;
            return this;
        }
        public virtual TweenCore OnPlay(Action a)
        {
            onPlay += a;
            return this;
        }

        /// <summary>
        /// time in percent, between 0-1
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public TweenCore SetElapsedTime(float t) // t, 0-1
        {
            t = Mathf.Clamp01(t);
            _timer = t * _length;
            return this;
        }

        private void WrapMethods(WrapMode mode)
        {
            switch (mode)
            {
                case WrapMode.PingPong:
                    _isForward *= -1;
                    if (_isForward < 0) SetRewind(); else SetForward();
                    Play();
                    _isAnimating = true;
                    break;
                case WrapMode.Loop:
                    _isAnimating = true;
                    Play();
                    _timer = _isForward > 0 ? 0.0f : _length;
                    break;
                default:
                    _isAnimating = false;
                    break;
            }
        }

        private Func<float, bool> _endCondition;

        public TweenCore()
        {
            _endCondition = (x) => ((x) >= 1.0f);
        }

        public TweenCore Append(Tween tween)
        {
            _tweens.Add(tween);
            var tweenLength = tween.GetDuration() + tween.GetDelay();
            _length = tweenLength > _length ? tweenLength : _length;
            return this;
        }
        public int TweenCount() =>
            _tweens.Count;

        public TweenCore RemoveAt(int index)
        {
            if (index >= 0 && index < _tweens.Count)
            {
                var tweenLength = _tweens[index].GetDuration() + _tweens[index].GetDelay();
                _tweens.RemoveAt(index);

                var findNewLength = false;
                if (tweenLength >= _length || TweenCount() == 0)
                    findNewLength = true;

                if (!findNewLength) return this;

                _length = 0.0f;
                for (int i = 0; i < _tweens.Count; i++)
                {
                    tweenLength = _tweens[i].GetDuration() + _tweens[i].GetDelay();
                    _length = tweenLength > _length ? tweenLength : _length;
                }
            }
            return this;
        }

        public TweenCore Remove(Tween tweenToRemove)
        {
            if (_tweens.Contains(tweenToRemove))
            {
                int idx = _tweens.FindIndex(x => x == tweenToRemove);
                RemoveAt(idx);
            }
            return this;
        }

        public TweenCore Play()
        {
            _isAnimating = true;
            onPlay?.Invoke();
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Play();
            }
            return this;
        }

        public TweenCore SetForward()
        {
            _isAnimating = true;
            _endCondition = (x) => ((x) >= 1.0f);
            _isForward = 1;
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Forward();
            }
            return this;
        }

        public TweenCore SetRewind()
        {
            _isAnimating = true;
            _endCondition = (x) => ((x) < 0.0f);
            _isForward = -1;
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Rewind();
            }
            return this;
        }

        public TweenCore Pause()
        {
            _isAnimating = false;
            onPause?.Invoke();
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Stop();
            }
            return this;
        }

        public TweenCore SetWrap(WrapMode wrapMode)
        {
            _wrapMode = wrapMode;
            return this;
        }

        public TweenCore EvaluateValue(float t)
        {
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].EvaluateValue(t);
            }
            return this;
        }

        public void Tick()
        {
            if (!_isAnimating) return;
            _timer += Time.deltaTime * _isForward;
            onUpdate?.Invoke();
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Tick(_timer);
            }

            float x = (_timer / _length);
            if (_endCondition(x))
            {
                onComplete?.Invoke(); // todo: czy oncomplete powiinien byc invokowany wtedy kiedy wrap jest na once, tzn konczy sie calkowicei animacja?
                WrapMethods(_wrapMode);
                //_isAnimating = false;
            }
        }
    }
}
