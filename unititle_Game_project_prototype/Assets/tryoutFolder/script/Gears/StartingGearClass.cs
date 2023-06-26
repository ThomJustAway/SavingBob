//using System.Collections;
//using UnityEngine;


//public class StartingGearClass : MonoBehaviour
//{
//    private Gear gearHost;
//    private Gear GearHost { get { return gearHost; } }
//    [SerializeField] private float speed;
//    [SerializeField] private RotationDirection rotationDirection;

//    private void Start()
//    {
//        gearHost = GetComponent<Gear>();
//    }

//    private void Update()
//    {
//        Vector3 direction = GetVector3FromDirection(rotationDirection);
//        gearHost.SetNoFriction();
//        gearHost.AddSpeedAndRotation(speed, direction);
//    }

//    private enum RotationDirection
//    {
//        clockWise,
//        antiClockWise
//    }

//    private Vector3 GetVector3FromDirection(RotationDirection direction)
//    {
//        switch (direction)
//        {
//            case RotationDirection.clockWise:
//                return Vector3.back;
//            case RotationDirection.antiClockWise:
//                return Vector3.forward;
//            default:
//                return Vector3.forward;
//        }
//    }
//}



using Assets.tryoutFolder.script;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class StartingGearClass : MonoBehaviour
{
    private RotatableElement gearHost;
    [SerializeField] private float speed;
    [SerializeField] private RotationDirection rotationDirection;

    private void Start()
    {
        gearHost = GetComponent<RotatableElement>();
    }

    private void Update()
    {
        Vector3 direction = GetVector3FromDirection(rotationDirection);
        gearHost.AddSpeedAndRotation(speed, direction);
    }

    private enum RotationDirection
    {
        clockWise,
        antiClockWise
    }

    private Vector3 GetVector3FromDirection(RotationDirection direction)
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