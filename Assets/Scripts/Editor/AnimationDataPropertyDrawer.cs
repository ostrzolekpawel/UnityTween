#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityTween;

namespace UnityTweenEditor
{
    [CustomPropertyDrawer(typeof(AnimationData))]
    public class AnimationDataPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (20 - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 3); //9
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0.0f;
            //float labelWidth = 100.0f;

            EditorGUI.BeginProperty(position, label, property);
            {
                DrawField("Forward: ", position, height, property.FindPropertyRelative("AnimationForward"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                DrawField("Rewind Is Different: ", position, height, property.FindPropertyRelative("RewindIsDifferent"));

                var isDifferent = property.FindPropertyRelative("RewindIsDifferent").boolValue;
                if (isDifferent)
                {
                    height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    DrawField("Rewind: ", position, height, property.FindPropertyRelative("AnimationRewind"));
                }


                //var fromIsDifferent = property.FindPropertyRelative("FromIsDifferentThanCurrent").boolValue;
                //DrawField("From Is Different: ", position, height, property.FindPropertyRelative("FromIsDifferentThanCurrent"));
                //if (fromIsDifferent)
                //{
                //    height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                //    DrawField("From: ", position, height, property.FindPropertyRelative("From"));
                //}
                //height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                //DrawField("To: ", position, height, property.FindPropertyRelative("To"));
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

#endif