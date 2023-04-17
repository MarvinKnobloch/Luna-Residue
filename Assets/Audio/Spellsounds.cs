using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellsounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip singlehealend;
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void playsound(AudioClip newclip)
    {
        audiosource.clip = newclip;
        audiosource.Play();
    }

    public void playsinglehealend() => playsound(singlehealend);
}
