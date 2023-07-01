using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditscontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menuobj;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    private void Update()
    {
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            menusoundcontroller.playmenubuttonsound();
            menuobj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
