using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{


    //Scene button is for button that require to change scene.
    // some rules place so that it is much neater
    /*
        0,1 are the main menu and level menu respectively
        the other scene are the levels;

    NOTE: This is not the same as Level button
    */
 

    public void GoNextSceneBuild()
    {
        PlayClickButtonSound();
        int currentbuild = SceneManager.GetActiveScene().buildIndex;
        SceneTransitionManager.instance.StartTransition(currentbuild + 1);
        
    }

    public void GoSpecifiedScene(int scene)
    {
        PlayClickButtonSound();
        // 1 is the level menu scene
        SceneTransitionManager.instance.StartTransition(scene);

    }

    private void PlayClickButtonSound()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
    }

    public void StopGame()
    {
        SceneTransitionManager.instance.EndApplication();
    }
}
