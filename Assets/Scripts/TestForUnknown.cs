using UnityEngine;
using UnityEngine.UI;

namespace UnityTween.Tests
{
    public class TestForUnknown : MonoBehaviour
    {
        [SerializeField] private AnimationBuilder _builder;
        [SerializeField] private Image _image;
        private float _firstValue = 0.0f;
        private float _lastValue = 1.0f;

        private void Start()
        {
            var tweenCore = _builder.TweenCore;
            var unknownTween = new UnityTweenUnknown(Fill, Complete)
                    .SetDuration(2.0f);
            tweenCore.Append(unknownTween);
            _builder.PlayForward();
        }

        private void Fill(float t)
        {
            _image.fillAmount = Mathf.Lerp(_firstValue, _lastValue, t);
        }

        private void Complete()
        {
            _image.fillAmount = 1.0f;
        }
    }
}
