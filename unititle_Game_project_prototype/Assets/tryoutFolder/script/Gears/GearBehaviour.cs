using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : Gear
{
    [Range(-50f, 50f)]
    public float friction = 20f;
    [SerializeField] private float gearRadius;
    [SerializeField] private int teeths;
    [SerializeField] private Collider2D thisEntireGearArea;
    [SerializeField] private Collider2D thisInnerGearArea;

    private float speed =0;
    private Vector3 direction;

    private void Start()
    {
        Setter(gearRadius, teeths, thisInnerGearArea, thisEntireGearArea);
    }

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
    }


}
