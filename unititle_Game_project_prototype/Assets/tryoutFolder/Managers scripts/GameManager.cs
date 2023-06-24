using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] private GameObject layerButtonPrefab;
    [SerializeField] private Transform LayerPanel;

    [HideInInspector] public UnityEvent FinishCreatingGearButtonEvent = new UnityEvent();
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
        CreateLayerButtons();
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
        IMoveable[] dataAboutButtons = currentGameData.moveables;
        foreach (var data in dataAboutButtons)
        {
            var button = Instantiate(itemButtonPrefab, gearPanel);
            button.GetComponent<ItemButton>().Init(data);
        }
        FinishCreatingGearButtonEvent?.Invoke();
    }

    private void CreateLayerButtons()
    {
        for(int i = 1; i <= currentGameData.NumberOfLayers; i++)
        {
            var layerButton = Instantiate(layerButtonPrefab, LayerPanel);
            LayerButton layerComponent = layerButton.GetComponent<LayerButton>();
            layerComponent.Init(i);
        }
    }
}
