using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currenttutorial : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject tutorialcontroller;

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
        if (controlls.Menusteuerung.F1.WasPressedThisFrame() && tutorialcontroller.activeSelf == false)
        {
            tutorialcontroller.SetActive(true);
            tutorialcontroller.GetComponent<Tutorialcontroller>().opencurrenttutorial();
        }
    }
}
