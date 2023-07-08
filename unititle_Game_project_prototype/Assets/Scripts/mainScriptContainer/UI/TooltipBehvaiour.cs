using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipBehvaiour : MonoBehaviour
{
    public static TooltipBehvaiour instance; //singleton as I only want to have one tool tip to show the message
    private Animator animator;
    private TextMeshProUGUI textMessage;
    private int isActivatedHash = Animator.StringToHash("IsActivated");
    private bool isActivated = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        textMessage = GetComponent<TextMeshProUGUI>();
    }

    public void StartMessage(string message)
    {
        if (!isActivated)
        {
            isActivated = !isActivated;
            SetText(message);
            animator.SetBool(isActivatedHash, isActivated);
        }
    }

    public void EndMessage() 
    { 
        if (isActivated) 
        {
            isActivated = !isActivated;
            animator.SetBool(isActivatedHash, isActivated);
            
        }
    }

    private void SetText(string message)
    {
        textMessage.text = message;
    }
    
    public void ResetText()
    {
        textMessage.text = "";
    }

}
