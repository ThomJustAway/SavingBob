
using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System;
using System.Linq;
using System.Collections.Generic;

//Have a Gear class called Gear
//The dragable one are called dragable gears
// The 

public class DragableGear : Gear, IMoveable
{
    [SerializeField] private string nameOfElement;
    [SerializeField] private int cost;

    private SpriteRenderer spriteRenderer;
    public int Cost => cost;
    public string Name => nameOfElement;
    public GameObject Getprefab => gameObject;

    private ColorData colorData = ColorData.Instance;
    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

                Debug.DrawLine(distance.pointA, distance.pointB, Color.red, 2f);
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
        spriteRenderer.color = colorData.NormalColor;

    }

    public void Move(Vector3 position)
    {
        transform.position = position;
        Collider2D[] surroundInnerGear = GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if (surroundInnerGear == null)
        {
            return;
        }
        if (surroundInnerGear.Length > 0)
        {
            spriteRenderer.color = colorData.InvalidPositionColor;
        }
        else
        {
            spriteRenderer.color = colorData.ValidPositionColor;
        }
        //do some code to visually show that the gear can be place or not.
    }
}



