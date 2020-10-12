using UnityEngine;

namespace UnityTween
{
    public class UnityTweenMaterialColor : UnityTween<Renderer, Color>
    {
        public UnityTweenMaterialColor(Renderer renderer, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = renderer;
            _from = _componentToAnimate.material.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.material.color = Color.Lerp(_from, _to, EaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.material.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.material.color;
        }

        public override Color EvaluateValue(float t) => Color.Lerp(_from, _to, EaseMethod(t));
        //public override float GetTime(Vector3 v) => v.InverseLerp(_from, _to);
    }
}
