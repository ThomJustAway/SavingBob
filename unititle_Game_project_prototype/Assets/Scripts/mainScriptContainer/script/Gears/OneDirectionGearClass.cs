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
    [SerializeField] private RotationDirectionClass.RotationDirection direction; //this will determine the rotation direction
    private Vector3 vector3Direction;

    protected override void Start()
    {
        base.Start(); //do same pattern as the normal gear behaviour
        vector3Direction = RotationDirectionClass.GetVector3FromDirection(direction);//get the intented rotation for the gear
        if (direction == RotationDirectionClass.RotationDirection.clockWise)
        { //this is to show the sprite of the one direction gear. The sprite used is initially anticlockwise.
          //so if it is clockwise, the sprite has to flip
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    protected override void CheckJammingElement() 
    {//one directional gear has a special jamming sequence
        //sometimes, it will be the one that is jamming so it will check if the jamming gear is itself
        if (driverJammingElement == this)//if it is
        {
            if (surroundingElements.Length > 0  )//it will check the surrounding to see if there is any gears
            {//problem here. will continue to jam gear even if it is rotated at the correct direction
                //just found out recently (9 august)
                return;
            }
            else
            {
                //else make it null as there is nothing to addspeed and rotation to the gear
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
        if (rotation == vector3Direction)//if correct direction
        {
            base.AddSpeedAndRotation(speed, vector3Direction, driver);//do the same thing where addspeed and rotation
            driverJammingElement = null;
        }
        else
        {
            this.speed = 0; //stop the gear
            driverJammingElement = this;//and add itself as the jamming element to jam the other gears
        }
    }
}
