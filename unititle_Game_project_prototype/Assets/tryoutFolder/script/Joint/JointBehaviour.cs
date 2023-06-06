using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JointBehaviour : MonoBehaviour
{

    [SerializeField] private Collider2D lowerJoint;
    [SerializeField] private Collider2D upperJoint;
    private Gear connectedGearLower;
    private Gear connectedGearUpper;
    private float radius = 0.5f;
    public bool isConnected;

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

}