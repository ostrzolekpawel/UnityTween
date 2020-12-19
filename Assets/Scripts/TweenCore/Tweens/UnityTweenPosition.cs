using UnityEngine;

namespace UnityTween
{
    public class UnityTweenPosition : Tween<Transform, Vector3>
    {
        public UnityTweenPosition(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.position;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.position = Vector3.Lerp(_from, _to, CurrentEaseMethod(x)); //
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.position = x;
            };

            ValueOnBegin += () => _componentToAnimate.position;
        }

        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear

        public override Tween SetFrom(object from)
        {
            if (from is Vector3 vector)
                _from = vector;
            return this;
        }

        public override Tween SetTo(object to)
        {
            if (to is Vector3 vector)
                _to = vector;
            return this;
        }
    }
}
