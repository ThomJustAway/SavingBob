using Assets.tryoutFolder.script;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class StartingGearClass : MonoBehaviour 
{
    // readme
    /*
     Starting gear class is a class that will make a gear the
    starting gear if it is place at the component together with
    Gear class. it will basically add speed and rotation constantly
    to show that it is a starting class
     */

    private RotatableElement gearHost;
    [SerializeField] private float speed;
    [SerializeField] private RotationDirectionClass.RotationDirection rotationDirection;

    private void Start()
    {
        //setting up the starting gear
        gearHost = GetComponent<RotatableElement>();
        gearHost.GetComponent<SpriteRenderer>().color = ColorData.Instance.StartingGearColor;
    }

    private void Update()
    { //in the update call, add speed and rotation
        Vector3 direction = RotationDirectionClass.GetVector3FromDirection(rotationDirection);
        gearHost.AddSpeedAndRotation(speed, direction);
    }
}