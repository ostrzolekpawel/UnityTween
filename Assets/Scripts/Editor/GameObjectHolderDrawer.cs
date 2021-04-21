using Assets.Scripts.TweenCore.Tests;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(GameObjectHolder))]
    public class GameObjectHolderDrawer : PropertyDrawer
    {
        private readonly Dictionary<AnimationType, Type> _animationTypes = new Dictionary<AnimationType, Type>()
        {
            [AnimationType.Float] = typeof(float),
            [AnimationType.Vector2] = typeof(Vector2),
            [AnimationType.Vector3] = typeof(Vector3),
            [AnimationType.Quaternion] = typeof(Quaternion)
        };
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 4);
        }
        private string selectedField = "";
        private string selectedType ="";
        private AnimationType _currentAnimationType;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0.0f;
            float labelWidth = 100.0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                DrawField("Target: ", position, height, property.FindPropertyRelative("Target"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                DrawField("Type: ", position, height, property.FindPropertyRelative("Type"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                GameObject tagret = property.FindPropertyRelative("Target").objectReferenceValue as GameObject;
                AnimationType animType = (AnimationType)property.FindPropertyRelative("Type").intValue;
                
                if (animType != _currentAnimationType)
                {
                    _currentAnimationType = animType;
                    selectedField = "";
                }

                if (GUI.Button(new Rect(position.x, position.y + height, 30, EditorGUIUtility.singleLineHeight), ">"))
                {
                    var components = tagret.GetComponents<Component>();
                    GenericMenu menu = new GenericMenu();
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
                            if (p.PropertyType == _animationTypes[animType])
                                allProperties.Add((type, p));
                        }
                        foreach (var f in fields)
                        {
                            if (f.FieldType == _animationTypes[animType])
                                allFields.Add((type, f));
                        }
                    }

                    foreach (var f in allProperties)
                    {
                        menu.AddItem(new GUIContent($"{f.Item1}/{f.Item2}"), false, (a) =>
                        {
                            selectedType = f.Item1.ToString();
                            selectedField = f.Item2.ToString();
                            Debug.Log($"you picked: {f.Item2} from {f.Item1}");
                        }, f);
                    }

                    foreach (var f in allFields)
                    {
                        menu.AddItem(new GUIContent($"{f.Item1}/{f.Item2}"), false, (a) =>
                        {
                            selectedType = f.Item1.ToString();
                            selectedField = f.Item2.ToString();
                            Debug.Log($"you picked: {f.Item2} from {f.Item1}");
                        }, f);
                    }
                    menu.DropDown(position);
                }

                EditorGUI.LabelField(new Rect(position.x + labelWidth, position.y + height, position.width - labelWidth, EditorGUIUtility.singleLineHeight), selectedField);
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.LabelField(new Rect(position.x + labelWidth, position.y + height, position.width - labelWidth, EditorGUIUtility.singleLineHeight), selectedType);
            }
            EditorGUI.EndProperty();
        }

        private void DrawField(string name, Rect position, float height, SerializedProperty serializedProperty)
        {
            float labelWidth = 100.0f;
            EditorGUI.LabelField(new Rect(position.x, position.y + height, labelWidth, EditorGUIUtility.singleLineHeight), name);
            EditorGUI.PropertyField(
                new Rect(position.x + labelWidth, position.y + height, position.width - labelWidth, EditorGUIUtility.singleLineHeight),
                serializedProperty, GUIContent.none);
        }
    }
}
