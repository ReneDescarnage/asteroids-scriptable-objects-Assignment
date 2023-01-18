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
    private List<VisualElement> ChangableElements = null;
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
        if (ChangableElements == null) {
            ChangableElements = rootVisualElement.Query(className: "BindableElement").ToList();
        }
        IEnumerable<VisualElement> iteratable = rootVisualElement.Q("ChangableSettings").Children();
        BindElements(iteratable);
        // //Debug.Log(iteratable.Count());
        // foreach (var element in ChangableElements) {
        //     if (element == null) {
        //         continue;
        //     }
        //     BindElement(element);
        // }
        
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
    private void BindElements(IEnumerable<VisualElement> iteratable) {
        foreach (var element in ChangableElements) {
            if (element == null) {
                continue;
            }
            BindElement(element);
        }
      
    }
}
