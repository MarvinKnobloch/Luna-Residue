using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellsounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip singlehealend;
    [SerializeField] private AudioClip groupheal;
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

    public void playsinglehealend() => playsound(singlehealend, 0.4f);
    public void playgroupheal() => playsound(groupheal, 0.35f);
    public void playresurrect() => playsound(resurrect, 0.3f);
}
