using System.Collections;
using UnityEngine;


public struct RotationDirectionClass 
{
    //this struct is so that it is much easier to 
    // So that I dont have to remember which direction is which and can just use
    //this to get vector 3 direction
    public enum RotationDirection //only two direction
    {
        clockWise,
        antiClockWise
    }

    public static Vector3 GetVector3FromDirection(RotationDirection direction)
    {
        //getting the vector 3 direction using a switch. 
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
