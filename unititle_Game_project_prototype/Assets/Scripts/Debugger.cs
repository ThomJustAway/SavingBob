using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    /*Debugger is sort of a gameobject that acts
    as a cheat code. It helps set up the game level
    if there and help reset the leve. Great to move to 
    different level.
     */

    private void Start()
    {
        if (!PlayerPrefs.HasKey(LevelButton.Key)) // set up the game
        {//if player first start, they wont have the key so it will be set to scene 2 (level 1)
            PlayerPrefs.SetInt(LevelButton.Key, 2);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            print("hello");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt(LevelButton.Key, 2);
        } //if it hears the R key and left control, it will delete all the keys which reset the level
        else if (Input.GetKeyUp(KeyCode.I)){
            int data = PlayerPrefs.GetInt(LevelButton.Key);
            PlayerPrefs.SetInt(LevelButton.Key, data+1); //increment the level
        }
    }
}
