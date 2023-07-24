using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //readme
    /*
        The musicmanager is sort of inspired by brackey video
    but I did not follow through the tutorial as I got the idea https://www.youtube.com/watch?v=6OT43pvUyfY .

    I did not like how they use magic strings to play music. So I decided
    to use a script to store the data (sounddata script). This is a script
    that persist throughout the game as you can see from the DontDestroyOnLoad().

    it does one thing which is to play music clips .
    I am not too sure If I should add a button to stop and start music.
    */
    [SerializeField] private ClipSetter[] audioClips = new ClipSetter[4];
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            var audioInstance = gameObject.AddComponent<AudioSource>();
            audioInstance.clip = audioClips[i].clip;
            audioInstance.volume = audioClips[i].volume;
        }
    }

    public void PlayMusicClip(string clipName)
    {
        var audiosources = gameObject.GetComponents<AudioSource>();
        foreach (var audioSource in audiosources)
        {
            if (audioSource.clip.name == clipName)
            {
                audioSource.Play();
                return;
            }
        }
        Debug.LogError("There is no clips to play. there might be something wrong with the string");
    }
}

[System.Serializable]
public struct ClipSetter
{
    public AudioClip clip;
    [Range(0,1)]
    public float volume;
}
