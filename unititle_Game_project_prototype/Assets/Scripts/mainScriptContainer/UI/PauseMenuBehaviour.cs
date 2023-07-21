using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBehaviour : MonoBehaviour
{
    // this script will listen for a click in the esc button
    //will pause the game.
    [SerializeField] private GameObject PauseMenu;
    private bool isActive = false;

    void Start()
    {
        PauseMenu.SetActive(isActive);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isActive = !isActive;
            PauseMenu.SetActive(isActive);
        }
    }
}
