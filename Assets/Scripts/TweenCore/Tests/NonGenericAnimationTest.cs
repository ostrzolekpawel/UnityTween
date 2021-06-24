using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityTweenReflection;

namespace Assets.Scripts.TweenCore.Tests
{
    public class NonGenericAnimationTest : MonoBehaviour
    {
        [SerializeField] private GameObject _referenceObject;
        private string _transformType;
        private string _fieldType;

        private UnityTween.TweenCore _tweenCore = new UnityTween.TweenCore();

        private void Start()
        {
            var components = _referenceObject.GetComponents<Transform>();
            List<(Type, PropertyInfo)> allProperties = new List<(Type, PropertyInfo)>();
            List<(Type, FieldInfo)> allFields = new List<(Type, FieldInfo)>();
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

            foreach (var component in components)
            {
                Type type = component.GetType();
                var properties = type.GetProperties(flags);
                var fields = type.GetFields(flags);
                foreach (var p in properties)
                {
                    if (p.PropertyType == typeof(Vector3))
                        allProperties.Add((type, p));
                }
                foreach (var f in fields)
                {
                    if (f.FieldType == typeof(Vector3))
                        allFields.Add((type, f));
                }
            }

            var a = allProperties[0];
            _transformType = a.Item1.FullName;
            _fieldType = a.Item2.Name;

            var tween = new UnityTweenVector3(_referenceObject, _transformType, _fieldType, Vector3.one, true).SetDuration(1.0f).SetForwardEase(UnityTween.Ease.Linear);
            _tweenCore.Append(tween);
        }

        [ContextMenu("Play")]
        private void Play()
        {
            _tweenCore.SetForward().Play();
        }

        private void Update()
        {
            _tweenCore.Tick();
        }
    }
}
