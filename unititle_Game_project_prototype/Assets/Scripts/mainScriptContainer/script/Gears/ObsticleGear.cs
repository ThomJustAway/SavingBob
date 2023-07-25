using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class ObsticleGear : Gear
{
    [SerializeField] private ObsticleBehaviour obsticle;
    private bool isActivated;

    protected override void Start()
    {
        isActivated = false;
        base.Start();
        GetComponent<SpriteRenderer>().color = ColorData.Instance.ObsticleGearColor;
    }

    protected override void Update()
    {
        base.Update();
        ToggleBool();
    }

    private void ToggleBool()
    {
        bool hasSpeed = this.speed > 0;
        if (hasSpeed != isActivated)
        {
            if (hasSpeed == true)
            {
                obsticle.RemoveObsticle();
            }
            else
            { 
                obsticle.MoveObsticleBack();
            }
            //change the bool no matter what happen
            isActivated = !isActivated;
        }
    }

}
