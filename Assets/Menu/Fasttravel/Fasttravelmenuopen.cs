using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fasttravelmenuopen : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject menuoverview;
    [SerializeField] private GameObject commitfasttravel;

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
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitfasttravel.activeSelf == false)
        {
            menuoverview.SetActive(true);
            gameObject.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
    }
}
