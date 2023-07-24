using Assets.tryoutFolder.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionGearClass : Gear
{
    // readme
    /*
    one direction gear are gears that cant be rotated in a certain direction (clockwise or anticlockwise)
    
    some changes to the inital Rotatable element:
    1. It will add itself as a driver jamming gear if it detect it is being 
    rotated at another direction
    2. To compensate, it will check if the driver jamming element is itself. (CheckJammingElement())
    */
    [SerializeField] private RotationDirectionClass.RotationDirection direction;
    private Vector3 vector3Direction;

    protected override void Start()
    {
        base.Start();
        vector3Direction = RotationDirectionClass.GetVector3FromDirection(direction);
        if (direction == RotationDirectionClass.RotationDirection.clockWise)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    protected override void CheckJammingElement()
    {
        if (driverJammingElement == this)
        {
            if (surroundingElements.Length > 0)
            {
                return;
            }
            else
            {
                driverJammingElement = null;
            }
        }
        else
        {
            base.CheckJammingElement();
        }
    }

    public override void AddSpeedAndRotation(float speed, Vector3 rotation, RotatableElement driver = null)
    {
        if (rotation == vector3Direction)
        {
            base.AddSpeedAndRotation(speed, vector3Direction, driver);
            driverJammingElement = null;
        }
        else
        {
            this.speed = 0;
            driverJammingElement = this;
        }
    }
}
