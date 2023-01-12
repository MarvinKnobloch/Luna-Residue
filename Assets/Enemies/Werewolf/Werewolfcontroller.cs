using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Werewolfcontroller : MonoBehaviour
{
    [SerializeField] private GameObject werewolfsphere;
    [SerializeField] private GameObject dazeimage;
    [SerializeField] private Text dazetext;
    private SpielerSteu Steuerung;
    private InputAction action;

    [SerializeField] private float spheredmg;
    [SerializeField] private int buttonpressesneed;
    [SerializeField] private float timetopressbuttons;

    const string dazestate = "Daze";

    private void Awake()
    {
        Steuerung = Keybindinputmanager.inputActions;
        action = Steuerung.Player.Attack3;
        werewolfsphere.GetComponent<Werewolfsphere>().basedmg = spheredmg;
        werewolfsphere.GetComponent<Werewolfsphere>().explodetime = timetopressbuttons;

    }
    private void OnEnable()
    {
        werewolfsphere.transform.position = LoadCharmanager.Overallmainchar.transform.position;
        werewolfsphere.SetActive(true);
        if (Statics.dash == false)
        {
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().ChangeAnimationStateInstant(dazestate);
            LoadCharmanager.Overallmainchar.GetComponent<Movescript>().state = Movescript.State.Daze;
            dazeimage.SetActive(true);
            dazetext.text = "Spam " + action.GetBindingDisplayString();
            Statics.dazestunstart = true;
            Statics.dazecounter = 0;
            Statics.dazekicksneeded = buttonpressesneed;
            Statics.dash = true;
        }
    }
}
