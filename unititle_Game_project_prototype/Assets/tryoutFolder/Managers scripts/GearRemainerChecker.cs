using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GearRemainerChecker : MonoBehaviour
{
    private EndGearClass[] inactivatedGears;
    private bool isSolve;
    private TextMeshProUGUI gearText;
    [SerializeField] private UnityEvent SolvedEvent;
    // Start is called before the first frame update
    void Start()
    {
        gearText = GetComponent<TextMeshProUGUI>();
        inactivatedGears = GameObject
            .FindGameObjectsWithTag("InactivedGear") //er ignore the spelling error here...
            .Select(gear => gear.GetComponent<EndGearClass>())
            .ToArray();
        SetText(0);
    }

    // Update is called once per frame
    void Update()
    {
        isSolve = CheckIfSolve();
        if (isSolve)
        {
            SolvedEvent?.Invoke();
            //do something here!
        }
    }

    private bool CheckIfSolve()
    {
        if (inactivatedGears == null)
        {
            Debug.LogError("You have not have any inactiavated gears in the scene!");
            return false;
        }

        bool solve = true;
        int numberOfGearsSolve = 0;

        foreach (var gear in inactivatedGears)
        {
            solve = solve && gear.IsActivated;
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
        gearText.text = $"{number} / {inactivatedGears.Length} <sprite name=\"Gear icon\">";
    }
}
