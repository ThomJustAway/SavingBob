using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MouseMoveSelectedObject : IMouseStates
{
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


