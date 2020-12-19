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
                _componentToAnimate.rotation = Quaternion.Lerp(_from, _to, CurrentEaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.rotation = x;
            };
            ValueOnBegin += () => _componentToAnimate.rotation;
        }

        public override Tween SetFrom(object from)
        {
            if (from is Quaternion quaternion)
                _from = quaternion;
            return this;
        }

        public override Tween SetTo(object to)
        {
            if (to is Quaternion quaternion)
                _to = quaternion;
            return this;
        }
    }
}
