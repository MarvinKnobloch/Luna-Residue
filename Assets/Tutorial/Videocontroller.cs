using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videocontroller : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private VideoPlayer videoplayer;
    private bool isplaying;
    [SerializeField] private GameObject playicon;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        videoplayer.frame = 0;
        videoplayer.targetTexture.Release();
        controlls.Enable();      
    }

    private void Update()
    {
        if (controlls.Menusteuerung.Space.WasPerformedThisFrame())
        {
            switchplaystate();
        }
        if (controlls.Menusteuerung.F2.WasPerformedThisFrame())
        {
            restartvideo();
        }
    }
    public void switchplaystate()
    {
        if (isplaying == false)
        {
            isplaying = true;
            videoplayer.Play();
            playicon.SetActive(false);
        }
        else
        {
            isplaying = false;
            videoplayer.Pause();
            playicon.SetActive(true);
        }
    }
    public void restartvideo()
    {
        if (videoplayer.clip != null)
        {
            playicon.SetActive(false);
            videoplayer.Play();
            videoplayer.frame = 0;
            isplaying = true;
        }
    }
    public void setnewvideo(VideoClip newvideo)
    {
        isplaying = true;
        playicon.SetActive(false);
        videoplayer.clip = newvideo;
        videoplayer.frame = 0;
        videoplayer.Play();
    }
}
