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
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 2);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0.0f;
            float labelWidth = 100.0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                DrawField("Type: ", position, height, property.FindPropertyRelative("Target"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                GameObject tagret = property.FindPropertyRelative("Target").objectReferenceValue as GameObject;

                if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
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
                            if (p.PropertyType == typeof(Vector3))
                                allProperties.Add((type, p));
                        }
                        foreach (var f in fields)
                        {
                            if (f.FieldType == typeof(Vector3))
                                allFields.Add((type, f));
                        }
                    }

                    foreach (var f in allProperties)
                    {
                        menu.AddItem(new GUIContent($"{f.Item1}/{f.Item2}"), false, (a) =>
                        {
                            Debug.Log($"you picked: {f.Item2} from {f.Item1}");
                        }, f);
                    }

                    foreach (var f in allFields)
                    {
                        menu.AddItem(new GUIContent($"{f.Item1}/{f.Item2}"), false, (a) =>
                        {
                            Debug.Log($"you picked: {f.Item2} from {f.Item1}");
                        }, f);
                    }
                    menu.DropDown(position);
                }
            }
            EditorGUI.EndProperty();
        }

        private GenericMenu.MenuFunction2 DisplayType(object parametr)
        {
            Debug.Log($"{parametr}");
            return null;
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
