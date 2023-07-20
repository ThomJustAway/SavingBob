using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{

    [SerializeField] private float delay = 0.3f;

    //Scene button is for button that require to change scene.
    // some rules place so that it is much neater
    /*
        0,1 are the main menu and level menu respectively
        the other scene are the levels;
    */
    public void GoNextSceneBuild()
    {
        PlayClickButtonSound();
        StartCoroutine(PauseBeforeChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoSpecifiedScene(int index)
    {
        PlayClickButtonSound();
        // 1 is the level menu scene
        StartCoroutine(PauseBeforeChangeScene(index));
    }

    public void RestartScene()
    {
        PlayClickButtonSound();
        StartCoroutine(PauseBeforeChangeScene(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator PauseBeforeChangeScene(int scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    private void PlayClickButtonSound()
    {
        MusicManager.Instance.PlayMusicClip(SoundData.ClickButton);
    }

    public void StopGame()
    {
        Application.Quit();
    }
}
