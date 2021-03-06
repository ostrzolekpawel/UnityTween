﻿using System;
using System.Reflection;
using UnityEngine;

namespace UnityTweenReflection
{
    public class UnityTweenVector2 : TweenNonGeneric<Vector2>
    {
        public UnityTweenVector2(object reference, Type referenceType, string fieldName, Vector2 endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            MemberInfo fieldInfo = referenceType.GetProperty(fieldName);

            if (fieldInfo == null)
                fieldInfo = referenceType.GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getter = FastInvoke.BuildUntypedGetter<object>(fieldInfo);
            _setter = FastInvoke.BuildUntypedSetter<object>(fieldInfo);

            _from = (Vector2)_getter(_componentToAnimate);
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (time) =>
            {
                var value = Vector2.Lerp(_from, _to, CurrentEaseMethod(time));
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
