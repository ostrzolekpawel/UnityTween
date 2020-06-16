using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTween
{
    public enum AnimationType
    {
        Translate,
        RectTranslate,
        EulerRotation,
        Scale,
        QuaternionRotation,
        ImageColor,
        MaterialColor
    }

    [Serializable]
    public class TweenData
    {
        public GameObject Target;
        public AnimationType Type;
        public float Delay;
        public float Duration;
        public bool IsAdditive;
        public Vector3 Vector;
        public Quaternion Quaternion;
        public Color Color;
        public Ease Ease;
        public AnimationCurve Curve;
    }

    [Serializable]
    [ExecuteInEditMode]
    public class AnimationBuilder : MonoBehaviour // todo, moze zmienic nazwe na tweenbuilder
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

        [SerializeField] private List<TweenData> _tweenDatas = new List<TweenData>();
        private List<UnityTween> _tweens = new List<UnityTween>();

        private UnityTweenCore _tweenCore = null;
        public UnityTweenCore TweenCore { get => _tweenCore; set => _tweenCore = value; }

        public bool IsInit { get; set; } = false;

        private void Start()
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
            _tweenCore = new UnityTweenCore();
            //_tweens.Clear();
            _tweenDatas.ForEach(x => AddTween(x));

            for (int i = 0; i < _tweens.Count; i++)
            {
                _tweenCore.Append(_tweens[i]);
            }
            _tweenCore.SetWrap(WrapMode);
            IsInit = true;
        }

        private void Reset()
        {
            IsInit = false; // chyba z automatu
        }

        public void AddTween(UnityTween tween)
        {
            _tweens.Add(tween);
        }

        public void AddTween(TweenData data)
        {
            if (data.Target == null) return;
            bool curveExist = (data.Ease == Ease.Custom) && data.Curve.length >= 2;
            switch (data.Type)
            {
                case AnimationType.Translate: //
                    if (data.Target.transform == null) break;
                    var tweenTranslate = new UnityTweenTranslate(data.Target.transform, data.Vector, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenTranslate.SetEase(data.Curve);
                    else tweenTranslate.SetEase(data.Ease);
                    AddTween(tweenTranslate);
                    break;
                case AnimationType.RectTranslate: //
                    if (data.Target.GetComponent<RectTransform>() == null) break;
                    var tweenRectTranslate = new UnityTweenRectTranslate(data.Target.GetComponent<RectTransform>(), data.Vector, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenRectTranslate.SetEase(data.Curve);
                    else tweenRectTranslate.SetEase(data.Ease);
                    AddTween(tweenRectTranslate);
                    break;
                case AnimationType.EulerRotation: //
                    if (data.Target.transform == null) break;
                    var tweenEuler = new UnityTweenRotateEuler(data.Target.transform, data.Vector, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenEuler.SetEase(data.Curve);
                    else tweenEuler.SetEase(data.Ease);
                    AddTween(tweenEuler);
                    break;
                case AnimationType.Scale: //
                    if (data.Target.transform == null) break;
                    var tweenScale = new UnityTweenScale(data.Target.transform, data.Vector, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenScale.SetEase(data.Curve);
                    else tweenScale.SetEase(data.Ease);
                    AddTween(tweenScale);
                    break;
                case AnimationType.QuaternionRotation: //
                    if (data.Target.transform == null) break;
                    var tweenQuaternion = new UnityTweenRotateQuaternion(data.Target.transform, data.Quaternion, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenQuaternion.SetEase(data.Curve);
                    else tweenQuaternion.SetEase(data.Ease);
                    AddTween(tweenQuaternion);
                    break;
                case AnimationType.ImageColor: //
                    if (data.Target.GetComponent<Image>() == null) break;
                    var tweenImage = new UnityTweenImageColor(data.Target.GetComponent<Image>(), data.Color, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenImage.SetEase(data.Curve);
                    else tweenImage.SetEase(data.Ease);
                    AddTween(tweenImage);
                    break;
                case AnimationType.MaterialColor:
                    if (data.Target.GetComponent<Renderer>() == null) break;
                    var tweenMaterial = new UnityTweenMaterialColor(data.Target.GetComponent<Renderer>(), data.Color, data.IsAdditive)
                            .SetDelay(data.Delay)
                            .SetDuration(data.Duration);
                    if (curveExist) tweenMaterial.SetEase(data.Curve);
                    else tweenMaterial.SetEase(data.Ease);
                    AddTween(tweenMaterial);
                    break;
            }
        }

        public void PlayForward()
        {
            _tweenCore.SetForward().Play();
        }

        public void PlayReverse()
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
