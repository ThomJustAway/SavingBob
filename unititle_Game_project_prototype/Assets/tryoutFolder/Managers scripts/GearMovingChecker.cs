using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearMovingChecker : MonoBehaviour
{

    [SerializeField] private StartingGearClass startingGear;
    [SerializeField] private float speed;
    //do a recurrsive sequence;
    public LayerMask layer;
    private void Update()
    {
        List<Gear> GearConnected = new List<Gear>();
        GearConnected.Add(startingGear.GearHost);
        RotateConnectingGears(startingGear.GearHost,RotationDirection.clockWise,speed,GearConnected);
    }

    private void RotateConnectingGears(Gear currentGear,RotationDirection direction,float speed,List<Gear> GearsRotated)
    {
        currentGear.AddSpeedAndRotation(speed,GetDirection(direction));
        Gear[] gearsFound = currentGear.GetGearsAroundRadiusBasedOnLayer(LayerData.GearAreaLayer)
            .Where(gear =>
            {
                if (!GearsRotated.Contains(gear))
                {
                    //if gear is not found in existing list
                    //add it to the existing gear and put it in the gears Found
                    GearsRotated.Add(gear);
                    return true;
                }

                else
                {
                    //if the gear is already been rotated, ignore.
                    return false;
                }
            }).ToArray();
        //get all the gears that are connected to the gears

        foreach (Gear gear in gearsFound)
        {
            float newSpeed = calculateSpeedDrivenGear(currentGear, gear, speed);
            RotateConnectingGears(gear,ChangeDirection(direction),newSpeed, GearsRotated);
        }
    }
    
    private float calculateSpeedDrivenGear(Gear driverGear, Gear drivenGear, float speed)
    {
        float gearRatio = (float)drivenGear.Teeths/(float)driverGear.Teeths;  
        float calculatedSpeed = speed / gearRatio;
        return calculatedSpeed;

    }


    public enum RotationDirection
    {
        clockWise,
        antiClockWise
    }

    private Vector3 GetDirection(RotationDirection direction)
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

    private RotationDirection ChangeDirection(RotationDirection direction)
    {
        if(direction == RotationDirection.clockWise)
        {
            return RotationDirection.antiClockWise;
        }
        else
        {
            return RotationDirection.clockWise;   
        }
    }
}
