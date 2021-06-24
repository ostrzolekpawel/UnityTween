#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityTween;

namespace UnityTweenEditor
{
    [CustomPropertyDrawer(typeof(AnimationOptions))]
    public class AnimationOptionsPropertyDrawer : PropertyDrawer
    {
        private float _height;
        private float _labelWidth = 50;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return _height;
            //return property.isExpanded ? _height : EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            _height = EditorGUIUtility.singleLineHeight;
            //property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

            //if (property.isExpanded)
            //{
                EditorGUI.BeginProperty(position, label, property);
                {
                    DrawField("Ease: ", position, _labelWidth, property.FindPropertyRelative("Ease"));

                    var ease = (Ease)property.FindPropertyRelative("Ease").intValue;
                    var curve = CreateCurveFromEase(ease);
                    if (ease == Ease.Custom) // 
                    {
                        DrawField("Curve: ", position, _labelWidth, property.FindPropertyRelative("Curve"));
                    }
                    else
                    {
                        DrawCurveField("Curve: ", position, _labelWidth, curve);
                    }
                }
                EditorGUI.EndProperty();
            //}
        }

        private void DrawField(string name, Rect position, SerializedProperty serializedProperty)
        {
            float labelWidth = EditorGUIUtility.labelWidth / 2;
            DrawField(name, position, labelWidth, serializedProperty);
        }

        private void DrawField(string name, Rect position, float labelWidth, SerializedProperty serializedProperty)
        {
            EditorGUI.LabelField(new Rect(position.x + 10, position.y + _height, labelWidth + 10, EditorGUIUtility.singleLineHeight), name);
            EditorGUI.PropertyField(
                new Rect(position.x + labelWidth * 2, position.y + _height, position.width - labelWidth, EditorGUIUtility.singleLineHeight),
                serializedProperty, GUIContent.none);
            _height += EditorGUI.GetPropertyHeight(serializedProperty, true);
        }

        private void DrawCurveField(string name, Rect position, AnimationCurve serializedProperty)
        {
            float labelWidth = EditorGUIUtility.labelWidth / 2;
            DrawCurveField(name, position, labelWidth, serializedProperty);
        }
        private void DrawCurveField(string name, Rect position, float labelWidth, AnimationCurve serializedProperty)
        {
            EditorGUI.LabelField(new Rect(position.x + 10, position.y + _height, labelWidth + 10, EditorGUIUtility.singleLineHeight), name);
            EditorGUI.CurveField(
                new Rect(position.x + labelWidth * 2, position.y + _height, position.width - labelWidth, EditorGUIUtility.singleLineHeight),
                serializedProperty);
            _height += EditorGUIUtility.singleLineHeight;
        }


        private Func<float, float> CurveFunction = null;
        private AnimationCurve CreateCurveFromEase(Ease ease)
        {
            var curve = new AnimationCurve();

            if (Tween.EaseFunctions.ContainsKey(ease))
                CurveFunction = Tween.EaseFunctions[ease];
            else
                return curve;

            float sample = 0.01f;
            float i = 0.0f;
            do
            {
                curve.AddKey(i, CurveFunction.Invoke(i));
                i += sample;
            }
            while (i <= 1.0f);
            return curve;
        }
    }

}

#endif