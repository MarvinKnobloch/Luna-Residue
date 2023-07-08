using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspezialsound : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip werewolfspezialsound;
    [SerializeField] private AudioClip werewolfexplosionspezialsound;
    [SerializeField] private AudioClip plantspezialsound;
    [SerializeField] private AudioClip plantmushroomexplosionsound;
    [SerializeField] private AudioClip vampirespezialsound;
    [SerializeField] private AudioClip vampirespherespezialsound;
    [SerializeField] private AudioClip vampireendspezialsound;
    [SerializeField] private AudioClip fishmanspezialendsound;          //fishman start hat seine eigene audiosource bei fishmancircle
    [SerializeField] private AudioClip goblinspezialsound;
    [SerializeField] private AudioClip goblinexplosionspezialsound;
    [SerializeField] private AudioClip doozyspezialsound;
    //[SerializeField] private AudioClip knightspezialsound;           //knightcontroller
    //[SerializeField] private AudioClip ninjaspezialsound;            //hat eigene audiosource
    [SerializeField] private AudioClip elkspezialsoundstart;
    [SerializeField] private AudioClip elkspezialsoundmid;
    [SerializeField] private AudioClip elkspezialsoundend;
    [SerializeField] private AudioClip gasmanspezialsound;             //laser sound macht Line1
    [SerializeField] private AudioClip nightshadespezialsound;
    [SerializeField] private AudioClip mathmanspezialsound;
    [SerializeField] private AudioClip paladinspezialsound;
    [SerializeField] private AudioClip battlemonkspezialsound;
    [SerializeField] private AudioClip battlemonkspezialhitsound;
    [SerializeField] private AudioClip zombiespezialsound;
    [SerializeField] private AudioClip zombiespezialbombsound;
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

    public void playwerewolfspezial() => playsound(werewolfspezialsound, 0.5f);
    public void playwerewolfexplosionspezial() => playsound(werewolfexplosionspezialsound, 0.2f);
    public void playplantspezial() => playsound(plantspezialsound, 0.4f);
    public void playplantmushroomexplosionspezial() => playsound(plantmushroomexplosionsound, 0.3f);
    public void playvampirespezial() => playsound(vampirespezialsound, 0.3f);
    public void playvampirespherespezial() => playsound(vampirespherespezialsound, 0.2f);
    public void playvampireendspezial() => playsound(vampireendspezialsound, 0.2f);
    public void playfishmanspezialend() => playsound(fishmanspezialendsound, 0.4f);
    public void playgoblinspezial() => playsound(goblinspezialsound, 0.4f);
    public void playgoblinexplosionspezial() => playsound(goblinexplosionspezialsound, 0.3f);
    public void playdoozyspezial() => playsound(doozyspezialsound, 0.5f);
    public void playelkspezialstart() => playsound(elkspezialsoundstart, 0.4f);
    public void playelkspezialmid() => playsound(elkspezialsoundmid, 0.4f);
    public void playelkspezialend() => playsound(elkspezialsoundend, 0.4f);
    public void playgasmanspezial() => playsound(gasmanspezialsound, 0.4f);
    public void playnightshadespezial() => playsound(nightshadespezialsound, 0.6f);
    public void playmathmanspezial() => playsound(mathmanspezialsound, 0.4f);
    public void playpaladinspezial() => playsound(paladinspezialsound, 0.3f);
    public void playbattlemonkspezial() => playsound(battlemonkspezialsound, 0.4f);
    public void playbattlemonkhitspezial() => playsound(battlemonkspezialhitsound, 0.4f);
    public void playzombiespezial() => playsound(zombiespezialsound, 0.4f);
    public void playzombiebombspezial() => playsound(zombiespezialbombsound, 0.2f);

}
