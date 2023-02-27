using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mouseslider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI slidertext;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("mousesensitivity");
        slidertext.text = PlayerPrefs.GetFloat("mousesensitivity").ToString();
        slider.onValueChanged.AddListener(delegate { valuechange(); });
    }
    private void valuechange()
    {
        float slidervalue = slider.value;
        PlayerPrefs.SetFloat("mousesensitivity", slidervalue);
        slidertext.text = slider.value.ToString();
    }
}
