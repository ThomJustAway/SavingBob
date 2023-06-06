using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GearMovingChecker : MonoBehaviour
{

    [SerializeField] private StartingGearClass startingGear;
    [SerializeField] private float speed;
    //do a recurrsive sequence;
    private void Start()
    {
        StartCoroutine(ImproveConnectingGear());
    }

    private void RotateConnectingGears(Gear currentGear,RotationDirection direction,float speed,List<Gear> GearsRotated)
    {
        currentGear.AddSpeedAndRotation(speed,GetVector3FromDirection(direction));

        JointBehaviour[] jointsConnectedToTheGear = currentGear.GetJointsAroundGear();

        if (jointsConnectedToTheGear != null)
        {
            foreach (var joint in jointsConnectedToTheGear)
            {
                print(joint.gameObject.name);
                //    Gear connectedGear = joint.GetRespondingGear(currentGear);
                //    List<Gear> newGearConnected = new List<Gear>();
                //    RotateConnectingGears(connectedGear, ChangeDirection(direction), speed, newGearConnected);
                //}
            }
        }

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
            float newSpeed = GetCalculateSpeedDrivenGear(currentGear, gear, speed);
            RotateConnectingGears(gear,ChangeDirection(direction),newSpeed, GearsRotated);
        }


    }

    //coroutine
    /*
        1.get all the gear that are avaliable
        2.return to the update call.
        3.rotate the gear.
        4.If there is no more, then stop coroutine
     */

    //maybe use delegates for the rotation?
    /*
        1. gear will get the gears that are around
        2. When gear is found, the gear will subscribe to the delegate that will rotate the gear
        3. once the finding process is finish, call the delegate to invoke the rotate gear to rotate all the gear that are connected.
        4. repeat process 
     */


    //this code has a problem of taking a very long time. Please figure this out.
    private IEnumerator ImproveConnectingGear()
    {
        //maybe have a list of gear that is connected
        List<Gear> previousGearList = new List<Gear>() { startingGear.GearHost }; // change this to add multiple gear
        startingGear.GearHost.AddSpeedAndRotation(speed, GetVector3FromDirection(RotationDirection.clockWise));
        RotationDirection direction = RotationDirection.antiClockWise;

        List<Gear> gearRotated = new List<Gear>() { startingGear.GearHost };

        while (previousGearList.Count != 0)
        {
            List<Gear> currentGearList = new List<Gear>();
            print("Code in loop");
            for (int i = 0; i < previousGearList.Count; i++)
            {
                Gear currentGear = previousGearList[i];

                //JointBehaviour[] jointsConnectedToTheGear = currentGear.GetJointsAroundGear();
                Gear[] gearsFound = currentGear.GetGearsAroundRadiusBasedOnLayer(LayerData.GearAreaLayer);

                //if (jointsConnectedToTheGear != null)
                //{
                //    foreach (var joint in jointsConnectedToTheGear)
                //    {
                //        Gear connectedGear = joint.GetRespondingGear(currentGear);
                //        RotateGear(currentGear, connectedGear, direction, true);
                //        currentGearList.Add(connectedGear);
                //    }
                //}

                foreach (Gear foundgear in gearsFound) //memory usage is high
                {
                    if (currentGear != foundgear)
                    {
                        RotateGear(foundgear, currentGear, direction);
                        currentGearList.Add(foundgear);
                    }
                }
            }
            previousGearList = currentGearList;
            direction = ChangeDirection(direction);
        }

        yield return StartCoroutine(ImproveConnectingGear());
    }

    private void RotateGear(Gear driverGear, Gear drivenGear, RotationDirection direction, bool isJoint =false)
    {
        float newSpeed;
        if (!isJoint) newSpeed = GetCalculateSpeedDrivenGear(driverGear, drivenGear, driverGear.Speed);
        else newSpeed = driverGear.Speed; //same speed in same joint
        drivenGear.AddSpeedAndRotation(newSpeed, GetVector3FromDirection(direction));
    }

    //Objective: to rotate gears in using iteration
    /*
        .need to take reference from the previous gear to know the speed of the driven gear
        1. starting with the first gear > next gear > next gear
        2. find all the gear and add them to the event to rotate gear
        3. store that into the gear list, and then iterate them over.
        3. do this until there is no more gears left.
     */

    private float GetCalculateSpeedDrivenGear(Gear driverGear, Gear drivenGear, float speed)
    {
        print("Driver gear: "+  driverGear.gameObject.name + "Driven gear: "+drivenGear.gameObject.name );
        float gearRatio = (float)drivenGear.Teeths/(float)driverGear.Teeths;  
        float calculatedSpeed = speed / gearRatio;
        return calculatedSpeed;

    }


    public enum RotationDirection
    {
        clockWise,
        antiClockWise
    }

    private Vector3 GetVector3FromDirection(RotationDirection direction)
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
