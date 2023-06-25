//using System.Collections;
//using UnityEditor.Rendering;
//using UnityEngine;

//public class EndGearClass : MonoBehaviour
//{
//    private Gear gearHost;
//    public Gear GearHost { get { return gearHost; } }
//    private bool isActivated;
//    [SerializeField] private float speedCondition;
//    [SerializeField] private TypeOfRotatingCondition rotatingCondition;

//    public bool IsActivated { get { return isActivated; } }

//    private enum TypeOfRotatingCondition
//    {
//        clockwise,
//        antiClockwise,
//        none
//    }

//    private void Start()
//    {
//        gearHost = GetComponent<Gear>();
//    }

//    private void Update()
//    {
//        isActivated = IfConditionMet(gearHost.Speed, gearHost.Direction);
//    }

//    public bool IfConditionMet(float speed, Vector3 direction)
//    {
//        bool rotatationConditionMet = GetRotationConditionIsMet(direction);
//        bool speedConditionMet = GetSpeedConditionIsMet(speed);
//        bool conditionMet = rotatationConditionMet && speedConditionMet;
//        return conditionMet;
//    }

//    private bool GetRotationConditionIsMet(Vector3 direction)
//    {
//        switch (rotatingCondition)
//        {
//            case TypeOfRotatingCondition.clockwise:
//                if (direction == Vector3.forward)
//                    return true;
//                break;
//            case TypeOfRotatingCondition.antiClockwise:
//                if (direction == Vector3.back)
//                    return true;
//                break;
//            case TypeOfRotatingCondition.none:
//                return true;

//            default:
//                return false;
//        }
//        return false;
//    }
//    // the naming conventiion here is confusing

//    private bool GetSpeedConditionIsMet(float speed)
//    {
//        if (speed > speedCondition)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    //re look at this later
//}



