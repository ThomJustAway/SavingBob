using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    //this is the slider that will control the svx and background volume
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider SVXVolumeSlider;

    private void Start()
    {
        //adding callback function to the sliders to make sure to change the volume when it is done
        backgroundVolumeSlider.onValueChanged.AddListener(delegate { ChangeBackgroundVolume(); });
        backgroundVolumeSlider.value = MusicManager.Instance.BackgroundVolume;

        SVXVolumeSlider.onValueChanged.AddListener(delegate { ChangeSVXVolume(); });
        SVXVolumeSlider.value = MusicManager.Instance.SvxVolume;
    }

    private void ChangeBackgroundVolume()
    {
        //will change the value of the music manager background volume
        MusicManager.Instance.ChangeBackgroundVolume(backgroundVolumeSlider.value);
    }

    private void ChangeSVXVolume()
    { 
        //will change the value of the music manager SVX volume
        MusicManager.Instance.ChangeSVXVolume(SVXVolumeSlider.value);
    }
}
