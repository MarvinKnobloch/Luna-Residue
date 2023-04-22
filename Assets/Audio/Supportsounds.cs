using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportsounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip resurrect;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void playsound(AudioClip newclip, float volume)
    {
        audiosource.clip = newclip;
        audiosource.volume = volume;
        audiosource.Play();
    }
    public void playresurrect() => playsound(resurrect, 0.3f);
}
