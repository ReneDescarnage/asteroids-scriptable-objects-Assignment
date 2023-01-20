using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.Serialization;

public class ConfigEditorWindow : EditorWindow {
    
    [FormerlySerializedAs("UXMLFile")] [SerializeField] private VisualTreeAsset _uxmlFile;
    private List<VisualElement> _changableElements = null;
    [FormerlySerializedAs("Config")] private Configuration _config = null;

    [MenuItem(("Tools/Config Editor"))]
    public static void ShowWindow() {
        ConfigEditorWindow window = GetWindow<ConfigEditorWindow>();
        window.titleContent = new GUIContent("Configuration Settings");
    }

    private void OnEnable() {
        _config = AssetDatabase.LoadAssetAtPath<Configuration>("Assets/_Game/DataAssets/Configuration.asset");
        var serializedObj = new SerializedObject(_config);
        rootVisualElement.Bind(serializedObj);
    }

    private void CreateGUI() {
        
        _uxmlFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIBuilder/ConfigWindow.uxml");
        _uxmlFile.CloneTree(rootVisualElement);
        if (_changableElements == null) {
            _changableElements = rootVisualElement.Query(className: "BindableElement").ToList();
        }
        //IEnumerable<VisualElement> iteratable = rootVisualElement.Q("ChangableSettings").Children();
        BindElements();
    }

   
    private void BindElements() {
        foreach (var element in _changableElements) {
            if (element == null) {
                continue;
            }
            BindElement(element);
        }
      
    }
    
    private void BindElement(VisualElement element) {
     
        ScriptableObject objRef = _config.FindSOVariable(element.name);
        if (objRef == null) {
            Debug.Log(" This element was null " + element.name);
            return;
        }
        var serializedObject = new SerializedObject(_config.FindSOVariable(element.name));
        element.Bind(serializedObject);   
    }  
}
