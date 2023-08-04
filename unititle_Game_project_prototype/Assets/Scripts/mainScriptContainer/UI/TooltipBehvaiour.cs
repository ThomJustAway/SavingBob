using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class TooltipBehvaiour : MonoBehaviour
{

    // Tooltip is just a text that shows up to inform players
    public static TooltipBehvaiour instance; //singleton as I only want to have one tool tip to show the message
    private Animator animator;
    private TextMeshProUGUI textMessage;
    private int isActivatedHash = Animator.StringToHash("IsActivated");
    public bool IsActivated { get; private set; }

    private void Awake()
    {
        //set up instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        //setting up the values
        animator = GetComponent<Animator>();
        textMessage = GetComponent<TextMeshProUGUI>();
        IsActivated = false;
    }

    public void StartMessage(string message)
    {
        if (!IsActivated)
        { //check if the tool tip is being used. if it is not then tooltip will be used to show the message
            IsActivated = !IsActivated;
            SetText(message);
            animator.SetBool(isActivatedHash, IsActivated);
        }
    }

    public void EndMessage()
    {
        if (IsActivated)
        {//if the tool tip is used, close the tooltip
            IsActivated = !IsActivated;
            animator.SetBool(isActivatedHash, IsActivated);
        }
    }

    public void SetText(string message)
    {
        //just change the text of the tool tip in some cases (like endgear script)
        textMessage.text = message;
    }

    public void ResetText()
    {
        //this is called in unity editor so you wont see the references
        textMessage.text = "";
    }

    public void StartShortMessage(string message) //this is just used for some edge cases
    {
        //this will help show a short message without using the start and stop message
        StartCoroutine(ShortMessage(message));
    }

    private IEnumerator ShortMessage(string message)
    {
        StartMessage(message);
        yield return new WaitForSeconds(3);
        EndMessage();
    }

}
