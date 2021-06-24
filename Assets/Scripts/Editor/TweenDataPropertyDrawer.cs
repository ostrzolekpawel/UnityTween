#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityTween;

namespace UnityTweenEditor
{
    public class TweenDataPropertyDrawer : PropertyDrawer
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
                    var fromIsDifferent = property.FindPropertyRelative("FromIsDifferentThanCurrent").boolValue;
                    DrawField("From Is Different: ", position, _labelWidth, property.FindPropertyRelative("FromIsDifferentThanCurrent"));
                    if (fromIsDifferent)
                    {
                        DrawField("From: ", position, _labelWidth, property.FindPropertyRelative("From"));
                    }
                    DrawField("To: ", position, _labelWidth, property.FindPropertyRelative("To"));

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
    }

    [CustomPropertyDrawer(typeof(VectorTweenData))]
    [CustomPropertyDrawer(typeof(QuaternionTweenData))]
    [CustomPropertyDrawer(typeof(ColorTweenData))]
    [CustomPropertyDrawer(typeof(FloatTweenData))]
    public class AnyTweenDataDrawer : TweenDataPropertyDrawer { }
}

#endif