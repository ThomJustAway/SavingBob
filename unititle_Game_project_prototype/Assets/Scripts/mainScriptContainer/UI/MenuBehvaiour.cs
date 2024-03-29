using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehvaiour : MonoBehaviour
{

    //readme
    /*
        This is a minor script used in the unity engine.
        only used for menu like the gear and layer menu,.
    */
    private Animator animator;
    private int isActivatedHash = Animator.StringToHash("IsActivated");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnClick()
    {
        //toggle the animation when it is click. This can be found in the unity editor
        animator.SetBool(isActivatedHash, !animator.GetBool(isActivatedHash));
    }
}
