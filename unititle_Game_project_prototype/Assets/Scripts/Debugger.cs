using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Update is called once per frame

    private void Start()
    {
        if (!PlayerPrefs.HasKey(LevelButton.Key))
        {
            PlayerPrefs.SetInt(LevelButton.Key, 2);
        }
        print(PlayerPrefs.GetInt(LevelButton.Key));
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            print("hello");
            PlayerPrefs.DeleteAll();
        }
        else if (Input.GetKeyUp(KeyCode.I)){
            print("increment level");
            int data = PlayerPrefs.GetInt(LevelButton.Key);
            print($"current layer: {data} new layer {data + 1}");
            PlayerPrefs.SetInt(LevelButton.Key, data+1);
        }

    }
}
