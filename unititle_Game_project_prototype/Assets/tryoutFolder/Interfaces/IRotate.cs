using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotate 
{
    void RotateGear(float speed,Vector3 direction);
}

public enum Layers
{
    EntireGearArea = 6,
    InnerGearArea = 7
}

public class Direction
{
    public static Vector3 Clockwise = Vector3.forward;
    public static Vector3 AntiClockwise = Vector3.back;
  
}
