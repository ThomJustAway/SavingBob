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
    public Grid grid;
    public Transform selectedObject=null;

    //States
    public IMouseStates currentState;
    public MouseDragGear mouseDragGear = new MouseDragGear();
    public MouseIdle mouseIdle = new MouseIdle();

    //layer

    public LayerMask moveableGearLayer;
    public LayerMask InnerGearLayer;

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
