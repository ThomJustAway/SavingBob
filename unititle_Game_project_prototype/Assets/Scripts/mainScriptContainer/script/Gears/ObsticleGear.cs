using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Gear))]
public class ObsticleGear : Gear
{

    //readme
    /*
        The gear that blocks gear placement
        simple script
     */
    [SerializeField] private ObsticleBehaviour obsticle; //will have reference to the obsticle
    private bool isActivated; //tells the script if the obsticle is remove or not (true if the obsticle is remove and false if it not)

    protected override void Start()
    {
        isActivated = false;
        base.Start(); //do the same start function as a normal gear
        GetComponent<SpriteRenderer>().color = ColorData.Instance.ObsticleGearColor; //change the color for players to identify the obsticle color
    }

    protected override void Update()
    {
        base.Update(); //do the samething as a gear.
        ToggleBool();//this toggle bool will toggle the bool of the isActivated.
    }

    private void ToggleBool()
    {
        bool hasSpeed = this.speed > 0;// check if it is rotated
        if (hasSpeed != isActivated) //we want to check if the hasspeed is different from isactivated
            //as it tells the obsticlegear that is a significant change and need to perform an action
        {
            if (hasSpeed == true)// this mean the isactiaved =false. which means the obsticle needs to be remove
            {
                obsticle.RemoveObsticle();
            }
            else
            { //this means that the isactiavated is true. This would mean that the obsticle need to return back to normal position
                obsticle.MoveObsticleBack();
            }
            //change the bool no matter what happen
            isActivated = !isActivated;
        }
    }

}
