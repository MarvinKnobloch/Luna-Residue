using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ninjasavezone : MonoBehaviour
{
    [SerializeField] private GameObject ninjastarcontroller;
    private bool issavezone;
    [SerializeField] private Color startcolor;
    [SerializeField] private Color savecolor;
    [SerializeField] private float switchtosavezonetimer;
    private float turnofftimer;
    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject timerobj;
    [SerializeField] private TextMeshProUGUI timertext;
    private float time;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        turnofftimer = 6 - switchtosavezonetimer;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        timertext.text = string.Format("{0:0}", time);
    }
    private void OnEnable()
    {
        time = switchtosavezonetimer;
        meshRenderer.material.color = startcolor;
        issavezone = false;
        timertext.text = string.Format("{0:0}", time);
        timerobj.SetActive(true);
        Invoke("switchsavezone", switchtosavezonetimer);
    }
    private void switchsavezone()
    {
        timerobj.SetActive(false);
        meshRenderer.material.color = savecolor;
        issavezone = true;
        Invoke("turnoff", turnofftimer);
    }
    private void turnoff()
    {
        ninjastarcontroller.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemythrowable") && issavezone == true)
        {
            other.gameObject.SetActive(false);
        }
    }
}
