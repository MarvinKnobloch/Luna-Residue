using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openmapcontroller : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject mapimage;
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject cardinalpoints;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void Start()
    {
        minimap.SetActive(true);                 //minimap wir erst geladen nachdem alles andere geladen ist
        cardinalpoints.SetActive(true);
    }
    private void Update()
    {
        if(controlls.Player.Map.WasPerformedThisFrame() && Statics.infight == false && LoadCharmanager.gameispaused == false)
        {
            if (mapimage.activeSelf == false)
            {
                mapimage.SetActive(true);
            }           
        }
    }
}
