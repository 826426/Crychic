using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelAttribute))]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LabelAttribute labelAttribute = (LabelAttribute)attribute;
        
        string displayName = labelAttribute.label;
        EditorGUI.PropertyField(position, property, new GUIContent(displayName), true);
    }
}
