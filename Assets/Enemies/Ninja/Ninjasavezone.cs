using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjasavezone : MonoBehaviour
{
    [SerializeField] private GameObject ninjastarcontroller;
    private bool issavezone;
    [SerializeField] private Color startcolor;
    [SerializeField] private Color savecolor;
    [SerializeField] private float switchtosavezonetimer;
    private float turnofftimer;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        turnofftimer = 6 - switchtosavezonetimer;
    }

    private void OnEnable()
    {
        meshRenderer.material.color = startcolor;
        issavezone = false;
        Invoke("switchsavezone", switchtosavezonetimer);
    }
    private void switchsavezone()
    {
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
