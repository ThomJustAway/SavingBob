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
    private Grid grid;
    private Transform selectedObject=null;

    private void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            
            if(selectedObject != null)
            {
                DragGear();
            }
            else
            {
                GetRayCast();
            }
        }
        else
        {
            selectedObject = null;
        }
    }

    private void DragGear()
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition=grid.WorldToCell(positionOfMouse);
        selectedObject.position= grid.GetCellCenterLocal(cellPosition);
        //make the gear follow the mouse while snaping to the grid

    }


    private void GetRayCast()
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collidedObjects=Physics2D.Raycast(positionOfMouse, Vector2.zero);
        //getting the objects that are colliding with the raycast.
        if(collidedObjects.collider != null)
        {
            //refactor this
            print("collider found" + collidedObjects.collider.gameObject.name);

            Transform hitGameObject = collidedObjects.collider.transform;
            selectedObject = hitGameObject.parent;
            //getting the parent gameobject
        }


    }
}
