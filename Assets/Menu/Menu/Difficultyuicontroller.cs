using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficultyuicontroller : MonoBehaviour
{
    private SpielerSteu controlls;
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
            gameObject.SetActive(false);
        }
    }
}
