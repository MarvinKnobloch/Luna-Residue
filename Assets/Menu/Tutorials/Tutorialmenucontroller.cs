using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialmenucontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menuoverview;
    [SerializeField] private Areacontroller areacontroller;

    [SerializeField] private GameObject[] starttutorialbuttons;
    [SerializeField] private GameObject targettutorialbutton;
    [SerializeField] private GameObject elementaltutorialbutton;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
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
    private void checkforfinishedtutorials()
    {
        if (areacontroller.tutorialcomplete[0] == true)
        {
            for (int i = 0; i < starttutorialbuttons.Length; i++)
            {
                starttutorialbuttons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < starttutorialbuttons.Length; i++)
            {
                starttutorialbuttons[i].SetActive(false);
            }
        }
        if (areacontroller.tutorialcomplete[1] == true) targettutorialbutton.SetActive(true);
        else targettutorialbutton.SetActive(false);
        if (Statics.elementalmenuunlocked == true) elementaltutorialbutton.SetActive(true);
        else elementaltutorialbutton.SetActive(false);
    }
}
