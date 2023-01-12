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
        slider.value = Statics.mousesensitivity * 50;
        slidertext.text = "" + Statics.mousesensitivity * 50;
        slider.onValueChanged.AddListener(delegate { valuechange(); });
    }
    private void valuechange()
    {
        Statics.mousesensitivity = slider.value / 50;
        slidertext.text = slider.value.ToString();
    }
}
