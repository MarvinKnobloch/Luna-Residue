using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fasttravelcontroller : MonoBehaviour
{
    private SpielerSteu steuerung;
    [SerializeField] private GameObject menuoverview;
    [SerializeField] private GameObject commitfasttravel;

    [SerializeField] private Menusoundcontroller menusoundcontroller;
    private void Awake()
    {
        steuerung = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        steuerung.Enable();
    }
    private void Update()
    {
        if (steuerung.Menusteuerung.Menuesc.WasPerformedThisFrame() && commitfasttravel.activeSelf == false)
        {
            menuoverview.SetActive(true);
            gameObject.SetActive(false);
            menusoundcontroller.playmenubuttonsound();
        }
    }
}
