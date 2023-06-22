using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videocontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    public VideoPlayer videoplayer;
    public bool isplaying;

    [SerializeField] private Videosurface videosurface;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        isplaying = true;
        videoplayer.Pause();
        videoplayer.targetTexture.Release();
    }

    private void Update()
    {
        if (controlls.Menusteuerung.Space.WasPerformedThisFrame())
        {
            switchplaystate();
        }
        if (controlls.Menusteuerung.F1.WasPerformedThisFrame())
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
        }
        else
        {
            isplaying = false;
            videoplayer.Pause();
        }
        videosurface.switchicon();
    }
    public void restartvideo()
    {
        if(videoplayer.clip != null)
        {
            StopCoroutine("restartnewvideo");
            //StopCoroutine("restartwhilepause");
            videoplayer.Play();
            videoplayer.frame = 0;
            //if (isplaying == false) StartCoroutine("restartwhilepause");
            //else (isplaying == true);
            isplaying = true;
            videosurface.switchicon();
        }
    }
    IEnumerator restartnewvideo()
    {
        while (videoplayer.frame < 5)
        {
            yield return null;
        }
        videoplayer.Pause();
    }
    /*IEnumerator restartwhilepause()                //funktioniert aber wenn ich restart drück will ich doch auch, dass das video wieder anfängt zu laufen
    {
        while (videoplayer.frame > 5)                //das video wird kurz weitergespielt bis die 0 frames von videoplayer.frame = 0 angenommmen sind, danach wird dann pausiert
        {
            yield return null;
        }
        Debug.Log("stop");
        videoplayer.Pause();
    }*/
    public void newvideo(VideoClip newvideo)
    {
        videoplayer.clip = newvideo;
        if (videoplayer.clip == null)
        {
            videoplayer.targetTexture.Release();
            videosurface.playimage.SetActive(false);
        }
        else
        {
            if (isplaying == false)
            {
                videosurface.playimage.SetActive(true);
                StopCoroutine("restartnewvideo");
                //StopCoroutine("restartwhilepause");
                videoplayer.Play();
                videoplayer.frame = 0;
                StartCoroutine("restartnewvideo");
            }
            else restartvideo();
        }
    }
}
