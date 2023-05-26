using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMovingChecker : MonoBehaviour
{

    //todo
    //1.maybe implement the gear class/abstract class to add the gears
    //2. Maybe add this at the starting gear

    public List<GearBehaviour> chainOfGears = new List<GearBehaviour>();
    public static GearMovingChecker Instance;
    private float speed = 30f;

    private void Awake()
    {
        Instance = this;
    }
    //private void UpdateGearSpeed()
    //{
    //    chainOfGears.ForEach( gear =>
    //    {
    //        gear.AddSpeed(speed);
    //    });
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("entering");
        GearBehaviour connectedGear = collision.gameObject.GetComponent<GearBehaviour>();
        chainOfGears.Add(connectedGear);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        print("staying");
        //UpdateGearSpeed();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        print("leaving");
        chainOfGears.Remove(collision.gameObject.GetComponent<GearBehaviour>());

    }
}
