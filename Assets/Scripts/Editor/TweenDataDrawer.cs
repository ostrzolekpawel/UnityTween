using UnityTween;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(TweenData))]
public class TweenDataDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 9);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = 0.0f;
        float labelWidth = 100.0f;

        EditorGUI.BeginProperty(position, label, property);
        {
            DrawField("Type: ", position, height, property.FindPropertyRelative("Type"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            DrawField("Target: ", position, height, property.FindPropertyRelative("Target"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            DrawField("Delay: ", position, height, property.FindPropertyRelative("Delay"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            DrawField("Duration: ", position, height, property.FindPropertyRelative("Duration"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            DrawField("Is Additive: ", position, height, property.FindPropertyRelative("IsAdditive"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            var type = (AnimationType)property.FindPropertyRelative("Type").intValue;
            if (type == AnimationType.EulerRotation ||
                    type == AnimationType.Scale ||
                    type == AnimationType.Translate ||
                    type == AnimationType.RectTranslate)
            {
                DrawField("Vector: ", position, height, property.FindPropertyRelative("Vector"));
            }
            else if (type == AnimationType.ImageColor ||
                        type == AnimationType.MaterialColor)
            {
                DrawField("Color: ", position, height, property.FindPropertyRelative("Color"));
            }
            else if (type == AnimationType.QuaternionRotation)
            {
                DrawField("Quaternion: ", position, height, property.FindPropertyRelative("Quaternion"));
            }
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            DrawField("Ease: ", position, height, property.FindPropertyRelative("Ease"));
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            var ease = (Ease)property.FindPropertyRelative("Ease").intValue;
            var curve = CreateCurveFromEase(ease);
            if (ease == Ease.Custom)
            {
                DrawField("Curve: ", position, height, property.FindPropertyRelative("Curve"));
            }
            else
            {
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

        if (UnityTween.UnityTween.EaseFunctions.ContainsKey(ease))
            CurveFunction = UnityTween.UnityTween.EaseFunctions[ease];
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

