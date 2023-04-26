using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Mathmancontroller : MonoBehaviour
{
    [SerializeField] private GameObject mathcommit;             //answertime ist bei mathmancommit wegen onenable reihenfolge(der wert wird beim erstenmal nicht richtig geladen)
    [SerializeField] private Text mathtasktext;
    [SerializeField] private int lowerfirstnumber;
    [SerializeField] private int upperfirstnumber;
    [SerializeField] private int lowersecondnumber;
    [SerializeField] private int uppersecondnumber;

    [SerializeField] private int spezialdmg;

    private int firstnumber;
    private int secondnumber;
    [NonSerialized] public int rightanswer;

    private void Awake()
    {
        mathcommit.GetComponent<Mathcommit>().basedmg = spezialdmg;
    }

    private void OnEnable()
    {
        firstnumber = UnityEngine.Random.Range(lowerfirstnumber, upperfirstnumber);
        secondnumber = UnityEngine.Random.Range(lowersecondnumber, uppersecondnumber);
        rightanswer = firstnumber - secondnumber;
        mathtasktext.text = firstnumber.ToString() + " - " + secondnumber.ToString();
    }
}
