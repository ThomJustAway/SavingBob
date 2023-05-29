using System.Collections;
using UnityEngine;

namespace Assets.tryoutFolder.script.Gears
{
    public class EndGearClass : MonoBehaviour
    {
        private Gear gearHost;
        public Gear GearHost { get { return gearHost; } }


        private void Start()
        {
            gearHost = GetComponent<Gear>();
        }

        private void Update()
        {
            //Todo
            //get speed from gear
           
        }

    }
}