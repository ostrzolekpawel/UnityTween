using System;
using UnityEngine;
using UnityTween;

namespace UnityTweenReflection
{
    public class UnityTweenFloat<T> : Tween<T, float> where T : UnityEngine.Object
    {
        private Action<T, object> _setter;
        private Func<T, object> _getter;

        public UnityTweenFloat(T reference, string fieldName, float endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            var fieldInfo = typeof(T).GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getter = FastInvoke.BuildUntypedGetter<T>(fieldInfo);
            _setter = FastInvoke.BuildUntypedSetter<T>(fieldInfo);

            _from = (float)_getter(_componentToAnimate);
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (time) =>
            {
                var value = Mathf.Lerp(_from, _to, CurrentEaseMethod(time));
                _setter(_componentToAnimate, value);
            };

            OnEvaluateComplete += (x) =>
            {
                _setter(_componentToAnimate, x);
            };

            //ValueOnBegin += () => _componentToAnimate.position;
        }
    }
}
