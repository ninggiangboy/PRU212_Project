using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;


    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }


    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
