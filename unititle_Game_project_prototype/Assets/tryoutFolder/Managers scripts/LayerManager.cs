using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private Camera mainCamera;
    public Transform background;

    private int currentLayer = 1;


    private void Start()
    {
        int StartingPoint = -1;
        mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(0, 0, StartingPoint);
        background.position = new Vector3(0,0,StartingPoint+2);
    }

    //layer 1 :-1
    //layer 2 : -4
    //layer 3 : -7

    public void ChangeLayer(int layer)
    {
        int difference = (currentLayer - layer) * 3;
        currentLayer = layer;
        int newZPosition = (int) mainCamera.transform.position.z + difference ;
        background.position = new Vector3(0, 0,newZPosition+2);
        mainCamera.transform.position = new Vector3(0, 0, newZPosition);
    }

}
