using UnityEngine;

namespace UnityTween
{
    public class UnityTweenMaterialColor : Tween<Renderer, Color>
    {
        public UnityTweenMaterialColor(Renderer renderer, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = renderer;
            _from = _componentToAnimate.material.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.material.color = Color.Lerp(_from, _to, CurrentEaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.material.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.material.color;
        }
    }
}
