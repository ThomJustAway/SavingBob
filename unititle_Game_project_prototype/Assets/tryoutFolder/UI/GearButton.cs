using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearButton : MonoBehaviour
{
    [SerializeField]private GameObject placeableGear;
    [SerializeField]private int numberOfGear = 10;

    private Queue<GameObject> pooledGear = new Queue<GameObject>();
    private GameObject gearParent;
    [SerializeField] private string nameOfButton;
    private void Start()
    {
        TextMeshPro gearButtonNameDisplay = gameObject.GetComponentInParent<TextMeshPro>();
        gearButtonNameDisplay.text = nameOfButton;
        //GameObject gearImage;
        AddingGearToPool();
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
