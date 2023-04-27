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
    [TextArea] [SerializeField] public string[] dialogue;
    private int dialogueindex;

    public bool interaction;
    [SerializeField] private TextMeshProUGUI npcinteraction;
    public string interactiontext;
    [SerializeField] private MonoBehaviour interactionscript;

    private string interactionhotkeyname;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
        interactionhotkeyname = controlls.Menusteuerung.Rightclick.GetBindingDisplayString();
    }
    private void OnEnable()
    {
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoemptystate();
        LoadCharmanager.disableattackbuttons = true;
        LoadCharmanager.interaction = true;
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
        if (controlls.Menusteuerung.Leftclick.WasPressedThisFrame())
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
                startinteraction();
                enddialogue();
            }
        }
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
            enddialogue();
        }
        if (controlls.Menusteuerung.Rightclick.WasPerformedThisFrame() && interaction == true)
        {
            startinteraction();
            enddialogue();
        }
    }
    IEnumerator startdialogue()
    {
        foreach(char letter in dialogue[dialogueindex].ToCharArray())
        {
            dialoguetext.text += letter;
            yield return new WaitForSeconds(Statics.dialoguetextspeed);
        }
        StopCoroutine(startdialogue());
    }
    private void startinteraction()
    {
        if (interaction == true)
        {
            if (interactionscript != null)
            {
                interactionscript.enabled = true;
            }
        }
        else
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
        }
    }
    public void enddialogue()
    {
        StopAllCoroutines();
        npcdialogueui.SetActive(false);
        enabled = false;
    }
}
