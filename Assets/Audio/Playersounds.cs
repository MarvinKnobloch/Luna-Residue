using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersounds : MonoBehaviour
{
    private AudioSource audiosource;
    [SerializeField] private AudioClip swordswing1;
    [SerializeField] private AudioClip swordswing2;
    [SerializeField] private AudioClip swordswing3;

    [SerializeField] private AudioClip bow1;
    [SerializeField] private AudioClip bow2;
    [SerializeField] private AudioClip bow3;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void playsound() => audiosource.Play();
    public void playsword1()
    {
        audiosource.clip = swordswing1;
        audiosource.Play();
    }
    public void playsword2()
    {
        audiosource.clip = swordswing2;
        audiosource.Play();
    }
    public void playsword3()
    {
        audiosource.clip = swordswing3;
        audiosource.Play();
    }
    public void loadbow1() => audiosource.clip = bow1;

    public void loadbow2() => audiosource.clip = bow2;

    public void loadbow3() => audiosource.clip = bow3;
}
