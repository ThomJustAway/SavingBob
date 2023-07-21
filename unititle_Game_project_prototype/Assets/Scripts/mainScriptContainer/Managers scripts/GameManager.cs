using System;
using System.Collections;
using System.Linq;
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
    public static GameManager instance;
    public GameDataScriptableObject currentGameData;
    [SerializeField] private GameObject itemButtonPrefab; //contains the item button script
    [SerializeField] private Transform gearPanel;
    [SerializeField] private GameObject layerButtonPrefab;
    [SerializeField] private Transform LayerPanel; // all this variable are set at the inspectors

    [HideInInspector] public event Action<ItemButton[]> FinishCreatingGearButtonEvent;

    [HideInInspector] public UnityEvent SolvedEvent = new UnityEvent();

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
    }

    private void CreateButtons()
    {
        IMoveable[] dataAboutButtons = currentGameData.moveables;
        ItemButton[] itemButtons = new ItemButton[dataAboutButtons.Length];
        //foreach (var data in dataAboutButtons)
        //{
        //    var button = Instantiate(itemButtonPrefab, gearPanel);
        //    button.GetComponent<ItemButton>().Init(data);
        //}

        for(int i = 0; i < dataAboutButtons.Length; i++)
        {
            var button = Instantiate(itemButtonPrefab, gearPanel);
            var itemComponent = button.GetComponent<ItemButton>();
            itemButtons[i] = itemComponent;
            itemComponent.Init(dataAboutButtons[i]);
        }
        FinishCreatingGearButtonEvent?.Invoke(itemButtons);
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
