using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musiccontroller : MonoBehaviour
{
    public static Musiccontroller instance;
    [SerializeField] private AudioSource audiosource;

    public AudioClip currentzonemusic;
    public float currentzonemusictime;

    public AudioClip oldzonemusic;
    public float zoneentertime;
    public float oldzonemusictimer;


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

    public void setcurrentzonemusic(AudioClip song)
    {
        if (currentzonemusic != song)
        {
            currentzonemusic = song;
            audiosource.clip = currentzonemusic;
            if (oldzonemusic == currentzonemusic)
            {
                float timedifference = Mathf.Abs(Time.time - zoneentertime);
                if (timedifference > 20)                                      // wenn die zeit zwischen den gleichen zonen wechsel länger als XXsec her ist wird der song zurückgesetzt
                {
                    audiosource.time = 0;
                }
                else
                {
                    audiosource.time = oldzonemusictimer;
                }
            }
            else audiosource.time = 0;
            audiosource.Play();
            StartCoroutine(fadeinvolume(3));
        }
    }

    public void savezonemusictime()
    {
        currentzonemusictime = audiosource.time;
    }
    public void startfadeout(AudioClip nextclip, float cliptime, float fadeoutspeed, float fadeinspeed)
    {
        StopAllCoroutines();
        StartCoroutine(fadeoutvolume(nextclip, cliptime, fadeoutspeed, fadeinspeed));
    }
    public IEnumerator fadeoutvolume(AudioClip nextclip, float cliptime, float fadeoutspeed, float fadeinspeed)
    {
        float duration = fadeoutspeed;
        float currentTime = 0;
        float start = audiosource.volume;
        float targetVolume = 0.1f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audiosource.clip = nextclip;
        audiosource.time = cliptime;
        audiosource.Play();
        StartCoroutine(fadeinvolume(fadeinspeed));
        yield break;
    }
    public IEnumerator fadeinvolume(float fadeinspeed)
    {
        float duration = fadeinspeed;
        float currentTime = 0;
        float start = 0.1f;
        float targetVolume = 1;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void enternewzone(AudioClip music)
    {
        oldzonemusic = currentzonemusic;
        oldzonemusictimer = audiosource.time;
        zoneentertime = Time.time;
        currentzonemusic = music;
        if (Statics.infight == false)
        {
            startfadeout(currentzonemusic, 0, 1, 3);
        }
    }
    public void enteroldzone(AudioClip music)
    {
        float musicstartpoint;
        float timedifference = Mathf.Abs(Time.time - zoneentertime);
        if (timedifference > 20)                                      // wenn die zeit zwischen den gleichen zonen wechsel länger als 30sec her ist wird der song zurückgesetzt
        {
            musicstartpoint = 0;
        }
        else
        {
            musicstartpoint = oldzonemusictimer;
        }
        oldzonemusic = currentzonemusic;
        oldzonemusictimer = audiosource.time;
        zoneentertime = Time.time;
        currentzonemusic = music;

        if (Statics.infight == false)
        {
            startfadeout(currentzonemusic, musicstartpoint, 1, 3);
        }
    }
}