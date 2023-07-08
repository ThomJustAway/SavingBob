using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMoveable
{
    public int Cost { get; }

    public void CheckValidPosition();

    public void Move(Vector3 position);

    public GameObject Getprefab { get; }

    public string Name { get; }
}
