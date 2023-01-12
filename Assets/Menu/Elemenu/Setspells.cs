using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setspells : MonoBehaviour
{
    public int spellslot;
    [SerializeField] public int spellnumber;                             //für dragswitch
    public int spellelement;                                             //steh auf 0(Feuerelement) beim spielstart, keine Ahnung ob das was macht

    private Dragslotspell dragslotspell;
    private void Awake()
    {
        dragslotspell = GetComponent<Dragslotspell>();   
    }
    public void setspell(int newspellnumber, Color spellslotcolor, string spelltext)
    {
        spellnumber = newspellnumber;
        Statics.spellnumbers[spellslot] = newspellnumber;
        Statics.spellcolors[spellslot] = spellslotcolor;
        spellelement = (int) Mathf.Floor((float)newspellnumber / 3);
        dragslotspell.gotspell = true;
        dragslotspell.spellnumber = newspellnumber;
        dragslotspell.spellcolor = spellslotcolor;
        dragslotspell.spelltext = spelltext;
    }
    public void ondragswitch()                                         //für dragswitch
    {
        Statics.spellnumbers[spellslot] = spellnumber;
        Statics.spellcolors[spellslot] = GetComponent<Image>().color;
        spellelement = (int)Mathf.Floor((float)spellnumber / 3);
        dragslotspell.spellnumber = spellnumber;
        dragslotspell.spellcolor = GetComponent<Image>().color;
        dragslotspell.spelltext = GetComponentInChildren<TextMeshProUGUI>().text;
    }
}
