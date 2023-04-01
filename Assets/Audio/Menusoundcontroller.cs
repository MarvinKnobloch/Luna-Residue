using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menusoundcontroller : MonoBehaviour
{
    private AudioSource audiosource;
    [SerializeField] private AudioClip menubuttonsound;
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void playmenubuttonsound()
    {
        if(audiosource != null)               //falls der awake call zu spät kommt
        {
            audiosource.clip = menubuttonsound;
            audiosource.Play();
        }
        else
        {
            audiosource = GetComponent<AudioSource>();
            audiosource.clip = menubuttonsound;
            audiosource.Play();
        }
    }
}
