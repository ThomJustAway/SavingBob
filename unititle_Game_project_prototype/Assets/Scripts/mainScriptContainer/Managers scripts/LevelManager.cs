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
    Level manager oversees the data and events that happen in the game
    it is a singleton as some of the other scripts will require the 
    gamedatascriptableObject data.

    The level manager is usually inactive and is used to start the game.

    It will take data from the gamedatascriptableObject to know how many
    layers and prefabs is needed for the game. It is also used to tell the
    other gameobjects if a game is completed
    */
    public static LevelManager instance;
    public GameDataScriptableObject currentGameData; //will get the scriptable object through the unity inspector
    [SerializeField] private GameObject itemButtonPrefab; //contains the item button prefab and script
    [SerializeField] private Transform gearPanel; //this variable is to let the level manager know where to place the itembutton
    [SerializeField] private GameObject layerButtonPrefab;// this contains the layerbutton prefab for the levelmanager to create
    [SerializeField] private Transform LayerPanel; //this variable is to let level manager know where to place the layerbutton


    [HideInInspector] public UnityEvent SolvedEvent = new UnityEvent(); //this event is called once the player manager to solve the event
    //this can be seen in the gear remainer checker

    public ItemButton[] itemButtons { get; private set; } //keeps a reference of all the item button in the game
    //item buttons are buttons that contains the prefabs to dragable item. Dragable items are gameobject that implements the Imoveable interface

    private void Awake()
    {//setting up instance
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
        //set up the buttons
        CreateButtons();
        CreateLayerButtons();
    }

    private void CreateButtons()
    {
        //this function will create all the itembuttons in the game
        IMoveable[] dataAboutButtons = currentGameData.moveables;
        itemButtons = new ItemButton[dataAboutButtons.Length]; //set a fix memory slot to store the itembuttons
        for(int i = 0; i < dataAboutButtons.Length; i++)
        {
            // will create each button that will be shown in the gear menu
            var button = Instantiate(itemButtonPrefab, gearPanel);
            var itemComponent = button.GetComponent<ItemButton>();
            itemButtons[i] = itemComponent; //adding the itemcomponent into memory so that other scripts can use it
            // putting Imoveable interface so that the item button to make the buttons
            itemComponent.Init(dataAboutButtons[i]); 
        }
    }
    private void CreateLayerButtons()
    {
        //create layer buttons by iterating now many layers there are and placing it in the layer menu
        for(int i = 1; i <= currentGameData.NumberOfLayers; i++)
        {
            var layerButton = Instantiate(layerButtonPrefab, LayerPanel);//create the layer buttons and store it in the layerpanel
            LayerButton layerComponent = layerButton.GetComponent<LayerButton>(); 
            layerComponent.Init(i); //get the layerbutton and initialize it by assigning what layer number it is assign to
        }
    }
}
