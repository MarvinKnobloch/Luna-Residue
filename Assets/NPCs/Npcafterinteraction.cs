using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npcafterinteraction : MonoBehaviour
{
    private SpielerSteu controlls;

    [SerializeField] private GameObject npcdialogueui;
    [SerializeField] private TextMeshProUGUI dialoguetext;
    [TextArea] [SerializeField] public string[] dialogue;
    private int dialogueindex;

    [SerializeField] private TextMeshProUGUI npcinteraction;

    private void Awake()
    {
        controlls = Keybindinputmanager.inputActions;
    }
    private void OnEnable()
    {
        npcdialogueui.SetActive(true);
        dialogueindex = 0;
        dialoguetext.text = string.Empty;
        npcinteraction.gameObject.SetActive(false);
        StartCoroutine(startdialogue());
    }

    private void Update()
    {
        if (controlls.Player.Interaction.WasPressedThisFrame())
        {
            if (dialogueindex < dialogue.Length - 1)
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
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
            LoadCharmanager.disableattackbuttons = false;
            LoadCharmanager.interaction = false;
            enddialogue();
        }
    }
    IEnumerator startdialogue()
    {
        foreach (char letter in dialogue[dialogueindex].ToCharArray())
        {
            dialoguetext.text += letter;
            yield return new WaitForSeconds(Statics.dialoguetextspeed);
        }
        StopCoroutine(startdialogue());
    }
    public void enddialogue()
    {
        StopAllCoroutines();
        LoadCharmanager.Overallmainchar.GetComponent<Movescript>().switchtoairstate();
        LoadCharmanager.disableattackbuttons = false;
        LoadCharmanager.interaction = false;
        npcdialogueui.SetActive(false);
        enabled = false;
    }
}
