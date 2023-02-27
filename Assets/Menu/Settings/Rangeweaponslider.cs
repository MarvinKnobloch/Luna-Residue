using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rangeweaponslider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI slidertext;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat("rangeweaponaimsensitivity");
        slidertext.text = PlayerPrefs.GetFloat("rangeweaponaimsensitivity").ToString();
        slider.onValueChanged.AddListener(delegate { valuechange(); });
    }
    private void valuechange()
    {
        float slidervalue = slider.value;
        PlayerPrefs.SetFloat("rangeweaponaimsensitivity", slidervalue);
        slidertext.text = slider.value.ToString();
    }
}
