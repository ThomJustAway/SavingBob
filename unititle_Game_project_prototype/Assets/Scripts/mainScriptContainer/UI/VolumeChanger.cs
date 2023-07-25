using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        volumeSlider.value = MusicManager.Instance.MasterVolume;
    }

    private void ChangeVolume()
    {
        MusicManager.Instance.ChangeSliderVolume(volumeSlider.value);
    }
}
