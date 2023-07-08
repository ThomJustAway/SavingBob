﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; //singleton since I wantto reference this object to different area of the code
    private string moneyIcon = " <sprite name=\"Money icon\">";
    private TextMeshProUGUI currentMoneyText;
    private GameManager gameManager;
    private int currentMoney;

    //this is used for animation to show how much money is earn or not.
    [SerializeField]private TextMeshProUGUI animationMoneyText;
    [SerializeField]private Animator animator;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        currentMoneyText = GetComponent<TextMeshProUGUI>();
        gameManager = GameManager.instance;
        currentMoney = gameManager.currentGameData.money;

        SetText();
      
    }

    public bool IfCanSubstractCost(int money)
    {
        if(currentMoney < money)
        {
            //if the cost of the specific item is more than the current money
            print("No money!");
            return false;
        }
        else
        {
            //if can
            currentMoney -= money;
            animationMoneyText.text = $"<color=#ff4d4d>-{money} "+moneyIcon;
            animator.SetTrigger("spend money");
            SetText();
            return true;
        }
    }

    public void RefundCost(int money)
    {
        currentMoney += money;
        animationMoneyText.text = $"<color=\"green\">+{money}"+ moneyIcon; // this code is quite bad since it is repetitive
        animator.SetTrigger("refund money");
        SetText();
        
    }

    private void SetText()
    {
        currentMoneyText.text = currentMoney.ToString() + moneyIcon;
    }

    private IEnumerator OnCompleteAttackAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        // TODO: Do something when animation did complete
    }

}