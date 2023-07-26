using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //took reference from bracky video https://www.youtube.com/watch?v=CE9VOZivb3I
    [SerializeField] private Animator animator;
    public static SceneTransitionManager instance; //requires singleton to have scene button to reference
    [SerializeField] private float timer = 1f;
    private void Start()
    {
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
