using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameDataScriptableObject))]
[CanEditMultipleObjects]
public class GameData_Inspector : Editor
{

    public VisualTreeAsset visualTree;
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();

        // Add a simple label
        myInspector.Add(new Label("This is a custom inspector"));

        // Load and clone a visual tree from UXML
        visualTree.CloneTree(myInspector);

        // Return the finished inspector UI
        return myInspector;
    }
}
