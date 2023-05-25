using System.Collections;
using TMPro;
using UnityEngine;
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
    private int currenttextindex;
    private string animatedtext;


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
                startinteraction();
                if (gameObject.TryGetComponent(out Endquest questend))
                {
                    Debug.Log("test");
                    questend.endquest();
                }
                if (gameObject.TryGetComponent(out Startquest queststart))
                {
                    queststart.startquest();
                }
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
