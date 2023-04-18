using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemysounds : MonoBehaviour
{
    public static Enemysounds instance;
    private AudioSource audiosource;

    [SerializeField] private AudioClip playergothit;
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

    public void playplayergothit() => playsound(playergothit, 0.5f);
}
