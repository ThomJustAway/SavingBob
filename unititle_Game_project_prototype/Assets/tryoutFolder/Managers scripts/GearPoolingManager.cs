using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPoolingManager : MonoBehaviour
{
    [SerializeField]private GameObject placeableGear;
    private Queue<GameObject> pooledGear = new Queue<GameObject>();
    [SerializeField]private int numberOfGear = 10;
    private GameObject gearParent;

    private void Start()
    {
        gearParent = GameObject.FindGameObjectWithTag("ParentGear");
        for (int i = 0; i < numberOfGear; i++) {
            GameObject gear =Instantiate(placeableGear,gearParent.transform);
            gear.SetActive(false);
            pooledGear.Enqueue(gear);
        } //stores the gameobject in a queue and put it in the parent to keep it more organise
    }

    public GameObject MakeGear()
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
