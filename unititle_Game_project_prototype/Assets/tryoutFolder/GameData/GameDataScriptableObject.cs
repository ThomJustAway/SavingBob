using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataScriptableObject : ScriptableObject
{

    public int money;
    public ItemButtonData[] itemButtons = new ItemButtonData[4];
}


[System.Serializable]
public class ItemButtonData 
{
    public GameObject prefab;
    public int cost;
    public string buttonName;
}

