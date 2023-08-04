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
    public float BackgroundVolume { get; private set; }

    public float SvxVolume { get; private set; }

    public bool CanPlaySvxVolume { get; private set; }
    public bool CanPlayBackgroundVolume { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            //make sure that the backgroundvolume and svxvolume is both at max 
            Instance = this;
            BackgroundVolume = 1.0f;
            SvxVolume = 1.0f;
            CanPlaySvxVolume = true;
            CanPlayBackgroundVolume = true;
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
            //setting an audio source for every music clip that will be played in the game
            var audioInstance = gameObject.AddComponent<AudioSource>();
            audioInstance.clip = audioClips[i].clip;
            audioInstance.volume = audioClips[i].volume;
            audioClips[i].AudioSource = audioInstance;

            //background music is more special since it plays at a loop 
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
        if (CanPlaySvxVolume)
        { //will play the clip only if it is enabled on the menu settings 
            var clip = SearchForClip(clipName);
            clip.AudioSource.volume = SvxVolume * clip.volume;
            clip.AudioSource.Play();
        }
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

    public void ChangeBackgroundVolume(float volume)
    {
        BackgroundVolume = volume; //changing the volume accordingly
        backgroundMusicClip.AudioSource.volume = volume * backgroundMusicClip.volume;
    }

    public void ChangeSVXVolume(float volume)
    {
        SvxVolume = volume; //changing the volume accordingly
    }

    public void ToggleCanPlayBackgroundMusic()
    {
        CanPlayBackgroundVolume = !CanPlayBackgroundVolume;
        if (!CanPlayBackgroundVolume)
        {
            //that mean you cant play the background music
            backgroundMusicClip.AudioSource.mute = true;
        }
        else
        {
            //mean that you can play background music
            backgroundMusicClip.AudioSource.mute = false;
        }
    }

    public void ToggleCanPlaySVXMusic()
    {
        CanPlaySvxVolume = !CanPlaySvxVolume;
    }

}

[System.Serializable]
public struct ClipSetter
{

    //this Clip setter is just to store data about the audio clip for the music manager to use
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;

    [HideInInspector] public AudioSource AudioSource;
}
