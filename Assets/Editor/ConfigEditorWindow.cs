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
    
    [SerializeField] private VisualTreeAsset UXMLFile;

    [FormerlySerializedAs("Config")] public Configuration Config = null;

    [MenuItem(("Tools/Config Editor"))]
    public static void ShowWindow() {
        ConfigEditorWindow window = GetWindow<ConfigEditorWindow>();
        window.titleContent = new GUIContent("Configuration Settings");
    }

    private void OnEnable() {
        Config = AssetDatabase.LoadAssetAtPath<Configuration>("Assets/_Game/DataAssets/Configuration.asset");
        var serializedObj = new SerializedObject(Config);
        rootVisualElement.Bind(serializedObj);
    }

    private void CreateGUI() {
        
        UXMLFile = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIBuilder/ConfigWindow.uxml");
        
        UXMLFile.CloneTree(rootVisualElement);
        IEnumerable<VisualElement> iteratable = rootVisualElement.Q("ChangableSettings").Children();
        //Debug.Log(iteratable.Count());
        foreach (var element in iteratable) {
            if (element == null) {
                continue;
            }
            BindElement(element);
        }
        
    }

    private void BindElement(VisualElement element) {
     
        element = rootVisualElement.Q(element.name);
        ScriptableObject objRef = Config.FindSOVariable(element.name);
        if (objRef == null) {
            return;
        }
        var serializedObject = new SerializedObject(Config.FindSOVariable(element.name));
        element.Bind(serializedObject);    
    }
}
