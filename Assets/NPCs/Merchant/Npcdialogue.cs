using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcdialogue : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject npcdialogueui;
    [SerializeField] private TextMeshProUGUI dialoguetext;
    [TextArea] [SerializeField] private string[] dialogue;
    private int dialogueindex;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        LoadCharmanager.disableattackbuttons = true;
        StopAllCoroutines();
        npcdialogueui.SetActive(true);
        dialogueindex = 0;
        dialoguetext.text = string.Empty;
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
                enddialogue();
            }
        }
        if (controlls.Menusteuerung.Menuesc.WasPerformedThisFrame())
        {
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
