using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExampleWindow : EditorWindow
{
    private GameObject[] examples;

    [MenuItem("Window/Examplewindow")]
    public static void ShowWindow() { 
        GetWindow<ExampleWindow>();
    }

    private void Awake()
    {
        examples = Resources.LoadAll<GameObject>("Prefabs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Hello world",EditorStyles.boldLabel);
        GUILayout.Label("Number of gameobject in prefabs"+examples.Length.ToString(), EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        foreach (GameObject example in examples)
        {

            SpriteRenderer renderer = example.GetComponent<SpriteRenderer>();


            GUILayout.Box(renderer.sprite.texture);
            GUILayout.Box(example.name);

        }
        GUILayout.EndHorizontal();


    }

}
