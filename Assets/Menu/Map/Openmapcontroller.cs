using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openmapcontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject mapimage;
    [SerializeField] private GameObject minimap;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void Start()
    {
        minimap.SetActive(true);                 //minimap wir erst geladen nachdem alles andere geladen ist
    }
    private void Update()
    {
        if(controlls.Player.Map.WasPerformedThisFrame() && Statics.infight == false && LoadCharmanager.disableattackbuttons == false)
        {
            if (mapimage.activeSelf == false)
            {
                mapimage.SetActive(true);
            }
            else 
            {
                mapimage.SetActive(false);
            }
            
        }
    }
}
