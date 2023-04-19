using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspezialsound : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip werewolfspezialsound;
    [SerializeField] private AudioClip plantspezialsound;
    [SerializeField] private AudioClip vampirespezialsound;
    [SerializeField] private AudioClip fishmanspezialstartsound;
    [SerializeField] private AudioClip fishmanspezialendsound;
    [SerializeField] private AudioClip goblinspezialsound;
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

    public void playwerewolfspezial() => playsound(werewolfspezialsound, 0.4f);
    public void playplantspezial() => playsound(plantspezialsound, 0.4f);
    public void playvampirespezial() => playsound(vampirespezialsound, 0.3f);
    public void playfishmanspezial() => playsound(fishmanspezialstartsound, 0.4f);
    public void playfishmanspezialend() => playsound(fishmanspezialendsound, 0.4f);
    public void playgoblinspezial() => playsound(goblinspezialsound, 0.4f);
    
}
