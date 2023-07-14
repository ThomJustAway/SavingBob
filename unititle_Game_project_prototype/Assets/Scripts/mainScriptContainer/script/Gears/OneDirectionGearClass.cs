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
        if (direction == RotationDirectionClass.RotationDirection.clockWise)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    protected override void CheckJammingElement()
    {
        if (driverJammingElement == this) return;
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
