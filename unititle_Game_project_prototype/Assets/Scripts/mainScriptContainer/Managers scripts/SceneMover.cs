using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public SceneMover instance { get; private set; } // another singleton as I want have a single entity to move scenes

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void GoLevelScene()
    {
        SceneManager.LoadScene(1);
    }

}
