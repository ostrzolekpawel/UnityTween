using System;
using System.Reflection;
using UnityEngine;
using UnityTween;

namespace UnityTweenReflection.Generic
{
    public class UnityTweenVector3<T> : Tween<T, Vector3> where T : UnityEngine.Object
    {
        private Action<T, object> _setterField;
        private Func<T, object> _getterField;


        private Action<T, object> _setterReference;
        private Func<T, object> _getterReference;

        public UnityTweenVector3(T reference, string fieldName, Vector3 endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            MemberInfo fieldInfo = typeof(T).GetProperty(fieldName);
            if (fieldInfo == null)
                fieldInfo = typeof(T).GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getterField = FastInvoke.BuildUntypedGetter<T>(fieldInfo);
            _setterField = FastInvoke.BuildUntypedSetter<T>(fieldInfo);

            _from = (Vector3)_getterField(_componentToAnimate);
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (time) =>
            {
                var value = Vector3.Lerp(_from, _to, CurrentEaseMethod(time));
                _setterField(_componentToAnimate, value);
            };

            OnEvaluateComplete += (x) =>
            {
                _setterField(_componentToAnimate, x);
            };

            ValueOnBegin += () => _from;
        }

        public UnityTweenVector3(GameObject reference, Type referenceType, string fieldName, Vector3 endValue, bool isAdditive = false)
        {
            
        }
    }
}
