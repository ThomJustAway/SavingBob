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
    private string moneyIcon = " <sprite name=\"Money icon\">"; //money icon so that I dont have to repeat myself all the time
    private TextMeshProUGUI currentMoneyText; //the text to show players how much money the level has
    private LevelManager levelManager;
    private int currentMoney; //the current amount of money the players has left

    //this is used for animation to show how much money is earn or not.
    [SerializeField] private TextMeshProUGUI animationMoneyText; //this will show how much money is deducted/added 
    [SerializeField] private Animator animator;//use the animator to call out the animation


    private void Awake()
    {// setting up singleton instance
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
        //starting up the money manager
        currentMoneyText = GetComponent<TextMeshProUGUI>();
        levelManager = LevelManager.instance;
        currentMoney = levelManager.currentGameData.money; //get the amount of money to start of the game
        SetText(); //set the text of the money manager to reflect how much money is left 
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.M) && Input.GetKey(KeyCode.LeftControl)) { //cheat code to add more money to the game
            RefundCost(20); //will give the player extra 20 coins for them to use
        }
    }

    public bool IfCanSubstractCost(int money)
    {
        //this method is for other script to call to see if they can buy a dragable item

        if (currentMoney < money) //this means that the player has not enough money to afford the dragable item
        {
            MusicManager.Instance.PlayMusicClip(SoundData.NoMoneySoundEffect); 
            return false; //return false to tell the script that they cant purchase the rotatable item
        }
        else
        {
            //if can substract cost, do the following
            currentMoney -= money; //minus of the cost of the dragable item from the current money the player has left
            animationMoneyText.text = $"<color=#ff4d4d>-{money} " + moneyIcon; //show how much money is spend in the animation
            animator.SetTrigger("spend money"); //show the animation that spend money
            MusicManager.Instance.PlayMusicClip(SoundData.BuyingGear);
            SetText(); //change the text to reflect how much money the player has left
            return true;
        }
    }

    public void RefundCost(int money)
    {
        //this will show a add money animation that will play 
        currentMoney += money; //add money to the current money
        animationMoneyText.text = $"<color=\"green\">+{money}" + moneyIcon; // this will show how much money is added back to the current money
        animator.SetTrigger("refund money");// play the animation
        SetText();
        MusicManager.Instance.PlayMusicClip(SoundData.SellingGear);
    }

    private void SetText()
    {
        //this function just change the display on the text mesh pro component
        //this function will show how much money is left 
        currentMoneyText.text = currentMoney.ToString() + moneyIcon;
    }

    public void ResetMoney()
    {
        // this just show the animation to getting back the difference of money (this just mean they will get back all the money they lost)
        int differenceInMoney = LevelManager.instance.currentGameData.money - currentMoney;
        RefundCost(differenceInMoney);
    }

}
