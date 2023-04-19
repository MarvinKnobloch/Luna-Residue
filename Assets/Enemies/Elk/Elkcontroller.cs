using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elkcontroller : MonoBehaviour
{
    [SerializeField] private GameObject elkcircle;
    [SerializeField] private GameObject elkcone;
    [SerializeField] private GameObject elkconecollider;
    [SerializeField] private float circledmg;
    [SerializeField] private float conedmg;

    private void Awake()
    {
        elkcircle.GetComponent<Elkcircle>().basedmg = circledmg;
        elkconecollider.GetComponent<Elkconedmg>().basedmg = conedmg;
    }

    public void conedisable()
    {
        elkconecollider.SetActive(false);
        elkcone.SetActive(false);
        gameObject.SetActive(false);
    }
}
