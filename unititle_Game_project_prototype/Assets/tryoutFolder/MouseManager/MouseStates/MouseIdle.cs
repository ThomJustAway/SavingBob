using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class MouseIdle : IMouseStates
    {
    public IMouseStates DoState(MouseBehaviour mouseBehaviour)
    {
        if (Input.GetMouseButton(0))
        {
            RayCastMouse(mouseBehaviour);
            if (mouseBehaviour.selectedObject != null)
            {
                //Debug.Log(mouseBehaviour.selectedObject);
                return mouseBehaviour.mouseDragGear;
            }
            else return mouseBehaviour.mouseIdle;
        }
        else return mouseBehaviour.mouseIdle;
    }

    private void RayCastMouse(MouseBehaviour mouseBehaviour)
    {
        Vector2 positionOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var collidedObject = Physics2D.Raycast(positionOfMouse, Vector2.zero,float.PositiveInfinity,LayerData.MoveableGearLayer);
        //getting the objects that are colliding with the raycast. this is from the moveable gear layer.
        //need to add code to get different things
        if (collidedObject.collider != null)
        {
            GetType(collidedObject.collider);
            Transform hitGameObject = collidedObject.collider.transform;
            mouseBehaviour.selectedObject = hitGameObject;
        }
    }

    private System.Type[] existingSystemType = { typeof(Gear), typeof(JointBehaviour) }; 

    private void GetType(Collider2D collider)
    {
        foreach(System.Type type in existingSystemType)
        {
            if(collider.TryGetComponent(type, out Component compenent))
            {
                Debug.Log(compenent.gameObject.name);
                return;
            }
        }
        Debug.Log("no avaliable component detected");
    }
}
