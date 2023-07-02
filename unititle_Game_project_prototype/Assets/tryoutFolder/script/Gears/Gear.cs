
using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System;
using System.Linq;
using System.Collections.Generic;

public class Gear : RotatableElement, IMoveable
{
    [SerializeField] private CircleCollider2D entireGearArea;
    [SerializeField] private Collider2D innerGearArea;
    [SerializeField] private string nameOfElement;
    [SerializeField] private int cost;
    
    private float gearRadius;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ColorScriptableObject colorData;
    public int Cost => cost;
    public string Name => nameOfElement;
    public GameObject Getprefab => gameObject;

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
            return transform.position.z + 0.7f;
        }
    }

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        
        if(joint != null)
        {
            List<RotatableElement> newElements = new List<RotatableElement>() {joint};
            for(int i = 0; i < surroundingRotatableComponents.Length; i++)
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
            .Where(collider => {
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

                Debug.DrawLine(distance.pointA,distance.pointB,Color.red, 2f);
                Debug.DrawLine(distance.pointA, Vector3.zero, Color.yellow, 2f);
                Debug.DrawLine(distance.pointB, Vector3.zero, Color.black, 2f);
                print($"Point A:{distance.pointA} Point B: {distance.pointB}");
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
        spriteRenderer.color = colorData.normalColor;

    }

    public void Move(Vector3 position)
    {
        transform.position = position;
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if(surroundInnerGear == null)
        {
            return;
        }
        print(surroundInnerGear.Length);
        if (surroundInnerGear.Length > 0)
        {
            spriteRenderer.color = colorData.invalidPositionColor;
        }
        else
        {
            spriteRenderer.color = colorData.validPositionColor; 
        }
        //do some code to visually show that the gear can be place or not.
    }

}
