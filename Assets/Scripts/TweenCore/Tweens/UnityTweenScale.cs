using UnityEngine;

namespace UnityTween
{
    public class UnityTweenScale : UnityTween<Transform, Vector3>
    {
        public UnityTweenScale(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.localScale;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.localScale = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.localScale = x;
            };
            ValueOnBegin += () => _componentToAnimate.localScale;
        }

        public override Vector3 EvaluateValue(float t) => Vector3.Lerp(_from, _to, EaseMethod(t));
        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear
    }
}
