using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Videosurface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject playimage;
    [SerializeField] private Sprite playsprite;
    [SerializeField] private Sprite pausesprite;
    [SerializeField] private Videomenucontroller videocontroller;

    private bool overplayer;

    private void OnEnable()
    {
        playimage.SetActive(false);
        overplayer = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (videocontroller.videoplayer.clip != null)
        {
            StopCoroutine("disablepauseimage");
            if (videocontroller.isplaying == true)
            {
                playimage.GetComponent<Image>().sprite = pausesprite;
                StartCoroutine("disablepauseimage");
            }
            else playimage.GetComponent<Image>().sprite = playsprite;
            playimage.SetActive(true);
            overplayer = true;
        }
    }
    IEnumerator disablepauseimage()
    {
        DateTime startdate = DateTime.Now;
        DateTime currentdate;
        float time = 0;
        while (time < 1)
        {
            currentdate = DateTime.Now;
            time = 0.0000001f * (currentdate.Ticks - startdate.Ticks);
            yield return null;
        }
        playimage.SetActive(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (videocontroller.isplaying == true) playimage.SetActive(false);
        overplayer = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (videocontroller.videoplayer.clip != null)
        {
            StopCoroutine("disablepauseimage");
            playimage.SetActive(true);
            videocontroller.switchplaystate();
        }          
    }
    public void switchicon()
    {
        if (videocontroller.isplaying == false)
        {
            playimage.GetComponent<Image>().sprite = playsprite;
            if (overplayer == false) playimage.SetActive(true);
        }
        else
        {
            playimage.GetComponent<Image>().sprite = pausesprite;
            if (overplayer == false) playimage.SetActive(false);
            else StartCoroutine("disablepauseimage");
        }

    }
}
