using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Gear: MonoBehaviour ,IMoveable
{
    //README
    /*
        Gear abstract class is a class which every gear that exist in the game will have.
        They have the basic gear functionality to make a gear work
        
        Still a work in progress.

        EntireGearArea: is the area in which the gear can be rotated (the edge of the gear). 
        InnterGearArea: is the area in which the gear cant be rotated (the inner circle of the gear)
        
        How the gears are rotated:
        1.Recursion. The gear will go through the surrounding gears and rotate them. (Problem: stack Overflow)
        
        The rest of the function in the class are very self explaintory.
     */

    [SerializeField]private int gearTeeth;
    [SerializeField]private CircleCollider2D entireGearArea;
    [SerializeField]private Collider2D innerGearArea;
    [SerializeField] private int cost;
    [Range(0, 50f)]
    [SerializeField] private float friction = 10f;
    [SerializeField] private string name;
    protected float speed ;
    protected Vector3 direction;
    private float gearRadius;
    private Gear[] surroundingGear;
    private Gear driverGear;

    public string Name { get { return name; } }
    public int Cost { get { return cost; } }
    public float Speed { get { return speed; } }
    public Vector3 Direction { get { return direction; } }
    public Collider2D EntireGearArea { get { return entireGearArea; } }
    public int Teeths { get { return gearTeeth; } }
    private float MinDept { get {
            return transform.position.z - 0.5f;
        } }
    private float MaxDept { get
        {
            return transform.position.z + 0.5f;
        } }
    public Gear DriverGear { get { return driverGear; } }

    public GameObject Getprefab { get { return gameObject; } }

    private void Start()
    {
        gearRadius = entireGearArea.radius;
    }


    private void Update()
    {
        surroundingGear = GetGearsAroundRadiusBasedOnLayer(LayerData.GearAreaLayer);
        if(driverGear != null)
        {
            CheckIfDriverGearStillExist();
        }
        if (speed > 0)
        {
            RotateGear();
            foreach (Gear gear in surroundingGear.Where(selectedGear => selectedGear != driverGear)) 
            {
                Vector3 newDirection = ChangeDirection(direction);
                float newSpeed = GetCalculateSpeedDrivenGear(this,gear,speed);
                gear.AddSpeedAndRotation(newSpeed, newDirection, this);
            }
            //after finishing to rotate all the gears
            AddFriction();
        }
        else if(speed <=0)
        {
            speed = 0;
        }
        //check if gear driven.
    }

    private void CheckIfDriverGearStillExist()
    {
        for(int i = 0; i < surroundingGear.Length; i++)
        {
            if (surroundingGear[i] == driverGear)
            {
                if(driverGear.Speed > 0) //check if the driverGear is still rotating
                {
                    return; 
                }
                break;
            }
        }
        driverGear = null; //else set the driver gear to null;

    } //take apart this

    public Collider2D[] GetColliderAroundRadiusBasedOnLayer(LayerMask layer)
    {
        Collider2D selectedCollider = GetRespectiveColliderByLayer(layer);
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(transform.position, gearRadius, layer, MinDept, MaxDept)
            .Where(collider => {
                return collider != selectedCollider;
            })
            .ToArray();
        return surroundingGears;
    }

    public Gear[] GetGearsAroundRadiusBasedOnLayer(LayerMask layer) // will get gears base on the layer choosen
    {
        Collider2D[] surroundingGears = GetColliderAroundRadiusBasedOnLayer(layer);
        //get all the collider 2D from speicfic layer and remove the current gear layer

        Gear[] selectedGear = surroundingGears.Select(collider =>
        {
            return collider.GetComponentInParent<Gear>();
        }).ToArray();

        return selectedGear;
    }

    public void SetNoFriction()
    {
        friction = 0;
    } //this is for starting gears who dont need friction

    private Collider2D GetRespectiveColliderByLayer(LayerMask layer)
    {
        if (layer == LayerData.GearAreaLayer) return entireGearArea;
        else return innerGearArea;
    }

    public virtual void AddSpeedAndRotation(float speed, Vector3 direction, Gear driverGear = null) 
    {
        this.driverGear = driverGear;
        this.speed = speed;
        this.direction = direction;
    } //depends on the gears as some gears can only rotate one direction

    private void RotateGear()
    {
        transform.Rotate(speed * direction * Time.deltaTime);
    }

    private void AddFriction()
    {
        speed -= friction * Time.deltaTime;
    }

    private float GetCalculateSpeedDrivenGear(Gear driverGear, Gear drivenGear, float speed)
    {
        float gearRatio = (float)drivenGear.Teeths / (float)driverGear.Teeths;
        float calculatedSpeed = speed / gearRatio;
        return calculatedSpeed;

    }

    private Vector3 ChangeDirection(Vector3 direction)
    {
        if (direction == Vector3.forward) return Vector3.back;
        else return Vector3.forward;
    }

    //this is the imoveable functions
    public void CheckValidPosition()
    {
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        Collider2D[] surroundingJoint = GetColliderAroundRadiusBasedOnLayer(LayerData.JointLayer);
        
        if (surroundInnerGear.Length > 0 )
        {
            //improve the finding of the valid position
            ColliderDistance2D distance;

            foreach (var innerGear in surroundInnerGear)
            {
                distance = innerGear.Distance(EntireGearArea);
                Vector2 resolveDistance = Math.Abs(distance.distance) * distance.normal;
                transform.Translate(resolveDistance);
            }
        }
        else if (surroundingJoint.Length > 0 )
        {
            if(surroundingJoint.Length == 1)
            {
                Collider2D joint = surroundingJoint[0];
                transform.position = joint.gameObject.transform.position;
            }
            else
            {
                Debug.LogWarning("Invalid position");
            }
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
        //do some code to visually show that the gear can be place or not.

    }


}

