using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Read me
    /*
    Game manager oversees the data and events that happen in the game
    it is a singleton as some of the other scripts will require the 
    gamedatascriptableObject data.

    The game manager is usually inactive and is used to start the game.

    It will take data from the gamedatascriptableObject to know how many
    layers and prefabs is needed for the game. It is also used to tell the
    other gameobjects if a game is completed
    */
    public static LevelManager instance;
    public GameDataScriptableObject currentGameData;
    [SerializeField] private GameObject itemButtonPrefab; //contains the item button script
    [SerializeField] private Transform gearPanel;
    [SerializeField] private GameObject layerButtonPrefab;
    [SerializeField] private Transform LayerPanel; // all this variable are set at the inspectors
    

    [HideInInspector] public UnityEvent SolvedEvent = new UnityEvent();

    public ItemButton[] itemButtons { get; private set; } 

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
        itemButtons = new ItemButton[dataAboutButtons.Length]; 
        for(int i = 0; i < dataAboutButtons.Length; i++)
        {
            var button = Instantiate(itemButtonPrefab, gearPanel);
            var itemComponent = button.GetComponent<ItemButton>();
            itemButtons[i] = itemComponent;
            itemComponent.Init(dataAboutButtons[i]); 
        }
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
