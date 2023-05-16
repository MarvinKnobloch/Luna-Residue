using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fpscounteractivate : MonoBehaviour
{
    private SpielerSteu controlls;
    [SerializeField] private GameObject fpscounter;
    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    void Update()
    {
        if(controlls.Menusteuerung.Fps.WasPerformedThisFrame())
        {
            if(fpscounter.activeSelf == false) fpscounter.SetActive(true);
            else fpscounter.SetActive(false);
        }
    }
}
