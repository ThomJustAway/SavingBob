using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JointBehaviour : MonoBehaviour ,IMoveable
{

    [SerializeField] private Collider2D lowerJoint;
    [SerializeField] private Collider2D upperJoint;
    [SerializeField] private int cost;
    [SerializeField] private string name;
    private Gear connectedGearLower;
    private Gear connectedGearUpper;
    private float radius = 0.5f;

    public int Cost { get { return cost; } }

    public GameObject Getprefab => this.gameObject;

    public string Name => name;


    void Update()
    {
        CheckBothJoint();

        if(CheckIsConnected()) // to move the gears
        {
            if (connectedGearLower.Speed > 0 && connectedGearLower.DriverGear != null)
            {
                connectedGearUpper.AddSpeedAndRotation(connectedGearLower.Speed, connectedGearLower.Direction, connectedGearLower);
            }
            else if(connectedGearUpper.Speed > 0 && connectedGearLower.DriverGear != null) 
            { 
                connectedGearUpper.AddSpeedAndRotation(connectedGearUpper.Speed,connectedGearUpper.Direction, connectedGearUpper);
            }
        }
    }



    private void CheckBothJoint()
    {
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);   //look at both joint
    }
    
    private bool CheckIsConnected() // if there is no gear connected, set is connected to false
    {
        return connectedGearLower != null && connectedGearUpper != null;
    }

    //joint needs to know the rotating gear and the non rotating gear
    //needs to know if it is rotated or not

    private void CheckJoint(Collider2D joint , bool isLowerJoint )
    {
        float minDept = joint.transform.position.z - 0.3f;
        float maxDept = joint.transform.position.z + 0.3f;

        Collider2D gearSurroundingJoint = Physics2D.OverlapCircle
            (joint.transform.position,
            radius,
            LayerData.InnerGearLayer,
            minDept
            , maxDept
            );
        if (gearSurroundingJoint != null)
        {
            if (isLowerJoint)
            {
                connectedGearLower = gearSurroundingJoint.GetComponentInParent<Gear>();
            }
            else
            {
                connectedGearUpper = gearSurroundingJoint.GetComponentInParent<Gear>();
            }

        }
        else
        {
            if (isLowerJoint)
            {
                connectedGearLower = null;
            }
            else
            {
                connectedGearUpper= null;
            }
        }
        //change this if else statement
    }


    public void CheckValidPosition() //fix this later
    {
        CheckBothJoint();
        if(connectedGearLower != null)
        {
            transform.position = connectedGearLower.transform.position;
        }
        else if (connectedGearUpper != null)
        {
            Vector3 newPosition = connectedGearUpper.transform.position;
            newPosition.z = newPosition.z + 3; // formula
            transform.position = newPosition;
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }


}