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

    private int currenttextindex;
    private string animatedtext;

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
        currenttextindex = 0;
        while (currenttextindex < dialogue[dialogueindex].Length)
        {
            currenttextindex++;
            dialoguetext.text = dialogue[dialogueindex];
            animatedtext = dialoguetext.text.Insert(currenttextindex, "<color=#00000000>");     //insert: der zweite wert, in der klammer, wird nach dem currentindex hinzugefügt, und danach wird der text normal beendet
            dialoguetext.text = animatedtext;                                                   //z.b text ist hallo, Insert(2, cya) = hacyallo
            yield return new WaitForSeconds(Statics.dialoguetextspeed);                         //in diesem fall, wird nach dem index die farbe auf null geändert, also ist der text danach unsichtbar
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
