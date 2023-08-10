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
    private Camera mainCamera; //this is to move the camera to the z-index position. This will give the illusion that there is a lot of layers in the game
    public Transform background; //this is important as the background prevents players from seeing what is happening below the other layers

    private int currentLayer = 1; //all layers will start at layer 1

    public int CurrentLayer { get { return currentLayer; } }

    [HideInInspector] public UnityEvent onButtonClick = new UnityEvent();
    //this event is for the layerbutton to tell the player which layer they are in.
    //so if layerbutton 1 is click, it will deactivate the previous layerbutton to show that the player is at layer 1. look at layerbutton to understanding what I mean

    private void Start()
    {
        int StartingPoint = -1; //this make sure that players starts at layer 1
        mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(0, 0, StartingPoint);//you can look on the top again if you are confuse. layer 1 camera position is -1;
        background.position = new Vector3(0, 0, StartingPoint + 2);
        instance = this; //seting up the instance
    }

    public int GetGearZIndexBasedOnCurrentLayer()
    {
        // -3 is the difference in layer. For instance if 
        //current layer = 1, the layer will be z-index of 0 which is what the (currentlayer - 1) is for.
        return -3 * (currentLayer - 1); 
    }

    public void ChangeLayer(int layer)
    {
        /*this is the formula to calculate the z-index difference between the two layers
        this difference is used to tell the background, camera to move to a new z-position.
        for example currentlyaer = 1 and layer = 2
        difference would be -3, in layer 1 camera position = -1 and background positiion = 1
        looking back camera position in layer 2 = -4 and background position = -2, both of which have to -3. 
        That is what the difference calculate. This calculation still applies if players
        want to go from a higher layer (like layer 3) to lower layer (layer 1)
        */
        int difference = (currentLayer - layer) * 3;
        currentLayer = layer; //set the current layer to the specified layer
        
        //doing this to get the the new position of the camera.
        int newZPosition = (int)mainCamera.transform.position.z + difference; //change the camera z-position based on layer

        background.position = new Vector3(0, 0,newZPosition+2); //change the background to new z position. The +2 is because the background and camera have a +2 distance apart in z index
        mainCamera.transform.position = new Vector3(0, 0, newZPosition); //change the z index of the position.
    }

}
