using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTween
{
    // moze tutaj powienien byc glowny kontroler czy play czy rewind?
    [Serializable]
    public class UnityTweenCore
    {
        [SerializeField][HideInInspector] private WrapMode _wrapMode = WrapMode.Default;
        private List<UnityTween> _tweens = new List<UnityTween>();
        private bool _isAnimating = false;
        private int _isForward = 1;
        private float _timer = 0.0f;
        private float _length = 0.0f;
        protected Action onUpdate = null;
        protected Action onComplete = null;
        protected Action onPause = null;
        protected Action onPlay = null;
        protected Action onRewind = null;
        public virtual UnityTweenCore OnUpdate(Action a)
        {
            onUpdate += a;
            return this;
        }
        public virtual UnityTweenCore OnComplete(Action a)
        {
            onComplete += a;
            return this;
        }
        public virtual UnityTweenCore OnPause(Action a)
        {
            onPause += a;
            return this;
        }
        public virtual UnityTweenCore OnPlay(Action a)
        {
            onPlay += a;
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

        public UnityTweenCore()
        {
            _endCondition = (x) => ((x) >= 1.0f);
        }

        public UnityTweenCore Append(UnityTween tween)
        {
            _tweens.Add(tween);
            var tweenLength = tween.GetDuration() + tween.GetDelay();
            _length = tweenLength > _length ? tweenLength : _length;
            return this;
        }

        public UnityTweenCore Remove(int index)
        {
            if (index > 0 && index < _tweens.Count)
            {
                var tweenLength = _tweens[index].GetDuration() + _tweens[index].GetDelay();
                var findNewLength = true;
                if (_length > tweenLength)
                    findNewLength = false;

                _tweens.RemoveAt(index);
                if (!findNewLength) return this;
                for (int i = 0; i < _tweens.Count; i++)
                {
                    tweenLength = _tweens[i].GetDuration() + _tweens[i].GetDelay();
                    _length = tweenLength > _length ? tweenLength : _length;
                }
            }
            return this;
        }

        public UnityTweenCore Play()
        {
            _isAnimating = true;
            onPlay?.Invoke();
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Play();
            }
            return this;
        }

        public UnityTweenCore SetForward()
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

        public UnityTweenCore SetRewind()
        {
            _isAnimating = true;
            _endCondition = (x) => ((x) < 0.0f);
            _isForward = -1;
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Reverse();
            }
            return this;
        }

        public UnityTweenCore Pause()
        {
            _isAnimating = false;
            onPause?.Invoke();
            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweens[i].Stop();
            }
            return this;
        }

        public UnityTweenCore SetWrap(WrapMode wrapMode)
        {
            _wrapMode = wrapMode;
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
                for (int i = 0; i < _tweens.Count; i++)
                {
                    _tweens[i].Tick(_timer);
                }

                onComplete?.Invoke(); // todo: czy oncomplete powiinien byc invokowany wtedy kiedy wrap jest na once, tzn konczy sie calkowicei animacja?
                WrapMethods(_wrapMode);
                //_isAnimating = false;
            }
        }
    }
}
