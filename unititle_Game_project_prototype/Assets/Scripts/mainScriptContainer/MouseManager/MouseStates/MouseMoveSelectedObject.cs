using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MouseMoveSelectedObject : IMouseStates
{
    //readme
    /*
    How it works:
    1. In order to make it look like players are dragging and dropping the objects,
    I had to change the canvas to screen space. This means that there can be 
    objects that can above the UI.
    2. this is where the offset from camera comes in. It works as a way to show the objects above UI
    3. Selected object from the mouseIdle state will be used to move the object
        - you can take a look at the moveselectedOBject function where it uses the 
        Imoveable function from the selectedobject (which is an imoveable).
        Do note that different classes has their own implementation of moving and
        checking for valid position.
    */
    private float offsetFromCamera = -0.7f; // to make sure that the gear stay above the UI component
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButton(0))
        {
            //check if the mouse is being hold. keep updating the select object during this period
            MoveSelectedObject(mouseBehaviour);
            return mouseBehaviour.mouseMoveSelectedObject;
        }
        else
        {
            //if the mouse button has stop moving, then it means that player has stop moving the selected object
            CheckIfValidPosition(mouseBehaviour);
            mouseBehaviour.selectedObject = null; //set this to null for other selectedobject to be selected
            return mouseBehaviour.mouseIdle; //change back to mouse Idle for next call
        }   
    }

    private void CheckIfValidPosition(MouseBehaviour mouseBehaviour)
    {
        
        Vector3 Position = mouseBehaviour.GetVector3FromMousePosition();
        mouseBehaviour.selectedObject.Move(Position); //this time make the imoveable to go back to current position 
        //without any offset (so that it does not look weird if the gear menu is were to overlap it
        mouseBehaviour.selectedObject.CheckValidPosition(); //ask the selectedobject to check if it is a valid position
    } 

    private void MoveSelectedObject(MouseBehaviour mouseBehaviour)
    {
        Vector3 newPosition = mouseBehaviour.GetVector3FromMousePosition();
        //get new position based on mouse behaviour
        newPosition.z += offsetFromCamera; //this offset is to show selected object to be above the gear menu
        mouseBehaviour.selectedObject.Move(newPosition); //ask the selectobject to move 
    }

}


