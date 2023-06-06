using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


//what to do
// make an customer editor for all the classes that inherit the gear class and make direct changes there
public abstract class Gear: MonoBehaviour
{
    [SerializeField]private float gearRadius;
    [SerializeField]private int gearTeeth;
    [SerializeField]private Collider2D entireGearArea;
    [SerializeField]private Collider2D innerGearArea;

    protected float speed ;
    protected Vector3 direction;

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

    public abstract void AddSpeedAndRotation(float speed, Vector3 direction);
    
    


}