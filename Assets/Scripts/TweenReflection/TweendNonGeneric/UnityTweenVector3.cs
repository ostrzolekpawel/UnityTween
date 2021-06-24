using System;
using System.Reflection;
using UnityEngine;
using UnityTween;

namespace UnityTweenReflection
{
    public class UnityTweenVector3 : Tween<object, Vector3>
    {
        private Action<object, object> _setter;
        private Func<object, object> _getter;

        public UnityTweenVector3(object reference, string typeName, string fieldName, Vector3 endValue, bool isAdditive = false)
        {
            _componentToAnimate = reference;
            Type type = GetType(typeName);
            MemberInfo fieldInfo = type.GetProperty(fieldName);

            if (fieldInfo == null)
                fieldInfo = type.GetField(fieldName);

            if (fieldInfo == null)
                throw new Exception($"Can't find field or property \"{fieldName}\" in type \"{reference.GetType()}\"");

            _getter = FastInvoke.BuildUntypedGetter<object>(fieldInfo);
            _setter = FastInvoke.BuildUntypedSetter<object>(fieldInfo);

            _from = (Vector3)_getter(_componentToAnimate);
            _to = isAdditive ? _from + endValue : endValue;

            OnEvaluate += (time) =>
            {
                var value = Vector3.Lerp(_from, _to, CurrentEaseMethod(time));
                _setter(_componentToAnimate, value);
            };

            OnEvaluateComplete += (x) =>
            {
                _setter(_componentToAnimate, x);
            };

            ValueOnBegin += () => _from;
        }

        public static Type GetType(string TypeName)
        {

            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, etc.
            var type = Type.GetType(TypeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // Get the name of the assembly (Assumption is that we are using
            // fully-qualified type names)
            var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));

            // Attempt to load the indicated Assembly
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                return null;

            // Ask that assembly to return the proper Type
            return assembly.GetType(TypeName);

        }

    }
}
