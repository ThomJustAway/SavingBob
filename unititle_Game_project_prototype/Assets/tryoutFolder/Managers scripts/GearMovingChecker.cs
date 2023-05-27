using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearMovingChecker : MonoBehaviour
{

    [SerializeField] private StartingGearBehaviour startingGear;
    [SerializeField] private LayerMask gearAreaMask;
    [SerializeField] private float speed;
    //do a recurrsive sequence;

    private void Update()
    {
        List<Gear> GearConnected = new List<Gear>();
        GearConnected.Add(startingGear);
        RotateConnectingGears(startingGear,Direction.clockWise,speed,GearConnected);
    }

    private void RotateConnectingGears(Gear currentGear,Direction direction,float speed,List<Gear> GearsRotated)
    {
        currentGear.AddSpeedAndRotation(speed,GetDirection(direction));

        Gear[] gearsFound = currentGear.GetGearsAroundRadius(currentGear.gameObject.transform.position, 
            gearAreaMask,
            currentGear.EntireGearArea).Where(gear =>
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


    private enum Direction
    {
        clockWise,
        antiClockWise
    }

    private Vector3 GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.clockWise:
                return Vector3.forward;
            case Direction.antiClockWise:
                return Vector3.back;
            default: 
                return Vector3.forward;
        }
    }

    private Direction ChangeDirection(Direction direction)
    {
        if(direction == Direction.clockWise)
        {
            return Direction.antiClockWise;
        }
        else
        {
            return Direction.clockWise;   
        }
    }
}
