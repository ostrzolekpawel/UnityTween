using UnityEngine;

namespace UnityTween
{
    public class UnityTweenTextSize : Tween<TMPro.TMP_Text, float>
    {
        public UnityTweenTextSize(TMPro.TMP_Text text, float endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = text;
            _from = _componentToAnimate.fontSize;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.fontSize = Mathf.Lerp(_from, _to, CurrentEaseMethod(x));
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.fontSize = x;
            };
            ValueOnBegin += () => _componentToAnimate.fontSize;
        }

        public override Tween SetFrom(object from)
        {
            if (from is float number)
                _from = number;
            return this;
        }

        public override Tween SetTo(object to)
        {
            if (to is float number)
                _to = number;
            return this;
        }
    }
}
