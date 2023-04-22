using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supportspellsounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip bottleheal;

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
    public void playbottleheal() => playsound(bottleheal, 0.35f);
}
