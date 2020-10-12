using UnityEngine;

namespace UnityTween
{
    public class UnityTweenPosition : UnityTween<Transform, Vector3>
    {
        public UnityTweenPosition(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.position;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.position = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.position = x;
            };

            ValueOnBegin += () => _componentToAnimate.position;
        }

        public override Vector3 EvaluateValue(float t) => Vector3.Lerp(_from, _to, EaseMethod(t));
        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear
    }
}
