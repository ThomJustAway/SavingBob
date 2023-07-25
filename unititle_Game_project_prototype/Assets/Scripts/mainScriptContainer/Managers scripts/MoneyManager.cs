using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    //readme
    /*
        Keep track of the money in the game. singleton since I want to reference this object
        to different area of the code. It does all thing money related

    1. subtracting money
    2. get refund money
    3. play music and animation to show those changes.
    */
    public static MoneyManager instance; //singleton since I want to reference this object to different area of the code
    private string moneyIcon = " <sprite name=\"Money icon\">";
    private TextMeshProUGUI currentMoneyText;
    private LevelManager gameManager;
    private int currentMoney;

    //this is used for animation to show how much money is earn or not.
    [SerializeField] private TextMeshProUGUI animationMoneyText;
    [SerializeField] private Animator animator;


    private void Awake()
    {
        if (instance == null)
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
        gameManager = LevelManager.instance;
        currentMoney = gameManager.currentGameData.money;

        SetText();

    }

    public bool IfCanSubstractCost(int money)
    {
        if (currentMoney < money)
        {
            //if the cost of the specific item is more than the current money
            MusicManager.Instance.PlayMusicClip(SoundData.NoMoneySoundEffect);
            return false;
        }
        else
        {
            //if can
            currentMoney -= money;
            animationMoneyText.text = $"<color=#ff4d4d>-{money} " + moneyIcon;
            animator.SetTrigger("spend money");
            MusicManager.Instance.PlayMusicClip(SoundData.BuyingGear);
            SetText();
            return true;
        }
    }

    public void RefundCost(int money)
    {
        currentMoney += money;
        animationMoneyText.text = $"<color=\"green\">+{money}" + moneyIcon; // this code is quite bad since it is repetitive
        animator.SetTrigger("refund money");
        SetText();
        MusicManager.Instance.PlayMusicClip(SoundData.SellingGear);
    }

    private void SetText()
    {
        currentMoneyText.text = currentMoney.ToString() + moneyIcon;
    }

    public void ResetMoney()
    {
        int differenceInMoney = LevelManager.instance.currentGameData.money - currentMoney;
        RefundCost(differenceInMoney);
    }

}
