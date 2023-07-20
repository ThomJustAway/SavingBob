using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class SynchroniseGear : MonoBehaviour
{
    [SerializeField] private Gear ConnectedGear;
    private Gear hostGear;

    private void Start()
    {
        hostGear = GetComponent<Gear>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer gearConnectedSpriteRenderer = ConnectedGear.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
        gearConnectedSpriteRenderer.color = Color.blue;
    }



}
