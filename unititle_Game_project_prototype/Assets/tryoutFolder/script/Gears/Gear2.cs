using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System;
using static UnityEngine.Rendering.VolumeComponent;
using System.Linq;

public class Gear2 : RotatableElement, IMoveable
{
    [SerializeField] private CircleCollider2D entireGearArea;
    [SerializeField] private Collider2D innerGearArea;

    [SerializeField] private string name;
    [SerializeField] private int cost;

    private float gearRadius;


    public int Cost => cost;
    public string Name => name;
    public GameObject Getprefab =>  gameObject;

    private float MinDept
    {
        get
        {
            return transform.position.z - 0.5f;
        }
    }
    private float MaxDept
    {
        get
        {
            return transform.position.z + 0.5f;
        }
    }

    private void Start()
    {
        gearRadius = entireGearArea.radius;
    }

    protected override void FindingRotatingElement()
    {
        var surroundingGears = GetColliderAroundRadiusBasedOnLayer(LayerData.GearAreaLayer).Select(collider => collider.GetComponent<RotatableElement>()).ToArray();
        var joint = GetJointComponent();

        if(joint != null)
        {
            surroundingGears.Append(joint);
        }

        surroundingElements = surroundingGears;
    }

    protected override void RotateElementVisually()
    {
        transform.Rotate(speed * rotationDirection * Time.deltaTime);
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

    public RotatableElement GetJointComponent()
    {
        Collider2D childCollider = Physics2D.OverlapCircle(transform.position, gearRadius, LayerData.JointLayer, MinDept, MaxDept);
        if(childCollider != null)
        {
            return childCollider.GetComponentInParent<RotatableElement>();
        }
        return null;
    }

    //public Gear[] GetGearsAroundRadiusBasedOnLayer(LayerMask layer) // will get gears base on the layer choosen
    //{
    //    Collider2D[] surroundingGears = GetColliderAroundRadiusBasedOnLayer(layer);
    //    //get all the collider 2D from speicfic layer and remove the current gear layer

    //    Gear[] selectedGear = surroundingGears.Select(collider =>
    //    {
    //        return collider.GetComponentInParent<Gear>();
    //    }).ToArray();

    //    return selectedGear;
    //}

    private Collider2D GetRespectiveColliderByLayer(LayerMask layer)
    {
        if (layer == LayerData.GearAreaLayer) return entireGearArea;
        else return innerGearArea;
    }


    public void CheckValidPosition()
    {
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        Collider2D[] surroundingJoint = GetColliderAroundRadiusBasedOnLayer(LayerData.JointLayer);

        if (surroundInnerGear.Length > 0)
        {
            //improve the finding of the valid position
            ColliderDistance2D distance;

            foreach (var innerGear in surroundInnerGear)
            {
                distance = innerGear.Distance(entireGearArea);
                Vector2 resolveDistance = Math.Abs(distance.distance) * distance.normal;
                transform.Translate(resolveDistance);
            }
        }
        else if (surroundingJoint.Length > 0)
        {
            if (surroundingJoint.Length == 1)
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
