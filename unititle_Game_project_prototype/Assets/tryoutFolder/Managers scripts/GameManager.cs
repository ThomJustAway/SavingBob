using System;
using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //todo
    /*
    1. Check If all inactivated gears are activated
    2. Send an event that the puzzle is solve.
    */
    private EndGearClass[] inactiavatedGears;
    private bool isSolve;
    public static GameManager instance;
    public GameDataScriptableObject currentGameData;
    [SerializeField] private GameObject itemButtonPrefab; //contains the item button script
    [SerializeField] private Transform gearPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        CreateButtons();
        inactiavatedGears = GameObject.FindObjectsOfType(typeof(EndGearClass)) as EndGearClass[];
    }

    void Update()
    {
        CheckIfSolve();
    }

    private void CheckIfSolve()
    {
        if(inactiavatedGears == null)
        {
            Debug.LogError("You have not have any inactiavated gears in the scene!");
            return;
        }

        bool solve = true;

        foreach (var gear in inactiavatedGears)
        {
            solve = solve && gear.IsActivated;
        }//check if all the gears are activated;

        isSolve = solve;

    }

    private void CreateButtons()
    {
        ItemButtonData[] dataAboutButtons = currentGameData.itemButtons;
        foreach(var data in dataAboutButtons)
        {
            var button=Instantiate(itemButtonPrefab,gearPanel);
            button.GetComponent<ItemButton>().Init(data);
        }
    }

}
