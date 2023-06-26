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
    [HideInInspector] public Grid grid;
    [HideInInspector] public IMoveable selectedObject = null;
    [HideInInspector] public bool deleteActivated = false;
    [HideInInspector] public ItemButton[] itemButtons;

    //States
    [HideInInspector] public IMouseStates currentState;
    [HideInInspector] public MouseMoveSelectedObject mouseMoveSelectedObject = new MouseMoveSelectedObject();
    [HideInInspector] public MouseIdle mouseIdle = new MouseIdle();
    [HideInInspector] public DeleteItems deleteItems = new DeleteItems();


    public IMoveable GetImoveableComponent(Collider2D collider)
    {
        Transform hitGameObject = collider.transform;
        //mouseBehaviour.selectedObject = hitGameObject.GetComponent<IMoveable>();
        if (hitGameObject.TryGetComponent<IMoveable>(out IMoveable selectedObject))
        {
           return selectedObject;
        }
        else
        {
            //the joint
           return hitGameObject.GetComponentInParent<IMoveable>();
        }
    }


    private void Start()
    {
        grid = Grid.FindObjectOfType<Grid>();
        currentState = mouseIdle;
        GameManager.instance.FinishCreatingGearButtonEvent.AddListener(SetButtons);
    }

    private void SetButtons()
    {
        itemButtons = ItemButton.FindObjectsOfType<ItemButton>();
        GameManager.instance.FinishCreatingGearButtonEvent.RemoveAllListeners();
    }

    public void ToggleDeletedActiavted()
    {
        deleteActivated = !deleteActivated;
    }

    private void Update()
    {
        currentState = currentState.DoState(this);
    }

    //private IEnumerable ChangeActiavated()
    //{
    //    deleteActivated = !deleteActivated;
        
    //}
}


