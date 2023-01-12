using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Uiactionscontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dashtext;
    [SerializeField] private TextMeshProUGUI healtext;
    [SerializeField] private TextMeshProUGUI weaponswitchtext;
    [SerializeField] private TextMeshProUGUI charswitchtext;
    [SerializeField] private TextMeshProUGUI spezialspelltext;

    [SerializeField] private GameObject spell1;
    [SerializeField] private GameObject spell2;
    [SerializeField] private GameObject spell3;
    [SerializeField] private GameObject spell4;
    [SerializeField] private GameObject spell5;
    [SerializeField] private GameObject spell6;

    private SpielerSteu Steuerung;

    private void Start()
    {
        Steuerung = Keybindinputmanager.inputActions;

        dashtext.text = Steuerung.Player.Dash.GetBindingDisplayString();
        healtext.text = Steuerung.Player.Heal.GetBindingDisplayString();
        charswitchtext.text = Steuerung.Player.Charchange.GetBindingDisplayString();
        spezialspelltext.text = Steuerung.Player.Spezial.GetBindingDisplayString();
        weaponswitchtext.text = Steuerung.Player.Weaponchange.GetBindingDisplayString();

        spell1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Steuerung.Player.Ability1.GetBindingDisplayString();
        spell2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Steuerung.Player.Ability2.GetBindingDisplayString();
        spell3.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Steuerung.Player.Ability3.GetBindingDisplayString();
        spell4.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Steuerung.Player.Ability4.GetBindingDisplayString();

        if (Statics.currentactiveplayer == 0)
        {
            spell1.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[0];
            spell2.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[1];
        }
        else
        {
            spell1.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[2];
            spell2.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[3];
        }
        spell3.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[4];
        spell4.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[5];
        spell5.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[6];
        spell6.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[7];
    }
    public void setimagecolor()               //wird im LoadCharmanager called
    {
        if (Statics.currentactiveplayer == 0)
        {
            spell1.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[0];
            spell2.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[1];
        }
        else
        {
            spell1.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[2];
            spell2.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[3];
        }
        spell3.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[4];
        spell4.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[5];
        spell5.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[6];
        spell6.transform.GetChild(0).GetComponent<Image>().color = Statics.spellcolors[7];
    }
}
