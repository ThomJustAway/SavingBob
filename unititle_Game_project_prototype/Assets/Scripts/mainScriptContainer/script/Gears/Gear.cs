using Assets.tryoutFolder.script;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Gear : RotatableElement
{
    [SerializeField] protected CircleCollider2D entireGearArea;
    [SerializeField] protected Collider2D innerGearArea;
    protected float gearRadius;
    protected float MinDept
    {
        get
        {
            return transform.position.z - 0.5f;
        }
    }

    protected float MaxDept
    {
        get
        {
            return transform.position.z + 0.7f;
        }
    }


    protected override void Start()
    {
        base.Start();
        gearRadius = entireGearArea.radius;
    }

    protected override RotatableElement[] FindingRotatingElement()
    {
        var surroundingRotatableComponents = GetColliderAroundRadiusBasedOnLayer(LayerData.GearAreaLayer)
            .Select(collider => collider.GetComponentInParent<RotatableElement>()).ToArray();
        var joint = GetJointComponent();

        if (joint != null)
        {
            List<RotatableElement> newElements = new List<RotatableElement>() { joint };
            for (int i = 0; i < surroundingRotatableComponents.Length; i++)
            {
                newElements.Add(surroundingRotatableComponents[i]);
            }
            return newElements.ToArray();
        }
        return surroundingRotatableComponents;
    }

    protected override void RotateElementVisually()
    {
        transform.Rotate(speed * rotationDirection * Time.deltaTime);
    }

    public Collider2D[] GetColliderAroundRadiusBasedOnLayer(LayerMask layer)
    {
        Collider2D selectedCollider = GetRespectiveColliderByLayer(layer);
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(transform.position, gearRadius, layer, MinDept, MaxDept)
            .Where(collider =>
            {
                return collider != selectedCollider;
            })
            .ToArray();
        return surroundingGears;
    }

    public RotatableElement GetJointComponent()
    {
        Collider2D childCollider = Physics2D.OverlapCircle(transform.position, gearRadius, LayerData.JointLayer, MinDept, MaxDept);
        if (childCollider != null)
        {
            return childCollider.GetComponentInParent<RotatableElement>();
        }
        return null;
    }

    protected Collider2D GetRespectiveColliderByLayer(LayerMask layer)
    {
        if (layer == LayerData.GearAreaLayer) return entireGearArea;
        else return innerGearArea;
    }
}
