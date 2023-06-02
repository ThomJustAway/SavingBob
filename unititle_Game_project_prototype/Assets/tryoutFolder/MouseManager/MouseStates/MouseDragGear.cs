using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MouseDragGear : IMouseStates
{

    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButton(0))
        {
            DragGear(mouseBehaviour);
            return mouseBehaviour.mouseDragGear;
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
        Gear gearComponent = mouseBehaviour.selectedObject.GetComponent<Gear>();
        Collider2D[] surroundInnerGear = gearComponent.GetColliderAroundRadiusBasedOnLayer(LayerData.InnerGearLayer);
        if(surroundInnerGear != null)
        {
            ColliderDistance2D distance;

            foreach (var innerGear in surroundInnerGear)
            {
                distance = innerGear.Distance(gearComponent.EntireGearArea);
                Vector2 resolveDistance = Math.Abs(distance.distance) * distance.normal;

                mouseBehaviour.selectedObject.Translate(resolveDistance);
            }
        }


    } 



    private void DragGear(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = mouseBehaviour.grid.WorldToCell(positionOfMouse);
        cellPosition.z = (int)mouseBehaviour.transform.position.z;
        mouseBehaviour.selectedObject.position = mouseBehaviour.grid.GetCellCenterLocal(cellPosition);
        //make the gear follow the mouse while snaping to the grid
    }
}


