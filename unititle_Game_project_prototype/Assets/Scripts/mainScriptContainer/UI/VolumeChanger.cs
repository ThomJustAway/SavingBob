using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider SVXVolumeSlider;

    private void Start()
    {
        backgroundVolumeSlider.onValueChanged.AddListener(delegate { ChangeBackgroundVolume(); });
        backgroundVolumeSlider.value = MusicManager.Instance.BackgroundVolume;

        SVXVolumeSlider.onValueChanged.AddListener(delegate { ChangeSVXVolume(); });
        SVXVolumeSlider.value = MusicManager.Instance.SvxVolume;
    }

    private void ChangeBackgroundVolume()
    {
        MusicManager.Instance.ChangeBackgroundVolume(backgroundVolumeSlider.value);
    }

    private void ChangeSVXVolume()
    {
        MusicManager.Instance.ChangeSVXVolume(SVXVolumeSlider.value);
    }
}
