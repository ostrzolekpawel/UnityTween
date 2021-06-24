using System;
using System.Reflection;
using UnityEngine;
using UnityTween;

namespace UnityTweenReflection
{
    public class UnityTweenFloat : Tween<object, float>
    {
        private Action<object, object> _setter;
        private Func<object, object> _getter;

        public UnityTweenFloat(object reference, string typeName, string fieldName, float endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            Type type = Type.GetType(typeName);
            MemberInfo fieldInfo = type.GetProperty(fieldName);

            if (fieldInfo == null)
                fieldInfo = type.GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field or property \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getter = FastInvoke.BuildUntypedGetter<object>(fieldInfo);
            _setter = FastInvoke.BuildUntypedSetter<object>(fieldInfo);

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

            ValueOnBegin += () => _from;
        }
    }
}
