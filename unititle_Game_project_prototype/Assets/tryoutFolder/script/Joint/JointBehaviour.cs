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


    void Update()
    {
        
    }



}


