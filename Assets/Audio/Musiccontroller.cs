using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musiccontroller : MonoBehaviour
{
    public static Musiccontroller instance;
    [SerializeField] private AudioSource audiosource;
    private AudioClip lastplayedsong;
    private float lastplayedsongtime;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audiosource = GetComponent<AudioSource>();
    }
    public void setclip(AudioClip clip)
    {
        audiosource.clip = clip;
        audiosource.time = 0;
        audiosource.Play();
    }
    public void savelastplayedsong()
    {
        lastplayedsong = audiosource.clip;
        lastplayedsongtime = audiosource.time;
    }
    public IEnumerator fadeoutvolume()
    {
        float duration = 1.5f;
        float currentTime = 0;
        float start = audiosource.volume;
        float targetVolume = 0.3f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        playlastplayedsong();
        yield break;
    }
    public void playlastplayedsong()
    {
        audiosource.clip = lastplayedsong;
        audiosource.time = lastplayedsongtime;
        audiosource.Play();
        StartCoroutine("fadeinvolume");
    }
    public IEnumerator fadeinvolume()
    {
        float duration = 4f;
        float currentTime = 0;
        float start = audiosource.volume;
        float targetVolume = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}