using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    /*
        took reference from bracky video https://www.youtube.com/watch?v=CE9VOZivb3I
        Scene transition manager is a gameobject that does help transition from one scene to another
        it is a singleton where different part of scripts like scenebutton and levelbutton that use
        this script to transition from one level to another.
     
        It will have an animation that will play before the start of game. When players call the 
        StartTransition(), it will play an exit animation before transitioning to another scene.
        This gives the illusion that there is a seemless transition between one scene to another.
     */

    [SerializeField] private Animator animator;
    public static SceneTransitionManager instance; //requires singleton to have scene button to reference
    [SerializeField] private float timer = 1f; // wait timers
    private void Start()
    {
        //setting up instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartTransition(int scene)
    {
        StartCoroutine(StartTransitonCoroutine(scene));
    }

    public void EndApplication()
    {
        StartCoroutine(EndApplicationCoroutine());
    }

    private IEnumerator EndApplicationCoroutine()
    {
        animator.SetBool("isactivated", true);
        yield return new WaitForSeconds(timer);
        Application.Quit(); 
    }


    private IEnumerator StartTransitonCoroutine(int scene)
    {
        animator.SetBool("isactivated", true); 
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(scene);
    }

    
}
