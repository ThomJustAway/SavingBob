using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataScriptableObject : ScriptableObject
{
    /*  Game data scriptable object is a scriptable object
        that tells the levelmanager what buttons and money
        to set. 
     
        This is helpful to create fast and testable levels without constantly
        adding prefabs to the scene to add more button
     */

    public int money; //tells the level manager how much money is required
    public GameObject[] items = new GameObject[4]; 
    //this items is to store prefabs of dragable item. Dragable items are gameobject that implements
    //the Imoveable interface. Imoveable interface is the interface use to move the gears/joints around.
    public int NumberOfLayers;
    //number of layers in the game
    public IMoveable[] moveables
    {
        //this is to get all the Imoveable component in the dragable item
        get
        { // this converts the gameobject input to Imoveable as the prefabs used will have Imoveable interface components
            return items.Select(item => item.GetComponent<IMoveable>()).ToArray();
        }
    }

}



