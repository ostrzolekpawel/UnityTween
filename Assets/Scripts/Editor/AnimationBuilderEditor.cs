using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityTween;

[CustomEditor(typeof(AnimationBuilder)), CanEditMultipleObjects]
public class AnimationBuilderEditor : Editor
{
    private ReorderableList _list;
    private AnimationBuilder _tweenBuilder;

    private void OnEnable()
    {
        _tweenBuilder = (AnimationBuilder)target;

        _list = CreateList(serializedObject.FindProperty("_tweenDatas"));
    }
    private ReorderableList CreateList(SerializedProperty property)
    {
        ReorderableList list = new ReorderableList(property.serializedObject, property, true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Animations");
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            rect.y += 10.0f;
            EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
        };

        list.elementHeightCallback += ElementHeightCallback;

        list.onAddCallback = (li) =>
        {
            li.serializedProperty.serializedObject.Update();
            li.serializedProperty.arraySize++;
            li.serializedProperty.serializedObject.ApplyModifiedProperties();
        };
        return list;
    }

    private float ElementHeightCallback(int index)
    {
        float propertyHeight =
            EditorGUI.GetPropertyHeight(_list.serializedProperty.GetArrayElementAtIndex(index), true);

        float spacing = EditorGUIUtility.singleLineHeight / 2;
        return propertyHeight + spacing;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _tweenBuilder.WrapMode = (WrapMode)EditorGUILayout.EnumPopup("Wrap Mode", _tweenBuilder.WrapMode);
        _tweenBuilder.LoadOnStart = EditorGUILayout.Toggle("Load On Start", _tweenBuilder.LoadOnStart);
        _tweenBuilder.PlayOnStart = EditorGUILayout.Toggle("Play On Start", _tweenBuilder.PlayOnStart);

        if (_list.count != 0)
            if (GUILayout.Button("Load Tweens"))
                _tweenBuilder.LoadTweens();

        if (_tweenBuilder.IsInit)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play Forward"))
            {
                _tweenBuilder.PlayForward();
            }
            if (GUILayout.Button("Play Reverse"))
            {
                _tweenBuilder.PlayReverse();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play"))
            {
                _tweenBuilder.Play();
            }
            if (GUILayout.Button("Pause"))
            {
                _tweenBuilder.Stop();
            }
            GUILayout.EndHorizontal();
        }

        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_tweenBuilder);
            //EditorSceneManager.MarkSceneDirty(castedTarget.gameObject.scene);
        }
    }
}

