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

    private ClipSetter backgroundMusicClip;
    public float MasterVolume { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            MasterVolume = 1.0f;
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
            audioClips[i].AudioSource = audioInstance;
            if (audioClips[i].clip.name == "background Music")
            {
                audioInstance.loop = true;
                backgroundMusicClip = audioClips[i];
                audioInstance.Play();
            }
        }
    }

    public void PlayMusicClip(string clipName)
    {
        var audiosources = gameObject.GetComponents<AudioSource>();
        var clip = SearchForClip(clipName);    
        clip.AudioSource.volume = MasterVolume * clip.volume;
        clip.AudioSource.Play();
    }

    private ClipSetter SearchForClip(string clipName)
    {
        foreach (var clip in audioClips)
        {
            if (clip.clip.name == clipName)
            {
                return clip;
            }
        }
        Debug.LogError("There is no clips to play. there might be something wrong with the string");
        return audioClips[0];
    }

    public void ChangeSliderVolume(float volume)
    {
        MasterVolume = volume;
        backgroundMusicClip.AudioSource.volume = volume * backgroundMusicClip.volume ;
    }
}

[System.Serializable]
public struct ClipSetter
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;

    [HideInInspector] public AudioSource AudioSource;
}
