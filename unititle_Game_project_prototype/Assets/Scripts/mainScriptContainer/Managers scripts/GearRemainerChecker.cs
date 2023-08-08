using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GearRemainerChecker : MonoBehaviour
{
    /// <summary>
    /// Gear remainer checker script check if all the inactivated gears are 
    /// activated. It will search through all the gears using Tag to find all
    /// the inactiavted gear. Afterwhich, it will do a constant check to see if they are activated.
    /// If they are all activated, then it will send an event saying that the puzzle is solve.
    /// </summary>

    //this script check for the remaining gears
    private EndGearClass[] inactivatedGears; //use to store reference to the inactivated gears
    private bool IsSolve; //a bool to check if the puzzle is solve
    private TextMeshProUGUI gearText; //will change the text to indicated how many inactivated gears are left

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
        {//if it is, invoke the solve event to let the other components to run their action when the puzzle is ended
            LevelManager.instance.SolvedEvent?.Invoke();
            LevelManager.instance.SolvedEvent.RemoveAllListeners(); //just to prevent any memory leakage
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
