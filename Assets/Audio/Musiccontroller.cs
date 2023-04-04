using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musiccontroller : MonoBehaviour
{
    public static Musiccontroller instance;
    [SerializeField] private AudioSource audiosource;
    public AudioClip[] allzonesongs;
    public int oldsongint;
    
    public AudioClip currentzonemusic;
    public float currentzonemusictime;

    public AudioClip oldzonemusic;
    public float zoneentertime;
    public float oldzonemusictimer;

    [SerializeField] private AudioClip normalbattle;
    [SerializeField] private AudioClip spezialbattle;
    private void Awake()
    {
        if (instance == null)
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
        StopAllCoroutines();
    }

    public void setcurrentzonemusic(int songint)
    {
        if (currentzonemusic != allzonesongs[songint])
        {
            Statics.currentzonemusicint = songint;
            currentzonemusic = allzonesongs[songint];
            audiosource.clip = allzonesongs[songint];
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
    public void startfadeout(AudioClip song, float cliptime, float fadeoutspeed, float fadeinspeed)
    {
        StopAllCoroutines();
        StartCoroutine(fadeoutvolume(song, cliptime, fadeoutspeed, fadeinspeed));
    }
    public IEnumerator fadeoutvolume(AudioClip song, float cliptime, float fadeoutspeed, float fadeinspeed)
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
        if(song != null)
        {
            audiosource.clip = song;
            audiosource.time = cliptime;
            audiosource.Play();
            StartCoroutine(fadeinvolume(fadeinspeed));
        }
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
    public IEnumerator gameoverfadeout(float fadeoutspeed)
    {
        float duration = fadeoutspeed;
        float currentTime = 0;
        float start = audiosource.volume;
        float targetVolume = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        StopCoroutine("gameoverfadeout");
    }
    public void enternewzone(int songint)
    {
        oldsongint = Statics.currentzonemusicint;
        oldzonemusic = currentzonemusic;
        oldzonemusictimer = audiosource.time;
        zoneentertime = Time.time;
        Statics.currentzonemusicint = songint;
        currentzonemusic = allzonesongs[songint];
        if (Statics.infight == false)
        {
            startfadeout(allzonesongs[songint], 0, 1, 3);
        }
    }
    public void enteroldzone(int songint)
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
        oldsongint = Statics.currentzonemusicint;
        oldzonemusic = currentzonemusic;
        oldzonemusictimer = audiosource.time;
        zoneentertime = Time.time;
        Statics.currentzonemusicint = songint;
        currentzonemusic = allzonesongs[songint];

        if (Statics.infight == false)
        {
            startfadeout(allzonesongs[songint], musicstartpoint, 1, 3);
        }
    }
    public void enemynormalbattle()
    {
        if(Statics.infight == false)
        {
            savezonemusictime();
            startfadeout(normalbattle, 0, 0.5f, 1.5f);
        }
    }
    public void spezialbattlemusic()
    {
        if (Statics.infight == false)
        {
            savezonemusictime();
        }
        if(currentzonemusic != spezialbattle)
        {
            startfadeout(spezialbattle, 0, 0.5f, 1.5f);
        }
    }
}