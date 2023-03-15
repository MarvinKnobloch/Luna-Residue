using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjasavezone : MonoBehaviour
{
    [SerializeField] private GameObject ninjastarcontroller;
    private bool issavezone;
    [SerializeField] private Color startcolor;
    [SerializeField] private Color savecolor;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Material>();
    }

    private void OnEnable()
    {
        material.color = startcolor;
        issavezone = false;
        Invoke("switchsavezone", 3f);
    }
    private void switchsavezone()
    {
        material.color = savecolor;
        issavezone = true;
        Invoke("turnoff", 3f);
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
