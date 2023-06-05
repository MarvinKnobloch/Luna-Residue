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
    [SerializeField] private Menusoundcontroller menusoundcontroller;
    [SerializeField] private Image mutecheck;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        float soundvalue = PlayerPrefs.GetFloat(gamevalue);
        slider.value = soundvalue;
        float textvalue;
        if (gamevalue != "soundeffectsvolume") textvalue = (soundvalue + 50) * 2f;
        else textvalue = (soundvalue + 40) * 2f;
        slidertext.text = Mathf.Round(textvalue).ToString();
        if (PlayerPrefs.GetFloat(gamevalue + "ismuted") == 0) mutecheck.gameObject.SetActive(true);
        else mutecheck.gameObject.SetActive(false);

    }
    public void valuechange(float slidervalue)
    {
        PlayerPrefs.SetInt("audiohasbeenchange", 1);
        if (PlayerPrefs.GetFloat(gamevalue + "ismuted") == 0)
        {
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
        }
        PlayerPrefs.SetFloat(gamevalue, slidervalue);
        float textvalue = (slidervalue + 50) * 2f;
        slidertext.text = Mathf.Round(textvalue).ToString();
    }
    public void soundeffectvaluechange(float slidervalue)
    {
        PlayerPrefs.SetInt("audiohasbeenchange", 1);
        if (PlayerPrefs.GetFloat(gamevalue + "ismuted") == 0)
        {
            audiomixer.SetFloat(gamevalue, slidervalue);
            bool gotvalue = audiomixer.GetFloat(gamevalue, out float soundvalue);            //verhindert das der audiomixer mehr als 10db haben kann
            if (gotvalue == true)
            {
                if (soundvalue > 10)
                {
                    Debug.Log(soundvalue);
                    audiomixer.SetFloat(gamevalue, 10);
                }
            }
            menusoundcontroller.playmenubuttonsound();
        }     
        PlayerPrefs.SetFloat(gamevalue, slidervalue);
        float textvalue = (slidervalue + 40) * 2f;
        slidertext.text = Mathf.Round(textvalue).ToString();
    }

    public void changesoundstate()
    {
        PlayerPrefs.SetInt("audiohasbeenchange", 1);
        if (PlayerPrefs.GetFloat(gamevalue + "ismuted") == 0)
        {
            PlayerPrefs.SetFloat(gamevalue + "ismuted", 1);
            audiomixer.SetFloat(gamevalue, -80);
            mutecheck.gameObject.SetActive(false);
        }
            
        else
        {
            PlayerPrefs.SetFloat(gamevalue + "ismuted", 0);
            audiomixer.SetFloat(gamevalue, PlayerPrefs.GetFloat(gamevalue));
            bool gotvalue = audiomixer.GetFloat(gamevalue, out float soundvalue);            //verhindert das der audiomixer mehr als 10db haben kann
            if (gotvalue == true)
            {
                if (soundvalue > 10)
                {
                    Debug.Log(soundvalue);
                    audiomixer.SetFloat(gamevalue, 10);
                }
            }
            mutecheck.gameObject.SetActive(true);
        }          
    }
}
