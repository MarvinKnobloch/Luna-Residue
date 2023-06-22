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

    [SerializeField] private GameObject dashbutton;
    [SerializeField] public Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
        checkforfinishedtutorials();
        EventSystem.current.SetSelectedGameObject(dashbutton);
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
