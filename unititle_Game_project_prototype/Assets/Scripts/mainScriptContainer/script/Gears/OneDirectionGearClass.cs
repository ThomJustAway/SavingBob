using Assets.tryoutFolder.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionGearClass : Gear
{
    [SerializeField] private RotationDirectionClass.RotationDirection direction;
    private Vector3 vector3Direction;

    protected override void Start()
    {
        base.Start();
        vector3Direction = RotationDirectionClass.GetVector3FromDirection(direction);
    }

    protected override void Update()
    {
        surroundingElements = FindingRotatingElement();
        if (driverElement != null && !isStartingElement)
        {
            CheckingDriverElement();
        }

        if (speed >= 0) //can also be >= 0 to make the gear jam.
        {
            RotateElementVisually();
            RotateSurroundingElements();
            if (hasFriction && speed != 0)
            {
                ApplyFriction();
            }
        }
        else
        {
            speed = 0;
        }
    }

    public override void AddSpeedAndRotation(float speed, Vector3 rotation, RotatableElement driver = null)
    {
        if (rotation == vector3Direction)
        {
            base.AddSpeedAndRotation(speed, rotation, driver);
        }
    }




}
