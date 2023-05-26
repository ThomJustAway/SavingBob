using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IRotate 
{
    void RotateGear(float speed,Vector3 direction);
}

public class Direction
{
    public static Vector3 Clockwise = Vector3.forward;
    public static Vector3 AntiClockwise = Vector3.back;
}


public abstract class Gear: MonoBehaviour
{
    private float radius;
    public float Radius { get { return radius; } set { radius = value; } }

    public Gear[] GetGearsAroundRadius(Vector3 currentPosition,LayerMask layer,Collider2D thisEntireGearArea)
    {
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(currentPosition, radius, layer)
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

}
