using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;

    public void UpdateHealthBar(float currentValue, float maxValue) 
    {
        slider.value = currentValue/maxValue;
    }

    void Update()
    {
        target.position = target.position;
    }
}
