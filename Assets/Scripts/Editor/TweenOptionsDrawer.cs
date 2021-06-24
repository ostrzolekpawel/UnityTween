#if UNITY_EDITOR
using UnityTween;
using System;
using UnityEditor;
using UnityEngine;
namespace UnityTweenEditor
{
    [CustomPropertyDrawer(typeof(TweenOptions))]
    public class TweenOptionsDrawer : PropertyDrawer
    {
        private float _height;
        private float _labelWidth = 50;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? _height : EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            _height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

            if (property.isExpanded)
            {
                EditorGUI.BeginProperty(position, label, property);
                {
                    DrawField("Type: ", position, _labelWidth, property.FindPropertyRelative("AnimationType"));
                    DrawField("Target: ", position, _labelWidth, property.FindPropertyRelative("Target"));
                    DrawField("Delay: ", position, _labelWidth, property.FindPropertyRelative("Delay"));
                    DrawField("Duration: ", position, _labelWidth, property.FindPropertyRelative("Duration"));
                    DrawField("Is Additive: ", position, _labelWidth, property.FindPropertyRelative("IsAdditive"));

                    var type = (AnimationType)property.FindPropertyRelative("AnimationType").intValue;

                    DrawField("Ease: ", position, _labelWidth, property.FindPropertyRelative("Ease"));

                    var ease = (Ease)property.FindPropertyRelative("Ease").intValue;
                    var curve = CreateCurveFromEase(ease);
                    if (ease == Ease.Custom)
                    {
                        DrawField("Curve: ", position, _labelWidth, property.FindPropertyRelative("Curve"));
                    }
                    else
                    {
                        DrawCurveField("Curve", position, _labelWidth, curve);
                    }

                    if (IsVectorType(type))
                    {
                        DrawField("Vector: ", position, _labelWidth, property.FindPropertyRelative("Vector"));
                    }
                    else if (IsColorType(type))
                    {
                        DrawField("Color: ", position, _labelWidth, property.FindPropertyRelative("Color"));
                    }
                    else if (type == AnimationType.QuaternionRotation)
                    {
                        DrawField("Quaternion: ", position, _labelWidth, property.FindPropertyRelative("Quaternion"));
                    }
                    else if (type == AnimationType.TextSize)
                    {
                        DrawField("Float: ", position, _labelWidth, property.FindPropertyRelative("Float"));
                    }

                    DrawField("Animation: ", position, _labelWidth, property.FindPropertyRelative("Animation"));

                }
                EditorGUI.EndProperty();
            }
        }

        private static bool IsColorType(AnimationType type)
        {
            return type == AnimationType.ImageColor ||
                                    type == AnimationType.MaterialColor ||
                                    type == AnimationType.TextColor;
        }

        private static bool IsVectorType(AnimationType type)
        {
            return type == AnimationType.EulerRotation ||
                                type == AnimationType.Scale ||
                                type == AnimationType.Position ||
                                type == AnimationType.AnchoredPosition ||
                                type == AnimationType.SizeDelta;
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

            if (UnityTween.Tween.EaseFunctions.ContainsKey(ease))
                CurveFunction = UnityTween.Tween.EaseFunctions[ease];
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