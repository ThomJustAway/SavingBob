using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanelBehaviour : MonoBehaviour
{
    /*
        This script is for the end panel which is called when the level clear event is called
     */
    [SerializeField] private TextMeshProUGUI levelText; // set the text to current level
    void Start()
    {
        //refer to the rule where 0 and 1 are used for main menu and level menu respectively. The first level will start at build 2
        //therefore, -1 to each respective build will calculate the current level 

        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;         
        levelText.text = $"Level {currentLevel} Completed!";
        LevelManager.instance.SolvedEvent.AddListener(ActivatePanel);
    }

    private void ActivatePanel()
    {
        
        int isActivatedHash = Animator.StringToHash("IsActivated"); //the value to activate the animation
        Animator animator = GetComponent<Animator>();

        int playerCurrentLevel = PlayerPrefs.GetInt(LevelButton.Key);
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > playerCurrentLevel)
        {
            PlayerPrefs.SetInt(LevelButton.Key, nextLevel);
        }
        //allow player to excess the next level through level scene. look at level button

        animator.SetBool(isActivatedHash, true);  // call the winning animation
        MusicManager.Instance.PlayMusicClip(SoundData.WinningSound);
    }
}
