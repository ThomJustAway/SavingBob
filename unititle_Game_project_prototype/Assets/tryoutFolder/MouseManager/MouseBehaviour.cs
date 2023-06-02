using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBehaviour : MonoBehaviour
{

    //This behaviour uses the finite state machine pattern 

    //this is used for 
    /*
     1. moving gears
     2. deleting gear
     3. checking if the gear is placeable or not
     4. buying gears
     */
    [HideInInspector]public Grid grid;
    [HideInInspector]public Transform selectedObject=null;

    //States
    [HideInInspector] public IMouseStates currentState;
    [HideInInspector] public MouseDragGear mouseDragGear = new MouseDragGear();
    [HideInInspector] public MouseIdle mouseIdle = new MouseIdle();


    private void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        currentState = mouseIdle;
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }
}


