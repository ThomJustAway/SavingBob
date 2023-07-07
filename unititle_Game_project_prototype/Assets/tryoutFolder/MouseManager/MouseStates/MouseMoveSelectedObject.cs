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

    //private Vector3 GetVector3FromMousePosition(MouseBehaviour mouseBehaviour)
    //{
    //    Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3Int cellPosition = mouseBehaviour.grid.WorldToCell(positionOfMouse);
    //    make the gear follow the mouse while snaping to the grid
    //    Vector3 newPosition = mouseBehaviour.grid.GetCellCenterLocal(cellPosition);
    //    var newPosition = new Vector3(positionOfMouse.x, positionOfMouse.y, LayerManager.instance.GetGearZIndexBasedOnCurrentLayer());
    //    make sure that the gear is place on top of the UI component
    //    newPosition.z = LayerManager.instance.GetGearZIndexBasedOnCurrentLayer();
    //    return newPosition;
    //}
}


