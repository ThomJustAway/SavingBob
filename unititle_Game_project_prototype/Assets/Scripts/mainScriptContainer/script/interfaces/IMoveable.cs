using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IMoveable
{
    /*
        Imoveable is a interface that gives scripts that implement it to be move by players.
        This gives a "has a " relationship in the script. All this method is what is required
        in order to move a item

        When a script implements the Imoveable, I would refer it as dragable object as its 
        has a function to move based on mouse movement
     */
    public int Cost { get; }
    // every moveable object will need a cost in order to buy and move it.

    public void CheckValidPosition();
    //This is called to check if the object is in a good position

    public void Move(Vector3 position);
    // Tells how the object should move

    public GameObject Getprefab { get; }
    // just a way to get the gameobject

    public string Name { get; }
    // show the name of the object

    public void RemoveItem();
    // how the item is suppose to be moved
}
