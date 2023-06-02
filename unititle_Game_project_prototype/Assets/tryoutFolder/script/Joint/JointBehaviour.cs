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

    void Update()
    {
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);
        print("lowerJoint is " + connectedGearLower.transform.name);
        print("upperJoint is " + connectedGearUpper.transform.name);
    }


    
    private void CheckJoint(Collider2D joint , bool isLower )
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
            if (isLower)
            {
                connectedGearLower = gearSurroundingJoint.GetComponentInParent<Gear>();
            }
            else
            {
                connectedGearUpper = gearSurroundingJoint.GetComponentInParent<Gear>();
            }
        }

    }
}


