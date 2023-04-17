using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponsounds : MonoBehaviour
{
    public static Weaponsounds instance;
    private AudioSource audiosource;

    [SerializeField] private AudioClip swordimpact;
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
    public void playsound(AudioClip newclip)
    {
        audiosource.clip = newclip;
        audiosource.Play();
    }

    public void playswordimpact() => playsound(swordimpact);
    public void playarrowimpact() => playsound(arrowimpact);

}
