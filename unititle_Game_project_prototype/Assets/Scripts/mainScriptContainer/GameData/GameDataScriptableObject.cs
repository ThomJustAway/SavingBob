using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataScriptableObject : ScriptableObject
{
    //this will store the game data that is necessary (like money and the number of layers in a game)
    public int money;
    public GameObject[] items = new GameObject[4];
    public int NumberOfLayers;

    public IMoveable[] moveables
    {
        get
        { // this converts the gameobject input to Imoveable as the prefabs used will have Imoveable interface components
            return items.Select(item => item.GetComponent<IMoveable>()).ToArray();
        }
    }

}



