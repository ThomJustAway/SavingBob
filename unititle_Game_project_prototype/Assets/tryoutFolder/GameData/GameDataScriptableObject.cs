using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataScriptableObject : ScriptableObject
{
    //README
    /*
        contains the money and gears that will be avaliable for that specific level
     */
    
    public int money;
    public GameObject[] gearButtons; // try to figure out how to make and change the button on the scriptable object
    

}
