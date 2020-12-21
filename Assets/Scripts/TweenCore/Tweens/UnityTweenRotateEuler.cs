using UnityEngine;

namespace UnityTween
{
    public class UnityTweenRotateEuler : Tween<Transform, Vector3>
    {
        public UnityTweenRotateEuler(Transform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.eulerAngles;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.eulerAngles = Vector3.Lerp(_from, _to, CurrentEaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.eulerAngles = x;
            };
            ValueOnBegin += () => _componentToAnimate.eulerAngles;
        }

        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear

    }
}
