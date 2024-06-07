#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;



[CustomPropertyDrawer(typeof(SearchableAttribute))]
public class SearchableAttributeDrawer : PropertyDrawer
{
    private string _search;
    private string[] _options;
    private static GUIStyle _searchTextFieldStyle;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = base.GetPropertyHeight(property, label);

        if (property.propertyType == SerializedPropertyType.Enum)
            height = height * 2 + EditorGUIUtility.standardVerticalSpacing;

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Enum)
        {
            _searchTextFieldStyle ??= GUI.skin.FindStyle("ToolbarSearchTextField");
            if (_options == null)
                UpdateOptions(property.enumDisplayNames);

            Rect searchRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            DrawSearchBar(searchRect, label, property.enumDisplayNames);

            Rect popupRect = new(position.x, searchRect.y + searchRect.height + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
            DrawEnumPopup(popupRect, property);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    private void DrawSearchBar(Rect position, GUIContent label, string[] allOptions)
    {
        EditorGUI.BeginChangeCheck();
        _search = EditorGUI.TextField(position, label, _search, _searchTextFieldStyle);
        if(EditorGUI.EndChangeCheck())
            UpdateOptions(allOptions);
    }

    private void DrawEnumPopup(Rect position, SerializedProperty property)
    {
        Rect fieldRect = EditorGUI.PrefixLabel(position, new GUIContent(" "));
        int currentIndex = Array.IndexOf(_options, property.enumDisplayNames[property.enumValueIndex]);
        int selectedIndex = EditorGUI.Popup(fieldRect, currentIndex, _options);
        if (selectedIndex >= 0)
        {
            int newIndex = Array.IndexOf(property.enumDisplayNames, _options[selectedIndex]);
            if (newIndex != currentIndex)
            {
                property.enumValueIndex = newIndex;
                _search = string.Empty;
                UpdateOptions(property.enumDisplayNames);
            }
        }
    }

    private void UpdateOptions(string[] allOptions)
    {
        _options = Array.FindAll(allOptions, name => string.IsNullOrEmpty(_search) || 
        name.IndexOf(_search, StringComparison.InvariantCultureIgnoreCase) >= 0);
    }
}

#endif