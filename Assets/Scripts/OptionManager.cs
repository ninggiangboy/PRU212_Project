using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public TextMeshProUGUI valueVolume;

    public void SetVolumeValue(float value)
    {
        valueVolume.text = value.ToString();
    }
}
