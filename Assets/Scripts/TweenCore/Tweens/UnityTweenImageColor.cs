﻿using UnityEngine;
using UnityEngine.UI;

namespace UnityTween
{
    public class UnityTweenImageColor : Tween<Image, Color>
    {
        public UnityTweenImageColor(Image image, Color endValue, bool isAdditive = false) : base()
        {
            _componentToAnimate = image;
            _from = image.color;
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (x) =>
            {
                _componentToAnimate.color = Color.Lerp(_from, _to, CurrentEaseMethod(x)); //
            };
            OnEvaluateComplete += (x) =>
            {
                _componentToAnimate.color = x;
            };
            ValueOnBegin += () => _componentToAnimate.color;
        }
    }
}
