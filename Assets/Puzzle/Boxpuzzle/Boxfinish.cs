using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boxfinish : MonoBehaviour
{
    [SerializeField] private GameObject requiredcube;
    public Color finishcolor;
    public bool finish;

    public static event Action checkforbridge;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == requiredcube)
        {
            Invoke("checkforposi", 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == requiredcube)
        {
            CancelInvoke();
            requiredcube.GetComponent<Renderer>().material.color = Color.white;
            finish = false;
            checkforbridge?.Invoke();
        }
    }
    private void checkforposi()
    {
        requiredcube.GetComponent<Renderer>().material.color = finishcolor;
        finish = true;
        checkforbridge?.Invoke();
    }
}
