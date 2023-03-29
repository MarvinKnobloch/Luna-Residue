using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Audioslider : MonoBehaviour
{
    public AudioMixer audiomixer;
    public string gamevalue;
    private Slider slider;

    [SerializeField] private TextMeshProUGUI slidertext;


    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        bool gotvalue = audiomixer.GetFloat(gamevalue, out float soundvalue);
        if(gotvalue == true)
        {
            slider.value = soundvalue;
            float textvalue = (soundvalue + 80) * 1.25f;
            slidertext.text = Mathf.Round(textvalue).ToString();
        }
    }
    public void valuechange(float slidervalue)
    {
        PlayerPrefs.SetInt("audiohasbeenchange", 1);
        audiomixer.SetFloat(gamevalue, slidervalue);
        bool gotvalue = audiomixer.GetFloat(gamevalue, out float soundvalue);            //verhindert das der audiomixer mehr als 0db haben kann
        if (gotvalue == true)
        {
            if (soundvalue > 0)
            {
                Debug.Log(soundvalue);
                audiomixer.SetFloat(gamevalue, 0);
            }
        }
        PlayerPrefs.SetFloat(gamevalue, slidervalue);
        float textvalue = (slidervalue + 80) * 1.25f;
        slidertext.text = Mathf.Round(textvalue).ToString();
    }
}
