using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : Gear
{
    [Range(-50f, 50f)]
    public float friction = 20f;
    private Gear[] surroundingGear;

    private void Update()
    {
        if (speed > 0)
        {
            RotateGear();
            AddFriction();
        }
        else
        {
            speed = 0;
        }
        Gear[] surroundingGear = GetGearsAroundRadiusBasedOnLayer(LayerData.GearAreaLayer);
    }

    private void AddFriction()
    {
        speed -= friction*Time.deltaTime;
    }

    private void RotateGear()
    {
        transform.Rotate(speed * direction * Time.deltaTime);
    }

    public override void AddSpeedAndRotation(float speed , Vector3 direction)
    {
        this.speed = speed;
        this.direction = direction;
        transform.Rotate(speed * direction * Time.deltaTime);
    }

    public override void Propogate(Gear previousGear =null, bool isConnected =false)
    {
        if(previousGear != null)
        {
            RotateDrivenGear(previousGear, this, isConnected);
        }

        //JointBehaviour[] connectedJoints = GetJointsAroundGear();

        foreach(Gear gear in  surroundingGear)
        {
            if(gear !=  previousGear)
            {
                gear.Propogate(this);
            }
        }
        print("end of function");

    }
}

//use events : gear to subscribe to the event.