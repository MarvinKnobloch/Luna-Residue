using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playersounds : MonoBehaviour
{
    private AudioSource audiosource;

    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip charswitch;

    [SerializeField] private AudioClip healingstate;
    [SerializeField] private AudioClip singleheal;

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
    public void playsound(AudioClip newclip) 
    {
        audiosource.clip = newclip;
        audiosource.Play();
    }

    public void playdash() => playsound(dash);
    public void playcharswitch() => playsound(charswitch);
    public void playsingleheal() => playsound(singleheal);
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
    public void playbow1()
    {
        audiosource.clip = bow1;
        audiosource.Play();
    }
    public void playbow2()
    {
        audiosource.clip = bow2;
        audiosource.Play();
    }
    public void playbow3()
    {
        audiosource.clip = bow3;
        audiosource.Play();
    }

    public void playsoundwithloop(AudioClip newclip)
    {
        audiosource.loop = true;
        audiosource.clip = newclip;
        audiosource.Play();
    }
    public void stopsoundloop()
    {
        audiosource.loop = false;
        audiosource.clip = null;
    }
    public void playhealingstate() => playsoundwithloop(healingstate);



}
