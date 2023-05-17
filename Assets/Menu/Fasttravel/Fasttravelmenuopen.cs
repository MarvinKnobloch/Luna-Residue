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

    [SerializeField] private Areacontroller areacontroller;
    [SerializeField] private Travelpointvalues[] alltravelpoints;
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
        if (controlls.Menusteuerung.Unlockfasttravel.WasPerformedThisFrame())
        {
            unlockfasttravelpoints();
            menusoundcontroller.playmenubuttonsound();
        }
    }
    private void unlockfasttravelpoints()
    {
        for (int i = 0; i < areacontroller.gotfasttravelpoint.Length; i++)
        {
            if (Fasttravelpoints.travelpoints.Contains(alltravelpoints[i]) == false)
            {
                Fasttravelpoints.travelpoints.Add(alltravelpoints[i]);
            }
            areacontroller.gotfasttravelpoint[i] = true;
        }
    }
}
