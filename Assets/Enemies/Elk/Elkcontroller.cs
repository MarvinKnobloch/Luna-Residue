using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elkcontroller : MonoBehaviour
{
    [SerializeField] private GameObject elkcircle;
    [SerializeField] private GameObject elkcone;
    [SerializeField] private GameObject elkconecollider;
    [SerializeField] private float circledmg;
    [SerializeField] private float circletriggertime;
    [SerializeField] private float conedmg;
    [SerializeField] private float conetriggertime;

    private void Awake()
    {
        elkcircle.GetComponent<Elkcircle>().basedmg = circledmg;
        elkconecollider.GetComponent<Elkconedmg>().basedmg = conedmg;
    }
    private void OnEnable()
    {
        elkcircle.SetActive(true);
        Invoke("dealcircledmg", circletriggertime);
    }
    private void dealcircledmg()
    {
        elkcircle.GetComponent<Elkcircle>().dealdmg();
        Invoke("dealconedmg", conetriggertime);
    }
    private void dealconedmg()
    {
        elkconecollider.SetActive(true);
    }
    public void conedisable()
    {
        elkconecollider.SetActive(false);
        elkcone.SetActive(false);
        gameObject.SetActive(false);
    }
}
