using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Npcupdatedialogue : MonoBehaviour
{
    [SerializeField] public Areacontroller areacontroller;
    [NonSerialized] public int dialoguenumber;

    private Npcdialogue npcdialogue;
    [SerializeField] private int newnpcdialogueamount;
    [TextArea][SerializeField] private string[] newdialogue;
    [SerializeField] private bool secondinteraction;
    [SerializeField] private string secondinteractiontext;
    private void Awake()
    {
        npcdialogue = GetComponent<Npcdialogue>();
        dialoguenumber = GetComponent<Areanumber>().areanumber;
    }
    private void OnEnable()
    {
        StartCoroutine("textupdate");
    }
    IEnumerator textupdate()
    {
        yield return null;
        if (areacontroller.npcdialoguestate[dialoguenumber] == 1)
        {
            npcdialogue.dialogue = new string[newnpcdialogueamount];
            for (int i = 0; i < newdialogue.Length; i++)
            {
                npcdialogue.dialogue[i] = newdialogue[i];
            }
            npcdialogue.interaction = secondinteraction;
            npcdialogue.interactiontext = secondinteractiontext;
        }
        enabled = false;
    }
}
