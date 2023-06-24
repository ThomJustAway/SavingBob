using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataScriptableObject : ScriptableObject
{

    public int money;
    public GameObject[] items = new GameObject[4];
    public int NumberOfLayers;

    public IMoveable[] moveables
    {
        get
        {
            return items.Select(item => item.GetComponent<IMoveable>()).ToArray();
        }
    }

}



