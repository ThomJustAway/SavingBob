using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    private string moneyIcon = " <sprite name=\"Money icon\">";
    private TextMeshProUGUI moneyText;
    private GameManager gameManager;
    private int money;

    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        gameManager = GameManager.instance;
        money = gameManager.currentGameData.money;
        moneyText.text = money.ToString() + moneyIcon;
    }

    
}
