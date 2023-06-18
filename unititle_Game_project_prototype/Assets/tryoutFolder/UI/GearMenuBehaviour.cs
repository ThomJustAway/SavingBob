using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMenuBehaviour : MonoBehaviour
{
    private Animator animator;
    private int isActivatedHash = Animator.StringToHash("IsActivated");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnClick()
    {
        animator.SetBool(isActivatedHash, !animator.GetBool(isActivatedHash));
    }



}
