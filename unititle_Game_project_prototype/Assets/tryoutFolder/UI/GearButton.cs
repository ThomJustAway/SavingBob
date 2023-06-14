using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class GearButton : MonoBehaviour
{
    [SerializeField]private GameObject placeableGear;
    [SerializeField]private int numberOfGear = 10;
    [SerializeField] private string nameOfButton;
    [SerializeField] private GameObject imageContainer;

    [Range(5,50)] // figure out how to make the gameobject change with the image?
    [SerializeField] private float scale = 37f;
    private Queue<GameObject> pooledGear = new Queue<GameObject>();
    private GameObject gearParent;
    private void Start()
    {
        SettingUpButton();

        //GameObject gearImage;
        AddingGearToPool();
    }

    private void SettingUpButton()
    {
        TextMeshProUGUI gearButtonNameDisplay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        gearButtonNameDisplay.text = nameOfButton; //setting the name

        Transform imageGearObject = Instantiate(placeableGear, imageContainer.transform).transform;

        foreach (Transform child in imageGearObject) //destroy the component inside the gameobject
        {
            print(child.name);
            Destroy(child.gameObject);
        }
        Gear gearComponent=imageGearObject.GetComponent<Gear>();
        Destroy(gearComponent); 
        Collider2D colliderComponent = gameObject.GetComponent<Collider2D>();
        Destroy(colliderComponent);
        RectTransform rectTransform=imageGearObject.AddComponent<RectTransform>();
        rectTransform.localScale = new Vector3(scale, scale, 0);
    }

    private void AddingGearToPool()
    {
        gearParent = GameObject.FindGameObjectWithTag("ParentGear");
        for (int i = 0; i < numberOfGear; i++)
        {
            GameObject gear = Instantiate(placeableGear, gearParent.transform);
            gear.SetActive(false);
            pooledGear.Enqueue(gear);
        } //stores the gameobjects in a queue and put it in the parent to keep it more organise
    }

    public GameObject GetGear()
    {
        if(pooledGear.Count > 0)
        {
            GameObject selectedGear = pooledGear.Dequeue();
            selectedGear.SetActive(true);
            return selectedGear;
        }
        else
        {
            GameObject gear = Instantiate(placeableGear, gearParent.transform);
            return gear;
        }
    }

    public void Removegear(GameObject selectedGear)
    {
        selectedGear.SetActive(false);
        pooledGear.Enqueue(selectedGear);
    }

}
