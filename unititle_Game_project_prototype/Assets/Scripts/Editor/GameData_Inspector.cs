using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameDataScriptableObject))]
[CanEditMultipleObjects]
public class GameData_Inspector : Editor
{

    // this script can be ignored. Old piece of code used in the making of the game
    public VisualTreeAsset visualTree;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();  
        myInspector.Add(new Label("This is a custom inspector"));
        visualTree.CloneTree(myInspector);
        return myInspector;
    }
}
