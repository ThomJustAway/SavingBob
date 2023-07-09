using System.Collections;
using UnityEngine;


public class RotationDirectionClass 
{
    public enum RotationDirection
    {
        clockWise,
        antiClockWise
    }

    public static Vector3 GetVector3FromDirection(RotationDirection direction)
    {
        switch (direction)
        {
            case RotationDirection.clockWise:
                return Vector3.back;
            case RotationDirection.antiClockWise:
                return Vector3.forward;
            default:
                return Vector3.forward;
        }
    }

}
