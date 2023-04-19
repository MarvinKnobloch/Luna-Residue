using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspezialsound : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip werewolfspezialsound;
    [SerializeField] private AudioClip plantspezialsound;
    [SerializeField] private AudioClip vampirespezialsound;             
    [SerializeField] private AudioClip fishmanspezialendsound;          //fishman start hat seine eigene audiosource bei fishmancircle
    [SerializeField] private AudioClip goblinspezialsound;
    [SerializeField] private AudioClip elkspezialsoundstart;
    [SerializeField] private AudioClip elkspezialsoundmid;
    [SerializeField] private AudioClip elkspezialsoundend;
    [SerializeField] private AudioClip nightshadespezialsound;
    [SerializeField] private AudioClip mathmanspezialsound;
    [SerializeField] private AudioClip zombiespezialsound;
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
    public void playfishmanspezialend() => playsound(fishmanspezialendsound, 0.4f);
    public void playgoblinspezial() => playsound(goblinspezialsound, 0.4f);
    public void playelkspezialstart() => playsound(elkspezialsoundstart, 0.4f);
    public void playelkspezialmid() => playsound(elkspezialsoundmid, 0.4f);
    public void playelkspezialend() => playsound(elkspezialsoundend, 0.4f);
    public void playnightshadespezial() => playsound(nightshadespezialsound, 0.5f);
    public void playmathmanspezial() => playsound(mathmanspezialsound, 0.4f);
    public void playzombiespezial() => playsound(zombiespezialsound, 0.4f);
    
}
