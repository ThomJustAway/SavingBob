using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LayerManager : MonoBehaviour
{
    //README
    /*
        LayerManager: keeps track of the layer the current player is in
        and move the camera and the background accordingly. 
        
        The layer is differentiated based on the Z-index

                camera z position in every layer
                layer 1 :-1
                layer 2 : -4
                layer 3 : -7

                Gear z position in every layer
                layer 1:0
                layer 2:-3
                layer 3:-6

                background z position in every layer
                layer 1: 1
                layer 2: -2
                layer 3: -5

        The layerManager is in charge of any responsibility regarding the layer which include

            1. making sure the camera and background are at the right layer
            2. Changing the z index of camera and background
            3. Giving out the current position of gears z index when needed.
     */

    public static LayerManager instance; //singleton because I want to have an object that stores the current layer of the game.
    private Camera mainCamera;
    public Transform background;

    private int currentLayer = 1;

    public int CurrentLayer { get { return currentLayer; } }
    [HideInInspector] public UnityEvent onButtonClick = new UnityEvent();

    private void Start()
    {
        int StartingPoint = -1;
        mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(0, 0, StartingPoint);
        background.position = new Vector3(0, 0, StartingPoint + 2);
        instance = this;
    }

    public int GetGearZIndexBasedOnCurrentLayer()
    {
        // -3 is the difference in layer. For instance if 
        //current layer = 1, the layer will be z-index of 0 which is what the (currentlayer - 1) is for.
        return -3 * (currentLayer - 1); 
    }

    public void ChangeLayer(int layer)
    {
        int difference = (currentLayer - layer) * 3;
        currentLayer = layer;
        int newZPosition = (int)mainCamera.transform.position.z + difference;
        //just know the two line belows just change the z position according. look at readme to understand the number
        background.position = new Vector3(0, 0,newZPosition+2);
        mainCamera.transform.position = new Vector3(0, 0, newZPosition);
    }

}
