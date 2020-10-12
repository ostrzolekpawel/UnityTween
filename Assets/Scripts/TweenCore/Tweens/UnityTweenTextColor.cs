using UnityEngine;

namespace UnityTween
{
    public class UnityTweenTextColor : UnityTween<TMPro.TMP_Text, Color>
    {
        public UnityTweenTextColor(TMPro.TMP_Text text, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = text;
            _from = _componentToAnimate.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.color = Color.Lerp(_from, _to, EaseMethod(x));
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.color;
        }

        public override Color EvaluateValue(float t) => Color.Lerp(_from, _to, EaseMethod(t));
        //public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to);
    }
}
