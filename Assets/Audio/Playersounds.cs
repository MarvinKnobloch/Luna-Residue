using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip charswitch;

    [SerializeField] private AudioClip healingstate;
    [SerializeField] private AudioClip singlehealstart;

    [SerializeField] private AudioClip bow1;
    [SerializeField] private AudioClip bow2;
    [SerializeField] private AudioClip bow3;

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

    public void playfootstep1() => playsound(footstep1, 0.2f);
    public void playfootstep2() => playsound(footstep2, 0.2f);
    public void playdash() => playsound(dash, 0.4f);
    public void playcharswitch() => playsound(charswitch, 0.4f);
    public void playsinglehealstart() => playsound(singlehealstart, 0.4f);
    public void playbow1() => playsound(bow1, 0.4f);
    public void playbow2() => playsound(bow2, 0.4f);
    public void playbow3() => playsound(bow3, 0.4f);


    public void stopsound()
    {
        audiosource.clip = null;
    }



}
