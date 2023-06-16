using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JointBehaviour : MonoBehaviour ,IMoveable
{

    [SerializeField] private Collider2D lowerJoint;
    [SerializeField] private Collider2D upperJoint;
    [SerializeField] private int cost;
    private Gear connectedGearLower;
    private Gear connectedGearUpper;
    private float radius = 0.5f;
    public bool isConnected;


    public int Cost { get { return cost; } }

    void Update()
    {
        CheckBothJoint();
    }

    private void CheckBothJoint()
    {
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);   
        CheckIsConnected();
    }
    
    private void CheckIsConnected() // if there is no gear connected, set is connected to false
    {
        if(connectedGearLower == null && connectedGearUpper == null)
        {
            isConnected = false;
        }
        else
        {
            isConnected = true;
        }
    }

    //joint needs to know the rotating gear and the non rotating gear
    //needs to know if it is rotated or not

    private void CheckJoint(Collider2D joint , bool isLowerJoint )
    {
        float minDept = joint.transform.position.z - 0.5f;
        float maxDept = joint.transform.position.z + 0.5f;

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

    public Gear GetRespondingGear(Gear calledGear) // give back a gear that is connected with the joint.
    {
        if (!isConnected) return null;
        else if (calledGear == connectedGearLower) return connectedGearUpper;
        else return connectedGearUpper;
    }

    public void CheckValidPosition()
    {
        if (isConnected)
        {
            if(connectedGearLower == null)
            {
                transform.position = connectedGearUpper.transform.position;
            }
            else if(connectedGearUpper == null)
            {
                transform.position = connectedGearLower.transform.position;
            }
            else
            {
                print("what should I do here?");
            }
        }
        else
        {
            print("no valid position");
        }
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }


}