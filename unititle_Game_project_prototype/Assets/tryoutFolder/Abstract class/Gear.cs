using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Gear: MonoBehaviour ,IMoveable
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
    protected float speed ;
    protected Vector3 direction;
    private float gearRadius;

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

    private void Start()
    {
        gearRadius = entireGearArea.radius;
    }

    private Gear[] surroundingGear;
    private Gear DriverGear;

    private void Update()
    {
        
    }



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

    private Collider2D GetRespectiveColliderByLayer(LayerMask layer)
    {
        if (layer == LayerData.GearAreaLayer) return entireGearArea;
        else return innerGearArea;
    }

    public JointBehaviour[] GetJointsAroundGear()
    {
        Collider2D[] JointSelected = GetColliderAroundRadiusBasedOnLayer(LayerData.JointLayer);

        if (JointSelected.Length == 0) return null;

        JointBehaviour[] foundJoint = JointSelected.Select(collider =>
        {
            return collider.GetComponentInParent<JointBehaviour>();
        }).ToArray();
        return foundJoint;

    }

    public abstract void AddSpeedAndRotation(float speed, Vector3 direction); //depends on the gears as some gears can only rotate one direction
    private float GetCalculateSpeedDrivenGear(Gear driverGear, Gear drivenGear, float speed)
    {
        float gearRatio = (float)drivenGear.Teeths / (float)driverGear.Teeths;
        float calculatedSpeed = speed / gearRatio;
        return calculatedSpeed;

    }

    public abstract void Propogate(Gear previousGear =null, bool isJoint = false);
    protected void RotateDrivenGear(Gear driverGear, Gear drivenGear, bool isJoint = false)
    {
        float newSpeed;
        Vector3 direction;
        if (!isJoint)
        {
            newSpeed = GetCalculateSpeedDrivenGear(driverGear, drivenGear, driverGear.Speed);
            direction = ChangeDirection(driverGear.Direction);
        }
        else
        {
            newSpeed = driverGear.Speed;
            direction = drivenGear.Direction;
        }//same speed in same joint
        drivenGear.AddSpeedAndRotation(newSpeed, direction);
    }


    private Vector3 ChangeDirection(Vector3 direction)
    {
        if (direction == Vector3.forward) return Vector3.back;
        else return Vector3.forward;
    }

    public void CheckValidPosition()
    {
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if (surroundInnerGear != null)
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
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
        //do some code to visually show that the gear can be place or not.
    }


}

public interface IMoveable
{
    public int Cost { get; }

    public void CheckValidPosition();

    public void Move(Vector3 position);

}