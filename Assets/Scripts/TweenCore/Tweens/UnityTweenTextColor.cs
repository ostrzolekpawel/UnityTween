using UnityEngine;

namespace UnityTween
{
    public class UnityTweenTextColor : Tween<TMPro.TMP_Text, Color>
    {
        public UnityTweenTextColor(TMPro.TMP_Text text, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = text;
            _from = _componentToAnimate.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.color = Color.Lerp(_from, _to, CurrentEaseMethod(x));
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.color;
        }

        public override Tween SetFrom(object from)
        {
            if (from is Color color)
                _from = color;
            return this;
        }

        public override Tween SetTo(object to)
        {
            if (to is Color color)
                _to = color;
            return this;
        }
    }
}
