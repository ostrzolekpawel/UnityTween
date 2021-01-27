using UnityEngine;
using UnityTween;

namespace UnityTweenReflection
{
    public class ReflectionTweenTest : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private TweenCore _tweenCore;

        private void Start()
        {
            _tweenCore = new TweenCore();
            var anim = new UnityTweenFloat<CanvasGroup>(_canvasGroup, "alpha", 0.0f)
                                .SetDuration(1f).SetForwardEase(Ease.Linear);
            _tweenCore.Append(anim).SetWrap(WrapMode.PingPong);
        }

        private void Update()
        {
            _tweenCore.Tick();
        }

        [ContextMenu("PlayForward")]
        private void PlayForward()
        {
            _tweenCore.SetForward().Play();
        }

        [ContextMenu("PlayRewind")]
        private void PlayRewind()
        {
            _tweenCore.SetRewind().Play();
        }
    }
}
