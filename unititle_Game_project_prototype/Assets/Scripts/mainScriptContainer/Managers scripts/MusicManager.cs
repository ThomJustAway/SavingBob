using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            var audioInstance = gameObject.AddComponent<AudioSource>();
            audioInstance.clip = audioClips[i];
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
