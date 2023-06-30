using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fpsslider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderfpstext;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        sliderfpstext.text = PlayerPrefs.GetInt("fpslimit").ToString();
        slider.value = PlayerPrefs.GetInt("fpslimit");
    }
    private void fpsupdate()
    {
        if (PlayerPrefs.GetInt("fpslimit") == 0 || PlayerPrefs.GetInt("fpslimit") > 200)
        {
            PlayerPrefs.SetInt("fpslimit", 60);
            Application.targetFrameRate = PlayerPrefs.GetInt("fpslimit");
        }
        else Application.targetFrameRate = PlayerPrefs.GetInt("fpslimit");
    }
    public void valuechange(float slidervalue)
    {
        PlayerPrefs.SetInt("fpslimit", (int)slidervalue);
        sliderfpstext.text = PlayerPrefs.GetInt("fpslimit").ToString();
        fpsupdate();
    }
}
