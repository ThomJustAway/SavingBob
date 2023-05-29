using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Collider2D EntireGearArea { get { return entireGearArea; } }
    public int Teeths { get { return gearTeeth; } }


    public Gear[] GetGearsAroundRadius(Vector3 currentPosition,LayerMask layer,Collider2D thisEntireGearArea)
    {
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(currentPosition, gearRadius, layer)
            .Where(collider => {
                return collider != thisEntireGearArea;
                })
            .ToArray();
        //get all the collider 2D from speicfic layer and remove the current gear layer

        Gear[] selectedGear = surroundingGears.Select(collider =>
        {
            return collider.GetComponentInParent<Gear>();
        }).ToArray();

        return selectedGear;

        //looking at distance debugger
        //Vector3 maximumXPosition = new Vector3(transform.position.x + radius, transform.position.y);
        //Vector3 maximumYPosition = new Vector3(transform.position.x, transform.position.y + radius);
        //Debug.DrawLine(transform.position, maximumXPosition, Color.red);
        //Debug.DrawLine(transform.position, maximumYPosition, Color.red);

        
    }

    //public abstract void RotateGear();

    public void MoveToValidPosition( LayerMask innerGearLayer)
    {
        Collider2D[] surroundingInnerGear = Physics2D.OverlapCircleAll(this.transform.position, gearRadius, innerGearLayer)
            .Where(collider => {
                return collider != innerGearArea;
            })
            .ToArray();
        if(surroundingInnerGear.Length != 0)
        {
            ColliderDistance2D distance;

            foreach (var innerGear in surroundingInnerGear)
            {
                distance = innerGear.Distance(entireGearArea);
                Vector2 resolveDistance = Math.Abs(distance.distance) * distance.normal;
                
                this.transform.Translate(resolveDistance);
            }
        }


    }

    public abstract void AddSpeedAndRotation(float speed, Vector3 direction);
    
    


}