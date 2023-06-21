using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videocontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private VideoPlayer videoplayer;
    private bool isplaying;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        isplaying = false;
        videoplayer.Pause();

    }

    private void Update()
    {
        if (controlls.Menusteuerung.Space.WasPerformedThisFrame())
        {
            if(isplaying == false)
            {
                isplaying = true;
                playvideo();
            }
            else
            {
                isplaying = false;
                pausevideo();
            }
        }
        if (controlls.Menusteuerung.F1.WasPerformedThisFrame())
        {
            restartvideo();
        }
    }

    public void playvideo()
    {
        videoplayer.Play();
    }
    public void pausevideo()
    {
        videoplayer.Pause();
    }
    public void restartvideo()
    {
        if(videoplayer.clip != null)
        {
            StopCoroutine("restartnewvideo");
            //StopCoroutine("restartwhilepause");
            playvideo();
            videoplayer.frame = 0;
            //if (isplaying == false) StartCoroutine("restartwhilepause");
            //else (isplaying == true);
            isplaying = true;
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
        if (videoplayer.clip == null) videoplayer.targetTexture.Release();
        else
        {
            if (isplaying == false)
            {
                StopCoroutine("restartnewvideo");
                //StopCoroutine("restartwhilepause");
                playvideo();
                videoplayer.frame = 0;
                StartCoroutine("restartnewvideo");
            } 
            else restartvideo();
        }
    }
}
