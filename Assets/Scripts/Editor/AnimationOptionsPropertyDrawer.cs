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
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 2); //9
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0.0f;
            float labelWidth = 200.0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                DrawField("Ease: ", position, height, property.FindPropertyRelative("Ease"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                var ease = (Ease)property.FindPropertyRelative("Ease").intValue;
                var curve = CreateCurveFromEase(ease);
                if (ease == Ease.Custom) // 
                {
                    DrawField("Curve: ", position, height, property.FindPropertyRelative("Curve"));
                }
                else
                {
                    //height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.LabelField(new Rect(position.x, position.y + height, labelWidth, EditorGUIUtility.singleLineHeight), "Curve: ");
                    EditorGUI.CurveField(new Rect(position.x + labelWidth, position.y + height, position.width - labelWidth, EditorGUIUtility.singleLineHeight), curve);
                }
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