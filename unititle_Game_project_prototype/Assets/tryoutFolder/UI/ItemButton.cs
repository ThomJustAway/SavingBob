using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField]private GameObject itemPrefab;
    [SerializeField]private int numberOfItem = 10;
    [SerializeField] private string nameOfItem;
    [SerializeField] private GameObject imageContainer;
    [SerializeField] private TextMeshProUGUI moneyTextBox;
    [SerializeField] private TextMeshProUGUI itemButtonNameDisplay;

    [Range(5,50)]
    [SerializeField] private float scale = 37f;
    private Queue<GameObject> pooledGear = new Queue<GameObject>();
    private GameObject gearParent;
    private void Start()
    {
        SettingUpButton();

        //GameObject gearImage;
        AddingItemToPool();
    }

    private void SettingUpButton()
    {
        itemButtonNameDisplay.text = nameOfItem; //setting the name

        Transform item = Instantiate(itemPrefab, imageContainer.transform).transform;

        if(item.TryGetComponent<Gear>(out Gear gearComponent))
        {
            MakeImageFromGear(item,gearComponent);
        }
        else if(item.TryGetComponent<JointBehaviour>(out JointBehaviour jointComponent))
        {
            MakeImageFromJoint(item, jointComponent);
        }

    } //making the button to set up to be like the gear

    private void MakeImageFromGear(Transform imageGearObject, Gear gearComponent)
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
        print("Making image for joint");
    }
    private void AddingItemToPool()
    {
        gearParent = GameObject.FindGameObjectWithTag("ParentGear");
        for (int i = 0; i < numberOfItem; i++)
        {
            GameObject gear = Instantiate(itemPrefab, gearParent.transform);
            gear.SetActive(false);
            pooledGear.Enqueue(gear);
        } //stores the gameobjects in a queue and put it in the parent to keep it more organise
    }

    public bool CanBuyItem()
    {
        IMoveable moveable = itemPrefab.GetComponent<IMoveable>();
        return MoneyManager.instance.IfCanSubstractCost(moveable.Cost);
    }
    public GameObject GetItem()
    {
        if(pooledGear.Count > 0)
        {
            GameObject selectedGear = pooledGear.Dequeue();
            selectedGear.SetActive(true);
            return selectedGear;
        }
        else
        {
            GameObject gear = Instantiate(itemPrefab, gearParent.transform);
            return gear;
        }
    }

    public void RemoveItem(GameObject selectedGear)
    {
        selectedGear.SetActive(false);
        pooledGear.Enqueue(selectedGear);
    }

    public bool IsGearRelated(GameObject selectedGameobject)
    {
        if(selectedGameobject == itemPrefab) return true;
        else return false;
    }
}
