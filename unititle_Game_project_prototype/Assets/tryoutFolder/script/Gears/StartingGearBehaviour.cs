using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingGearBehaviour : Gear, IRotate 
{
    // Start is called before the first frame update
    private float speed;
    public float radiusOfGear;
    [SerializeField] private Collider2D thisEntireGearArea;
    [SerializeField] private Collider2D thisInnerGearArea;
    public LayerMask gearAreaLayer;
    

    private void Start()
    {
        //add the radius to the abstract gear
        Radius = radiusOfGear;
    }
    void Update()
    {
        
        Gear[] gears=GetGearsAroundRadius(transform.position, gearAreaLayer,thisEntireGearArea);

        foreach(Gear gear in gears)
        {
            if (gear != null)
            {
                print(gear.name);
            }
        }
    }


    public void RotateGear(float speed, Vector3 direction)
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
