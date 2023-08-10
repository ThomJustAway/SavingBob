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
    [SerializeField] private ClipSetter[] audioClips = new ClipSetter[4]; //variable to store all the clips in the game
    public static MusicManager Instance { get; private set; } 

    private ClipSetter backgroundMusicClip; //will have to store the backgroundmusicclip as the background musicclip is speical
    public float BackgroundVolume { get; private set; } //float to control the background and svx volume

    public float SvxVolume { get; private set; }

    public bool CanPlaySvxVolume { get; private set; } //this boolen will determine whether the background music should be played
    public bool CanPlayBackgroundVolume { get; private set; }

    private void Awake()
    {
        //setting up the music manager 
        if (Instance == null)
        {
            //make sure that the backgroundvolume and svxvolume is both at max 
            Instance = this;
            BackgroundVolume = 1.0f;
            SvxVolume = 1.0f;
            //make sure the manager can play both svx and background volume
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
            audioInstance.volume = audioClips[i].volume;//seting the audiosource
            audioClips[i].AudioSource = audioInstance; //storing the audio source so that it can be called when the clip is played

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
        //different script can call this function to play a clip
        if (CanPlaySvxVolume)
        { //will play the clip only if it the svxvolume bool is true (which can only be toggled in the pause menu)
            var clip = SearchForClip(clipName);
            clip.AudioSource.volume = SvxVolume * clip.volume; //Sometimes different clips will have different volume, the clip.volume
            //tell the audiosource what should be the clip maximum volume be. Svxvolume toggles the percentage it should be played.
            //that is why the audiosource.volume should be =svxvolume * clip.volume
            clip.AudioSource.Play();
        }
    }

    private ClipSetter SearchForClip(string clipName)
    {
        foreach (var clip in audioClips)
        {//will iterate over all the audioclip to find the name
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
        //will set the overall volume of the background music clip. You can think of backgroundvolume as a percentage
        //that can be toggled to play the background softly or loudly. 100% (or 1f) would mean that the background should
        //be played at maximum volume
    }

    public void ChangeSVXVolume(float volume)
    {
        SvxVolume = volume; //changing the percentage of the svx volume so it affects all the clips pertaining to svx
    }

    public void ToggleCanPlayBackgroundMusic()
    {

        //this is to toggle the canplaybackgroundvolume bool
        CanPlayBackgroundVolume = !CanPlayBackgroundVolume;//change the bool
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
        //will toggle the svxmusic
        CanPlaySvxVolume = !CanPlaySvxVolume;
    }

}

[System.Serializable]
public struct ClipSetter
{

    //this Clip setter is just to store data about the audio clip for the music manager to use
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;//the volume set here is the maximum volume the clip should be played at. different clip will have different volume

    [HideInInspector] public AudioSource AudioSource; //the audiosource the clip will have.
}
