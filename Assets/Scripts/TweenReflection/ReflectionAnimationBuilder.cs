using System;
using System.Collections.Generic;
using UnityTween.TweenFactory;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTween
{

    [Serializable]
    //[ExecuteInEditMode]
    public class ReflectionAnimationBuilder : MonoBehaviour // todo, moze zmienic nazwe na tweenbuilder
    {
        [SerializeField] [HideInInspector] private WrapMode _wrapMode;
        public WrapMode WrapMode
        {
            get => _wrapMode;
            set {
                _wrapMode = value;
                _tweenCore?.SetWrap(value);
            }
        }
        [SerializeField] [HideInInspector] public bool PlayOnStart;
        [SerializeField] [HideInInspector] public bool LoadOnStart = true;

        [SerializeField] private List<TweenOptionsReflection> _tweenDatas = new List<TweenOptionsReflection>();
        private List<Tween> _tweens = new List<Tween>();

        private TweenCore _tweenCore = null;
        public TweenCore TweenCore { get => _tweenCore; set => _tweenCore = value; }

        public bool IsInit { get; set; } = false;

        private void Awake()
        {
            if (LoadOnStart)
            {
                LoadTweens();
                if (PlayOnStart)
                    _tweenCore.Play();
            }
        }

        public void LoadTweens()
        {
            _tweenCore = new TweenCore();
            //_tweens.Clear();
            _tweenDatas.ForEach(x => AddTween(x));

            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweenCore.Append(_tweens[i]);
            }
            _tweenCore.SetWrap(WrapMode).SetForward();
            IsInit = true;
        }

        private void Reset()
        {
            IsInit = false; // chyba z automatu
        }

        public void AddTween(Tween tween)
        {
            _tweens.Add(tween);
        }

        public void AddTween(TweenOptionsReflection data)
        {
            //var tween = TweenFactoryCreator.CreateTween(data.AnimationType, data);
            var tween = TweenReflectionFactoryCreator.CreateTween(AnimationTypeReflection.Vector3, null);
            AddTween(tween);
        }

        public void PlayForward()
        {
            _tweenCore.SetForward().Play();
        }

        public void PlayRewind()
        {
            _tweenCore.SetRewind().Play();
        }

        public void Play()
        {
            _tweenCore.Play();
        }
        public void Stop()
        {
            _tweenCore.Pause();
        }

        private void Update()
        {
            _tweenCore?.Tick();
        }
    }
}
