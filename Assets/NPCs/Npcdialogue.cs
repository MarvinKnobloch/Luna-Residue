using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class Npcdialogue : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject npcdialogueui;
    [SerializeField] private TextMeshProUGUI dialoguetext;
    [TextArea] [SerializeField] private string[] dialogue;
    private int dialogueindex;

    [SerializeField] private bool interaction;
    [SerializeField] private TextMeshProUGUI npcinteraction;
    [SerializeField] private string interactiontext;

    private string interactionhotkeyname;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        interactionhotkeyname = controlls.Menusteuerung.Rightclick.GetBindingDisplayString();
    }
    private void OnEnable()
    {
        LoadCharmanager.disableattackbuttons = true;
        StopAllCoroutines();
        npcdialogueui.SetActive(true);
        dialogueindex = 0;
        dialoguetext.text = string.Empty;
        if (interaction == true)
        {
            npcinteraction.gameObject.SetActive(true);
            npcinteraction.text = interactionhotkeyname + interactiontext;
        }
        else
        {
            npcinteraction.gameObject.SetActive(false);
        }
        StartCoroutine(startdialogue());
    }
    private void Update()
    {
        if (controlls.Player.Interaction.WasPressedThisFrame())
        {
            if(dialogueindex < dialogue.Length - 1)
            {
                StopAllCoroutines();
                dialoguetext.text = string.Empty;
                dialogueindex++;
                StartCoroutine(startdialogue());
            }
            else
            {
                if(interaction == true)
                {
                    //openshop
                }
                enddialogue();
            }
        }
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            enddialogue();
        }
        if (controlls.Menusteuerung.Rightclick.WasPerformedThisFrame() && interaction == true)
        {
            //openshop
            enddialogue();
        }
    }
    IEnumerator startdialogue()
    {
        foreach(char letter in dialogue[dialogueindex].ToCharArray())
        {
            dialoguetext.text += letter;
            yield return new WaitForSeconds(Statics.npcdialoguetextspeed);
        }
        StopCoroutine(startdialogue());
    }
    public void enddialogue()
    {
        StopAllCoroutines();
        npcdialogueui.SetActive(false);
        LoadCharmanager.disableattackbuttons = false;
        enabled = false;
    }
}
