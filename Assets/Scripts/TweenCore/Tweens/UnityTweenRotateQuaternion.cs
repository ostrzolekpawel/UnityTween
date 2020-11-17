using UnityEngine;

namespace UnityTween
{
    public class UnityTweenRotateQuaternion : Tween<Transform, Quaternion>
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
}
