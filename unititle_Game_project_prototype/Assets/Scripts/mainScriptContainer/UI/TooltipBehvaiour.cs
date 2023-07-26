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
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        textMessage = GetComponent<TextMeshProUGUI>();
        IsActivated = false;
    }

    public void StartMessage(string message)
    {
        if (!IsActivated)
        {
            IsActivated = !IsActivated;
            SetText(message);
            animator.SetBool(isActivatedHash, IsActivated);
        }
    }

    public void EndMessage()
    {
        if (IsActivated)
        {
            IsActivated = !IsActivated;
            animator.SetBool(isActivatedHash, IsActivated);
        }
    }

    public void SetText(string message)
    {
        textMessage.text = message;
    }

    public void ResetText()
    {
        //this is called in some other script so you wont see the references
        textMessage.text = "";
    }

    public void StartShortMessage(string message)
    {
        StartCoroutine(ShortMessage(message));
    }

    private IEnumerator ShortMessage(string message)
    {
        StartMessage(message);
        yield return new WaitForSeconds(3);
        EndMessage();
    }

}
