using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public TextMeshProUGUI valueVolume;
    public Slider sliderVolume;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void SetVolumeValue()
    {
        var value = (int)Math.Round(sliderVolume.value * 100);
        valueVolume.text = value.ToString();
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = sliderVolume.value;
        Save();
    }

    public void Load()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", sliderVolume.value);
    }
}