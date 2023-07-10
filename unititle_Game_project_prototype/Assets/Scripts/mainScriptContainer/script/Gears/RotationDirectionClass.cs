using System.Collections;
using UnityEngine;


public struct RotationDirectionClass 
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
                return Vector3.forward;
            case RotationDirection.antiClockWise:
                return Vector3.back;
            default:
                return Vector3.forward;
        }
    }

}
