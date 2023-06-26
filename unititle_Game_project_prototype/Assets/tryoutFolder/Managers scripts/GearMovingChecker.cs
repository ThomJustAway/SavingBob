using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class GearMovingChecker : MonoBehaviour
//{

//    [SerializeField] private StartingGearClass startingGear;
//    [SerializeField] private float speed;
//    //do a recurrsive sequence;
//    private GearNode gearNode;

//    private void Update()
//    {
//        startingGear.GearHost.AddSpeedAndRotation(speed, GetVector3FromDirection(RotationDirection.clockWise));
//    }
//    //coroutine
//    /*
//        1.get all the gear that are avaliable
//        2.return to the update call.
//        3.rotate the gear.
//        4.If there is no more, then stop coroutine
//     */
//    private void Propogate(Gear currentGear, Gear previousGear = null, bool isJoint = false)
//    {
//        //if (previousGear != null)
//        //{
//        //    RotateGear(previousGear, currentGear, isJoint);
//        //}
//        //Gear[] gearSurrounding = currentGear.GetGearsAroundRadiusBasedOnLayer(LayerData.GearAreaLayer);
//        //JointBehaviour[] jointsSurrounding = currentGear.GetJointsAroundGear();
//        //if (jointsSurrounding != null)
//        //{
//        //    foreach (JointBehaviour joint in jointsSurrounding)
//        //    {
//        //        Gear jointedGear = joint.GetRespondingGear(currentGear);
//        //        if (jointedGear == null)
//        //        {
//        //            print("the joint is not connected to any gear");
//        //        }
//        //        else
//        //        {
//        //            print(jointedGear.name);
//        //            if (jointedGear != previousGear)
//        //            {
//        //                yield return StartCoroutine(Propogate(jointedGear, currentGear, true));
//        //            }
//        //        }

//        //        //if (jointedGear != previousGear)
//        //        //{
//        //        //    Propogate(jointedGear, currentGear, true);
//        //        //}
//        //    }
//        //}
//        ////this code need to change

//        //foreach (Gear gear in gearSurrounding)
//        //{
//        //    if (gear != previousGear)
//        //    {
//        //        yield return StartCoroutine(Propogate(gear, currentGear));
//        //    }
//        //}

//        //yield return StartCoroutine(Propogate(startingGear.GearHost));

//    }

//    //private void CreateGearNode()
//    //{

//    //}

//    private void RotateGear(Gear driverGear, Gear drivenGear,  bool isJoint = false)
//    {
//        float newSpeed;
//        Vector3 direction;
//        if (!isJoint)
//        {
//            newSpeed = GetCalculateSpeedDrivenGear(driverGear, drivenGear, driverGear.Speed);
//            direction = ChangeDirection(driverGear.Direction);
//        }
//        else
//        {
//            newSpeed = driverGear.Speed;
//            direction = drivenGear.Direction;
//        }//same speed in same joint
//        drivenGear.AddSpeedAndRotation(newSpeed, direction);
//    }


//    private float GetCalculateSpeedDrivenGear(Gear driverGear, Gear drivenGear, float speed)
//    {
//        float gearRatio = (float)drivenGear.Teeths/(float)driverGear.Teeths;  
//        float calculatedSpeed = speed / gearRatio;
//        return calculatedSpeed;

//    }

//    public enum RotationDirection
//    {
//        clockWise,
//        antiClockWise
//    }

//    private Vector3 GetVector3FromDirection(RotationDirection direction)
//    {
//        switch (direction)
//        {
//            case RotationDirection.clockWise:
//                return Vector3.forward;
//            case RotationDirection.antiClockWise:
//                return Vector3.back;
//            default: 
//                return Vector3.forward;
//        }
//    }

//    private Vector3 ChangeDirection(Vector3 direction)
//    {
//        if (direction == Vector3.forward) return Vector3.back;
//        else return Vector3.forward;
//    }

//    private RotationDirection ChangeDirection(RotationDirection direction)
//    {
//        if(direction == RotationDirection.clockWise)
//        {
//            return RotationDirection.antiClockWise;
//        }
//        else
//        {
//            return RotationDirection.clockWise;   
//        }
//    }
//}
