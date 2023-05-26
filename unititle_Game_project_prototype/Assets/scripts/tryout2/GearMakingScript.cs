using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMakingScript : MonoBehaviour
{

    public GameObject gearbase;
    public GameObject tooth;
    private float gearDistance = 0.2f; //   meter/radian

    [ContextMenu ("helo")]
    private void MakeGear(Vector3 position,float diameter )
    {
        GameObject Holder = new GameObject();
        //create new empty gameobject
        Instantiate(gearbase,Vector3.zero,Quaternion.identity, Holder.transform);
        //create the circle of the gear

        Holder.transform.position = position;
        //put the gear at the position
        
        int numberOfGear = (int) Math.Floor(diameter / gearDistance);
        Holder.name = $"{numberOfGear} gear";
        //finding how many degree apart to place the tooth of the gear
        float degree = 360/numberOfGear; 
        for (int i =0; i<numberOfGear; i++)
        {
            Transform gearTeeth =Instantiate(tooth,Holder.transform).transform;
            gearTeeth.position = new Vector3(position.x,position.y+diameter/2);
            gearTeeth.RotateAround(position,Vector3.forward,degree*i);
        }
    }


    private void MakeGear(Vector3 position, int numberOfTooth)
    {
        GameObject Holder = new GameObject();
        //create new empty gameobject
        Instantiate(gearbase, Vector3.zero, Quaternion.identity, Holder.transform);
        //create the circle of the gear

        Holder.transform.position = position;
        //put the gear at the position
        int diameter = (int) Math.Floor(numberOfTooth*gearDistance);
        Holder.name = $"{numberOfTooth} gear";
        //finding how many degree apart to place the tooth of the gear
        float degree = 360 / numberOfTooth;
        for (int i = 0; i < numberOfTooth; i++)
        {
            Transform gearTeeth = Instantiate(tooth, Holder.transform).transform;
            gearTeeth.position = new Vector3(position.x, position.y + diameter / 2);
            gearTeeth.RotateAround(position, Vector3.forward, degree * i);
        }
    }
    private void Start()
    {
        MakeGear(Vector3.one,8);

        MakeGear(Vector3.zero,12);
    }


}
