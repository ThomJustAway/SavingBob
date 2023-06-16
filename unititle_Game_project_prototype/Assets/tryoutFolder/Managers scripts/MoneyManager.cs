using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance; //singleton since I wantto reference this object to different area of the code
    private string moneyIcon = " <sprite name=\"Money icon\">";
    private TextMeshProUGUI moneyText;
    private GameManager gameManager;
    private int currentMoney;

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
        moneyText = GetComponent<TextMeshProUGUI>();
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
            SetText();
            return true;
        }
    }

    private void SetText()
    {
        moneyText.text = currentMoney.ToString() + moneyIcon;
    }

}
