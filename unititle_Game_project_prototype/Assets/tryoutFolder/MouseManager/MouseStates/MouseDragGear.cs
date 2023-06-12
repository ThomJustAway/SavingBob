using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MouseDragGear : IMouseStates
{
    private float offsetFromCamera = -0.7f; // to make sure that the gear stay above the UI component

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
        mouseBehaviour.selectedObject.Translate(0,0, -offsetFromCamera); //make sure that the gear is place within the scene

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
        Vector3 newPosition = mouseBehaviour.grid.GetCellCenterLocal(cellPosition);

        // make sure that the gear is place on top of the UI component
        newPosition.z = LayerManager.Current.GetGearZIndexBasedOnCurrentLayer() + offsetFromCamera; 
        mouseBehaviour.selectedObject.position = newPosition;
        //make the gear follow the mouse while snaping to the grid
    }
}


