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
        slider.value = Statics.rangeweaponaimsensitivity * 50;
        slidertext.text = "" + Statics.rangeweaponaimsensitivity * 50;
        slider.onValueChanged.AddListener(delegate { valuechange(); });
    }
    private void valuechange()
    {
        Statics.rangeweaponaimsensitivity = slider.value / 50;
        slidertext.text = slider.value.ToString();
    }
}
