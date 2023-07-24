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
            MoveSelectedObject(mouseBehaviour);
            return mouseBehaviour.mouseMoveSelectedObject;
        }
        else
        {
            //put code here to prevent gears from overlapping one another
            CheckIfValidPosition(mouseBehaviour);
            mouseBehaviour.selectedObject = null;
            return mouseBehaviour.mouseIdle;
        }   
    }

    private void CheckIfValidPosition(MouseBehaviour mouseBehaviour)
    {
        Vector3 Position = mouseBehaviour.GetVector3FromMousePosition();
        mouseBehaviour.selectedObject.Move(Position);
        mouseBehaviour.selectedObject.CheckValidPosition();
    } 

    private void MoveSelectedObject(MouseBehaviour mouseBehaviour)
    {
        Vector3 newPosition = mouseBehaviour.GetVector3FromMousePosition();
        newPosition.z += offsetFromCamera;
        mouseBehaviour.selectedObject.Move(newPosition);
    }

}


