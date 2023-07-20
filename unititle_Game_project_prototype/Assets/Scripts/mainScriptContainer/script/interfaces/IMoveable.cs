using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMoveable
{
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
}
