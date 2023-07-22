using Assets.tryoutFolder.script;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class StartingGearClass : MonoBehaviour 
{
    private RotatableElement gearHost;
    [SerializeField] private float speed;
    [SerializeField] private RotationDirectionClass.RotationDirection rotationDirection;

    private void Start()
    {
        gearHost = GetComponent<RotatableElement>();
        gearHost.GetComponent<SpriteRenderer>().color = ColorData.Instance.StartingGearColor;
    }

    private void Update()
    {
        Vector3 direction = RotationDirectionClass.GetVector3FromDirection(rotationDirection);
        gearHost.AddSpeedAndRotation(speed, direction);
    }
}