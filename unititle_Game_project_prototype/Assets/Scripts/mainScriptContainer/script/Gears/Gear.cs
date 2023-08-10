using Assets.tryoutFolder.script;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Gear : RotatableElement
{
    //readme
    /*
        Read Rotatable elements before reading this.

    How the Gear does rotation?
    1. There are two collider in a gear prefab.
        - entire gear area
            :It is the entire area of the gear
        - inner gear area
            : it only consist of the area that is not affected by the teeth.
        *look at the prefabs of the gear to understand what this means
    2. The entiregeararea collider will find the surrounding gears based on
        Physic2d collision. Which can be seen in FindingRotatingElement() function
    3. Afterwards, it follows the update called by the rotatable elements.

    some thing to note:
    1. the Min dept and max dept are used for the gears know which layer is need to rotate.
    2. gear radius is used to detech surrounding rotatable element based on physic2d
    3. InnerGear Radius is used for circle calculator. To find out more look at the circle calculator
    (To help players to move gears to a valid position)

    */

    [SerializeField] protected CircleCollider2D entireGearArea;//a collider that cover the entire area of the gear
    [SerializeField] protected CircleCollider2D innerGearArea; //collider that will cover the inner area of the gear (a circle that the gear tooth does not touch)
    //you can look at gear prefab to better understand this concept
    public float GearRadius { get; private set; } //gear radius for raycasting 
    public float InnerGearRadius { get; private set; } //this raidus is used for circle calculator to calculate seperation between two gears
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
    //this min and max dept is for restricting the ray casting to its only dept.

    protected override void Start()
    {
        //setting up the class
        base.Start();
        GearRadius = entireGearArea.radius; 
        InnerGearRadius = innerGearArea.radius;
    }

    protected override void RotateElementVisually()
    {
        //show the rotation in game
        transform.Rotate(speed * rotationDirection * Time.deltaTime);
    }

    protected override RotatableElement[] FindingRotatingElement()
    {
        //this is how a gear would search the surrounding gear area
        var surroundingRotatableComponents = GetColliderAroundRadiusBasedOnLayer(LayerData.EntireAreaLayer)
            .Select(collider => collider.GetComponentInParent<RotatableElement>()).ToArray(); //this is to search for gear around the area

        var joint = GetJointComponent();

        //getting surrounding rotatable element. Both joint and gears

        if (joint != null)
        { //if there is a joint component, then make a list to compile all the rotatable elements together using a list
            List<RotatableElement> newElements = new List<RotatableElement>() { joint };
            for (int i = 0; i < surroundingRotatableComponents.Length; i++)
            {
                newElements.Add(surroundingRotatableComponents[i]);
            }
            return newElements.ToArray(); 
        }
        //this means no joint component so just send out the current surrounding rotatable component
        return surroundingRotatableComponents; 
    }


    public Collider2D[] GetColliderAroundRadiusBasedOnLayer(LayerMask layer)
    {
        Collider2D selectedCollider = GetRespectiveColliderByLayer(layer);
        //get respective collider so that when doing ray cast, the surrounding gear will not include selected collider as one of collider
        //when doing raycasting, there is min and max dept so that gear know to only look at their own area.
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(transform.position, GearRadius, layer, MinDept, MaxDept)
            .Where(collider =>
            {
                return collider != selectedCollider;
            })
            .ToArray();
        return surroundingGears;
    }

    public RotatableElement GetJointComponent()
    {
        //same as GetColliderAroundRadiusBasedOnLayer() but change the layer to jointlayer as the rotatable element have to
        //find the joint at the jointLayer
        Collider2D childCollider = Physics2D.OverlapCircle(transform.position, GearRadius, LayerData.JointLayer, MinDept, MaxDept);
        //this variable is called child collider as the rotatable element is in the parent container. Thus, I have to get the parent component of the child container
        if (childCollider != null)
        {
            return childCollider.GetComponentInParent<RotatableElement>();
        }
        return null;
    }

    protected Collider2D GetRespectiveColliderByLayer(LayerMask layer)
    {
        //this function just return a collider respective to the layer
        if (layer == LayerData.EntireAreaLayer) return entireGearArea;
        else return innerGearArea;
    }
}
