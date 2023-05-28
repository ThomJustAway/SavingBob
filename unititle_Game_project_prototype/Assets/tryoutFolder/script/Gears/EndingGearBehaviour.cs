using System.Collections;
using UnityEngine;

public interface IIsActivated
{
    public bool IsActivated();
}


public class EndingGearBehaviour :Gear , IIsActivated
{
    [Range(-50f, 50f)]
    public float friction = 20f;
    [SerializeField] private float gearRadius;
    [SerializeField] private int teeths;
    [SerializeField] private Collider2D thisEntireGearArea;
    [SerializeField] private Collider2D thisInnerGearArea;

    private float speed = 0;
    private Vector3 direction;
    private bool activated = false;

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
        CheckActivatedCondition();

    }

    private void CheckActivatedCondition()
    {
        if (speed >= 30)
        {
            activated = true;
        }
        else
        {
            activated= false;
        }

        print("The gear is " + activated);
    }
    private void AddFriction()
    {
        speed -= friction * Time.deltaTime;
    }

    private void RotateGear()
    {
        transform.Rotate(speed * direction * Time.deltaTime);
    }

    public override void AddSpeedAndRotation(float speed, Vector3 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    public bool IsActivated()
    {
        return activated;
    }



}
