using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GearRemainerChecker : MonoBehaviour
{
    //this script check the remaining gear
    private EndGearClass[] inactivatedGears;
    private bool IsSolve;
    private TextMeshProUGUI gearText;
    void Start()
    {
        gearText = GetComponent<TextMeshProUGUI>();

        //will find all the inactivated gears using inactivated gear tag
        inactivatedGears = GameObject
            .FindGameObjectsWithTag("InactivedGear") //er ignore the spelling error here...
            .Select(gear => gear.GetComponent<EndGearClass>())
            .ToArray(); //find all the end gear class
        SetText(0); 
    }

    void Update()
    {
        //the update function is used to keep track on whether the game is completed using the CHeckIfSolve() function
        IsSolve = CheckIfSolve();
        if (IsSolve)
        {//if it is, invoke the solve event to let the other components to run their action when the event is ended
            LevelManager.instance.SolvedEvent?.Invoke();
            LevelManager.instance.SolvedEvent.RemoveAllListeners();
        }
    }

    private bool CheckIfSolve()
    {
        if (inactivatedGears == null)
        {
            return false;
        } //just a edge case if there is no inactivated gears

        bool solve = true;
        int numberOfGearsSolve = 0;

        foreach (var gear in inactivatedGears)
        {
            solve = solve && gear.IsActivated; //go through each end gear and see if they are activated
            if (gear.IsActivated)
            {
                numberOfGearsSolve++; 
            }
            
        }//check if all the gears are activated;
        SetText(numberOfGearsSolve);
        return solve;
    }

    private void SetText(int number)
    {
        // will change the of the number of inactivated gear that is activated.
        gearText.text = $"{number} / {inactivatedGears.Length} <sprite name=\"Gear icon\">";
    }
}
