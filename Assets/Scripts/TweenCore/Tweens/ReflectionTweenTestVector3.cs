using UnityEngine;
using UnityTween;
using UnityTweenReflection.Generic;

namespace UnityTweenReflection
{
    public class ReflectionTweenTestVector3 : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        private TweenCore _tweenCore;

        private void Start()
        {
            _tweenCore = new TweenCore();
            var anim = new UnityTweenVector3<Transform>(_transform, "position", Vector3.one, true)
                                .SetDuration(1f).SetForwardEase(Ease.Linear);
            _tweenCore.Append(anim).SetWrap(WrapMode.PingPong);

            PlayForward();
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
