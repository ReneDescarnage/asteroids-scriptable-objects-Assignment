using System;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Vars.Editor
{
    [CustomPropertyDrawer(typeof(FloatRef))]
    public class FloatRefDrawer : UnityEditor.PropertyDrawer
    {
        private readonly string[] _popupOptions = {"Use Simple", "Use Variable"};

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle _popupStyle;
        
        SerializedProperty _useSimpleProperty;
        SerializedProperty _variableProperty; 
        SerializedProperty _simpleValueProperty; 

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.BeginChangeCheck();
            label = EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, label);
            
            // Get Properties
            GetProperties(property);
            
            // Draw button
            // Create Style
            _popupStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
            {
                imagePosition = ImagePosition.ImageOnly
            };

            // Calculate Button rect
            var buttonRect = new Rect(position);
            buttonRect.yMin += _popupStyle.margin.top;
            buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
        
            position.xMin = buttonRect.xMax;
            
            var selectedIndex = _useSimpleProperty.boolValue ? 0 : 1;
            var result = EditorGUI.Popup(buttonRect, selectedIndex , _popupOptions, _popupStyle);
            _useSimpleProperty.boolValue = result == 0;

            var propertyToDraw = _useSimpleProperty.boolValue ? _simpleValueProperty : _variableProperty;
            EditorGUI.PropertyField(position, propertyToDraw, GUIContent.none);
            
            EditorGUI.EndProperty();
            //EditorGUI.EndChangeCheck();

            // Draw bool
            //EditorGUI.PropertyField(position, useSimpleProperty, GUIContent.none);
            //position.y += position.height;
            // GetPropertyHeight(useSimpleProperty, GUIContent.none);

            // Draw variable 

            //EditorGUI.PropertyField(position, variableProperty, GUIContent.none);
            //position.height += 20f;
            //var positionSize = position.size;
            //positionSize.y += 20f;
            //position.size = positionSize;



            //EditorGUI.LabelField(position, "my other label");

            //SerializedProperty variableProperty = property.FindPropertyRelative(nameof(FloatRef._variable));
            //EditorGUI.PropertyField(position, variableProperty, GUIContent.none);

            /*popupStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
            {
                imagePosition = ImagePosition.ImageOnly
            };

            position = DrawPropertyName(position, property, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("_useSimpleValue");
            SerializedProperty constantValue = property.FindPropertyRelative("_variable");
            SerializedProperty variable = property.FindPropertyRelative("_simpleValue");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            useConstant.boolValue = ShowPopup(useConstant, buttonRect);

            EditorGUI.PropertyField(position,
                useConstant.boolValue ? constantValue : variable,
                GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();*/

        }
        private void GetProperties(SerializedProperty property)
        {
            _useSimpleProperty = property.FindPropertyRelative(FloatRef.UseSimpleValueName);
            _variableProperty = property.FindPropertyRelative(FloatRef.VariableName);
            _simpleValueProperty = property.FindPropertyRelative(FloatRef.SimpleValueName);
        }

        private static Rect DrawPropertyName(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);
            return position;
        }

        private bool ShowPopup(SerializedProperty useConstant, Rect buttonRect)
        {
            var result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, _popupOptions, _popupStyle);
            return result == 0;
        } 
    }
}
