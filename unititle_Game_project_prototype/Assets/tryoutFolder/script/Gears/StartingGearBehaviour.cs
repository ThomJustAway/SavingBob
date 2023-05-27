using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingGearBehaviour : Gear 
{
    // Start is called before the first frame update
    public float radiusOfGear;
    [SerializeField] private Collider2D thisEntireGearArea;
    [SerializeField] private Collider2D thisInnerGearArea;
    public LayerMask gearAreaLayer;
    public int teeths;
    

    private void Start()
    {
        
        //add the radius to the abstract gear
        Setter(radiusOfGear, teeths, thisInnerGearArea, thisEntireGearArea);
    }

    public override void AddSpeedAndRotation(float speed, Vector3 direction)
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
