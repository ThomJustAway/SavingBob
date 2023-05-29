using System.Collections;
using UnityEngine;


public class StartingGearClass : MonoBehaviour
{
    private Gear gearHost;
    public Gear GearHost { get { return gearHost; } }
    public float speed;
    public GearMovingChecker.RotationDirection RotationDirection;

    private void Start()
    {
        gearHost = GetComponent<Gear>();
    }


}
