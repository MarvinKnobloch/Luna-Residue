using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponsounds : MonoBehaviour
{
    public static Weaponsounds instance;
    private AudioSource audiosource;

    [SerializeField] private AudioClip swordmiss1;
    [SerializeField] private AudioClip swordmiss2;
    [SerializeField] private AudioClip swordhit1;
    [SerializeField] private AudioClip swordhit2;
    [SerializeField] private AudioClip fistmiss1;
    [SerializeField] private AudioClip fistmiss2;
    [SerializeField] private AudioClip fistmiss3;
    [SerializeField] private AudioClip fisthit1;
    [SerializeField] private AudioClip fisthit2;
    [SerializeField] private AudioClip fisthit3;
    [SerializeField] private AudioClip arrowimpact;
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void playsound(AudioClip newclip, float volume)
    {
        audiosource.clip = newclip;
        audiosource.volume = volume;
        audiosource.Play();
    }
    public void setswordmiss(float hit)
    {
        if (hit == 0) playsound(swordmiss1, 0.3f);
        else playsound(swordmiss2, 0.3f);
    }
    public void setswordhit(float hit)
    {
        if (hit == 0) playsound(swordhit1, 0.3f);
        else playsound(swordhit2, 0.3f);
    }
    public void setfistmiss(float hit)
    {
        if (hit == 0) playsound(fistmiss1, 0.3f);
        else if (hit == 1) playsound(fistmiss2, 0.3f);
        else playsound(fistmiss3, 0.3f);
    }
    public void setfisthit(float hit)
    {
        if (hit == 0) playsound(fisthit1, 0.3f);
        else if (hit == 1) playsound(fisthit2, 0.3f);
        else playsound(fisthit3, 0.3f);
    }

    public void playarrowimpact() => playsound(arrowimpact, 0.4f);

}
