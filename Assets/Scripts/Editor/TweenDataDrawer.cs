#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityTween;

namespace UnityTweenEditor
{
    public class TweenDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            float height = 0.0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                var fromIsDifferent = property.FindPropertyRelative("FromIsDifferentThanCurrent").boolValue;
                DrawField("From Is Different: ", position, height, property.FindPropertyRelative("FromIsDifferentThanCurrent"));
                if (fromIsDifferent)
                {
                    height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    DrawField("From: ", position, height, property.FindPropertyRelative("From"));
                }
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                DrawField("To: ", position, height, property.FindPropertyRelative("To"));

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

    [CustomPropertyDrawer(typeof(VectorTweenData))]
    [CustomPropertyDrawer(typeof(QuaternionTweenData))]
    [CustomPropertyDrawer(typeof(ColorTweenData))]
    [CustomPropertyDrawer(typeof(FloatTweenData))]
    public class AnyTweenData : TweenDataDrawer { }
}

#endif