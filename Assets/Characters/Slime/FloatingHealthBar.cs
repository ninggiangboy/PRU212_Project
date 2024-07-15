using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    private void Start()
    {
        slider.value = 1;
    }

    // Update is called once per frame
    private void Update()
    {
    }


    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}