using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class MouseIdle : IMouseStates
    {
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButton(0))
        {
            GetRayCast(mouseBehaviour);
            if (mouseBehaviour.selectedObject != null)
            {
                Debug.Log(mouseBehaviour.selectedObject);
                return mouseBehaviour.mouseDragGear;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void GetRayCast(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collidedObject = Physics2D.Raycast(positionOfMouse, Vector2.zero,float.PositiveInfinity,mouseBehaviour.moveableGearLayer);
        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
        //need to add code to get different things
        if (collidedObject.collider != null)
        {
            Transform hitGameObject = collidedObject.collider.transform;
            mouseBehaviour.selectedObject = hitGameObject;

        }


    }
}
