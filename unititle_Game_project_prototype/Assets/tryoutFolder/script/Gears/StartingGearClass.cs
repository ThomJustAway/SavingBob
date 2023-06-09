﻿using System.Collections;
using UnityEngine;


public class StartingGearClass : MonoBehaviour
{
    private Gear gearHost;
    private Gear GearHost { get { return gearHost; } }
    [SerializeField] private float speed;
    [SerializeField] private RotationDirection rotationDirection;

    private void Start()
    {
        gearHost = GetComponent<Gear>();
    }

    private void Update()
    {
        Vector3 direction = GetVector3FromDirection(rotationDirection);
        gearHost.AddSpeedAndRotation(speed, direction);
        gearHost.Propogate();
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
                return Vector3.forward;
            case RotationDirection.antiClockWise:
                return Vector3.back;
            default:
                return Vector3.forward;
        }
    }
}
