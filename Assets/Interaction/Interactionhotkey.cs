using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interactionhotkey : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionhotkey;
    private SpielerSteu controlls;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }

    private void OnEnable()
    {
        interactionhotkey.text = "(" + controlls.Menusteuerung.Leftclick.GetBindingDisplayString() + ")";
    }
}
