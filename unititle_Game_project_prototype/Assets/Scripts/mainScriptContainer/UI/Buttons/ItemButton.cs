using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    //readme
    /*
    Itembutton is a component required to make and keep gears.
    It uses the pooling pattern as I know that There can be
    multiple instances of different objects.

    how it work:
        1. once initialize by the gamemanger, it will produce
        a set number of inactivated IMoveable gameobject. 
        2. the mouseIdle will request a gameobject if they click on the Ui component
        that stores the ItemMouse using the GetItem() function.
        3. if Imoveable items are needed to be kept, they can use the ReturnAllGameObject() 
        which keeps all the gameobject by iterating all the existing instances of said object
        in the existing pool.
    */
    [SerializeField] public IMoveable moveableItem;
    [SerializeField] private int numberOfItem = 10;
    [SerializeField] private GameObject imageContainer;
    [SerializeField] private TextMeshProUGUI moneyTextBox;
    [SerializeField] private TextMeshProUGUI itemButtonNameDisplay;

    [Range(5, 50)]
    [SerializeField] private float scale = 37f;
    private Queue<GameObject> pooledItem = new Queue<GameObject>();
    private HashSet<GameObject> existingPool = new HashSet<GameObject>(); //memory is o(n)
    private GameObject gearParent;
    private void Start()
    {
        //GameObject gearImage;
        SettingUpButton();
        AddingItemToPool();
    }

    public void Init(IMoveable data)
    {
        moveableItem = data;
    }

    private void SettingUpButton()
    {
        itemButtonNameDisplay.text = moveableItem.Name; //setting the name

        Transform item = Instantiate(moveableItem.Getprefab, imageContainer.transform).transform;

        if (item.TryGetComponent<DragableGear>(out DragableGear gearComponent))
        {
            MakeImageFromGear(item, gearComponent);
        }
        else if (item.TryGetComponent<JointBehaviour>(out JointBehaviour jointComponent))
        {
            MakeImageFromJoint(item, jointComponent);
        }

    } //making the button to set up to be like the gear

    private void MakeImageFromGear(Transform imageGearObject, DragableGear gearComponent)
    {
        foreach (Transform child in imageGearObject) //destroy the component inside the gameobject
        {
            Destroy(child.gameObject);
        }
        moneyTextBox.text = gearComponent.Cost.ToString() + " <sprite name=\"Money icon\">";


        Destroy(gearComponent);
        Collider2D colliderComponent = imageGearObject.GetComponent<Collider2D>();
        Destroy(colliderComponent);
        RectTransform rectTransform = imageGearObject.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scale, scale, 0);
    }

    private void MakeImageFromJoint(Transform imageJointObject, JointBehaviour jointComponent)
    {
        moneyTextBox.text = jointComponent.Cost.ToString() + " <sprite name=\"Money icon\">";

        RectTransform rectTransform = imageJointObject.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scale, scale, 0);
        Destroy(jointComponent); // destroy the component that makes the joint work

    }
    private void AddingItemToPool()
    {
        gearParent = GameObject.FindGameObjectWithTag("ParentGear"); // the gameobject that will store all the gears and where all the gear interact
        for (int i = 0; i < numberOfItem; i++)
        {
            GameObject gear = Instantiate(moveableItem.Getprefab, gearParent.transform);
            gear.name = $"{moveableItem.Getprefab.name} {i}";
            gear.SetActive(false);
            pooledItem.Enqueue(gear); // not too sure if this is bad since I using memory to store the gears
            existingPool.Add(gear);
        } //stores the gameobjects in a queue and put it in the parent to keep it more organise
    }

    public bool CanBuyItem()
    {
        return MoneyManager.instance.IfCanSubstractCost(moveableItem.Cost);
    }
    public GameObject GetItem()
    {
        if (pooledItem.Count > 0)
        {
            GameObject item = pooledItem.Dequeue();
            item.SetActive(true);
            return item;
        }
        else
        {
            GameObject gear = Instantiate(moveableItem.Getprefab, gearParent.transform);
            existingPool.Add(gear);
            return gear;
        }
    }

    public void RemoveItem(GameObject SelectedItem)
    {
        SelectedItem.SetActive(false);
        pooledItem.Enqueue(SelectedItem);
        IMoveable item = SelectedItem.GetComponent<IMoveable>();
        MoneyManager.instance.RefundCost(item.Cost);
    }

    public bool IsGameObjectRelated(GameObject selectedGameobject)
    {
        return existingPool.Contains(selectedGameobject);
    } //this look out is o(1) since it is using hashset

    public void ReturnAllGameObject()
    {
        var allExistingGameObject = existingPool.ToArray();
        for(int i = 0; i < allExistingGameObject.Length; i++)
        {
            if (allExistingGameObject[i].activeInHierarchy)
            {
                allExistingGameObject[i].SetActive(false);
                pooledItem.Enqueue(allExistingGameObject[i]);
            }
        }
    }
}
