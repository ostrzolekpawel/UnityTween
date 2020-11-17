using UnityEngine;

namespace UnityTween
{
    public class UnityTweenSizeDelta : Tween<RectTransform, Vector3>
    {
        public UnityTweenSizeDelta(RectTransform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.sizeDelta;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.sizeDelta = Vector3.Lerp(_from, _to, EaseMethod(x)); 
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.sizeDelta = x;
            };

            ValueOnBegin += () => _componentToAnimate.sizeDelta;
        }

        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear
    }
}
