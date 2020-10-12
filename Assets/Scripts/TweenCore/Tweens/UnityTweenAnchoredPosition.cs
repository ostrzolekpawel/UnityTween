using UnityEngine;

namespace UnityTween
{
    public class UnityTweenAnchoredPosition : UnityTween<RectTransform, Vector3>
    {
        public UnityTweenAnchoredPosition(RectTransform transform, Vector3 endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = transform;
            _from = transform.anchoredPosition;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.anchoredPosition = Vector3.Lerp(_from, _to, EaseMethod(x)); //
            };

            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.anchoredPosition = x;
            };

            ValueOnBegin += () => _componentToAnimate.anchoredPosition;
        }

        public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to); // for now works only with linear
    }
}
