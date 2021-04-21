using System;
using System.Reflection;
using UnityEngine;
using UnityTween;

namespace UnityTweenReflection.Generic
{
    public class UnityTweenQuaternion<T> : Tween<T, Quaternion> where T : UnityEngine.Object
    {
        private Action<T, object> _setter;
        private Func<T, object> _getter;

        public UnityTweenQuaternion(T reference, string fieldName, Quaternion endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            MemberInfo fieldInfo = typeof(T).GetProperty(fieldName);
            if (fieldInfo == null)
                fieldInfo = typeof(T).GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getter = FastInvoke.BuildUntypedGetter<T>(fieldInfo);
            _setter = FastInvoke.BuildUntypedSetter<T>(fieldInfo);

            _from = (Quaternion)_getter(_componentToAnimate);
            _to = isAdditive ? _from * endValue : endValue;

            OnEvaluate += (time) =>
            {
                var value = Quaternion.Lerp(_from, _to, CurrentEaseMethod(time));
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
