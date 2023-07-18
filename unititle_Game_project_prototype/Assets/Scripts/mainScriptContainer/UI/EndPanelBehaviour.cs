using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanelBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText; // set the text to current level

    void Start()
    {
        //refer to the rule where 0 and 1 are used for main menu and level menu respectively. The first level will start at build 2
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1; //therefore, -1 to each respective build will calculate the current level 
        levelText.text = $"Level {currentLevel} Completed!";
        GameManager.instance.SolvedEvent.AddListener(ActivatePanel);
    }

    private void ActivatePanel()
    {
        int isActivatedHash = Animator.StringToHash("IsActivated");
        Animator animator = GetComponent<Animator>();
        animator.SetBool(isActivatedHash, true); //start playing the animation
        MusicManager.Instance.PlayMusicClip(SoundData.WinningSound);
    }



}
