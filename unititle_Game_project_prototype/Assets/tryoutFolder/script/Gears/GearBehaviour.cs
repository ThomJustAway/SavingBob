using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : Gear, IRotate
{
    //type of rotation
    //1.transform.rotate
    //

    [Range(-50f, 50f)]
    [SerializeField] private float currentSpeed;
    public float friction = 10f;
    public bool isStartingGear = false;//refactor this later
    public Vector3 currentDirection = Vector3.zero;
    [SerializeField] private int teeths;

    public void Start()
    {
        if(isStartingGear)
        {
            SetSpeed(50f);
            friction = 0f;
            currentDirection = Direction.Clockwise;
        }
        else
        {
            currentSpeed = 0f;
        }
        Radius = 1.17f;
    }

    private void Update()
    {
        RotateGear(currentSpeed, currentDirection);

        if (!isStartingGear)
        {
            AddFriction();
        }
    }
    [ContextMenu ("AddSpeed")]
    private void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }


    //This calculate and give speed to the another connected gear through the rotating gear
    private float GiveSpeed(int gearsTeeth)
    {//can also have torque to give speed of rotation
        float gearRatio = teeths / gearsTeeth;
        float targetGearSpeed = currentSpeed / gearRatio; 
        return targetGearSpeed;
    } 
    private void AddFriction()
    {
        if(currentSpeed > 0)
        {
            currentSpeed -= friction*Time.deltaTime;
        }
        else
        {
            currentSpeed = 0;
        }
    }

    public void RotateGear(float speed , Vector3 direction)
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }


}
