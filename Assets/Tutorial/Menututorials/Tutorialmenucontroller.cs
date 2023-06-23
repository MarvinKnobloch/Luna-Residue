using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorialmenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menuoverview;
    [SerializeField] private Areacontroller areacontroller;

    [SerializeField] private GameObject[] starttutorialbuttons;
    [SerializeField] private GameObject targettutorialbutton;
    [SerializeField] private GameObject[] elementaltutorialbuttons;

    [SerializeField] public Menusoundcontroller menusoundcontroller;

    public float scrollsize;
    public float highestrect;
    public float lowestrect;
    public float currenthighestposi;
    public float currentlowestposi;
    [SerializeField] private RectTransform scrollrecttransfrom;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        currenthighestposi = -1000;
        lowestrect = 0;
        controlls.Enable();
        checkforfinishedtutorials();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            menuoverview.SetActive(true);
            gameObject.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
    }
    public void checkforhighstrec(Transform rect, float rectheight)
    {
        float rectvalue = rect.localPosition.y;
        if (rectvalue > currenthighestposi)
        {
            currenthighestposi = rectvalue;
            highestrect = rectvalue;
            currentlowestposi = rectvalue - scrollrecttransfrom.rect.height;
        }
        if (rectvalue < lowestrect)
        {
            lowestrect = rectvalue - rectheight;
        }
        scrollsize = (lowestrect - highestrect + scrollrecttransfrom.rect.height) * -1;
    }
    public void upperrectupdate(float posi)
    {
        currenthighestposi = posi;
        currentlowestposi = posi - scrollrecttransfrom.rect.height;
    }
    public void lowerrectupdate(float posi, float rectheight)
    {
        currentlowestposi = posi - rectheight;
        currenthighestposi = posi - rectheight + scrollrecttransfrom.rect.height;
    }
    private void checkforfinishedtutorials()
    {
        if (areacontroller.tutorialcomplete[1] == true) targettutorialbutton.SetActive(true);
        else targettutorialbutton.SetActive(false);
        if (Statics.elementalmenuunlocked == true)
        {
            for (int i = 0; i < elementaltutorialbuttons.Length; i++)
            {
                elementaltutorialbuttons[i].SetActive(true);
            }
        }
        else 
        {
            for (int i = 0; i < elementaltutorialbuttons.Length; i++)
            {
                elementaltutorialbuttons[i].SetActive(false);
            }
        }
    }
}
